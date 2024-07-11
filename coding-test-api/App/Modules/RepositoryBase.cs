using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace coding_test_qa_api.App.Modules
{
    /// <summary>
    /// リポジトリ基底のインターフェース
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// テーブルを作成する
        /// </summary>
        void CreateTable();

        /// <summary>
        /// 全レコードを取得する
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> SelectAll();

        /// <summary>
        /// IDが一致するレコードを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Select(long id);

        /// <summary>
        /// 条件に一致するレコードを取得する
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SelectAny(Dictionary<string, object> conditions);

        /// <summary>
        /// レコードを挿入する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);

        /// <summary>
        /// レコードを更新する
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="targetColumns"></param>
        /// <param name="whereParameters"></param>
        /// <returns></returns>
        int Update(TEntity entity, IEnumerable<string> targetColumns, Dictionary<string, object> whereParameters);

        /// <summary>
        /// レコードを削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(long id);
    }

    /// <summary>
    /// リポジトリ基底
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> 
        : IRepositoryBase<TEntity> where TEntity : class
    {
        private static Dictionary<Type, DbType> _dbTypeMap = new Dictionary<Type, DbType>()
        {
            { typeof(long), DbType.Int64 },
            { typeof(Byte[]), DbType.Binary},
            { typeof(bool), DbType.Boolean },
            { typeof(string), DbType.String },
            { typeof(DateTime), DbType.DateTime },
            { typeof(DateTimeOffset), DbType.DateTimeOffset },
            { typeof(decimal), DbType.Decimal },
            { typeof(object), DbType.Object },
            { typeof(double), DbType.Double },
            { typeof(Single), DbType.Single },
            { typeof(int), DbType.Int32 },
            { typeof(short), DbType.Int16 },
            { typeof(TimeSpan), DbType.Time },
            { typeof(byte), DbType.Byte },
            { typeof(Guid), DbType.Guid }
        };

        private static Dictionary<Type, string> _dbTypeNameMap = new Dictionary<Type, string>()
        {
            { typeof(long), "bigint" },
            { typeof(string), "nvarchar" },
            { typeof(bool), "bit" },
            { typeof(decimal), "decimal" },
            { typeof(int), "int" },

            // NOTE:
            // "datetime" だと 0000/01/01 がエラーになるが "datetime2" だとエラーにならない
            // あくまで一時テーブル作成用なので、有効値が広い "datetime2" を使うことにする
            { typeof(DateTime), "datetime2" }
        };

        private readonly IDbSession dbSession;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public RepositoryBase(IDbSession dbSession)
        {
            this.dbSession = dbSession;
        }

        /// <inheritdoc/>
        public void CreateTable()
        {
            var tableType = typeof(TEntity);
            
            var parametersQuery = tableType.GetProperties().Where(x => x.CanWrite).Select(property =>
            {
                var type = property.PropertyType;
                var nullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

                if (nullable)
                {
                    type = Nullable.GetUnderlyingType(type);
                }

                var name = property.Name;
                var typeName = property.GetCustomAttribute<ColumnAttribute>()?.TypeName;

                if (string.IsNullOrEmpty(typeName))
                {
                    typeName = _dbTypeNameMap[type];
                }

                string length;

                if (type == typeof(string))
                {
                    var maxLenthAttribute = property.GetCustomAttribute<MaxLengthAttribute>();

                    if (maxLenthAttribute != null)
                    {
                        length = $"({maxLenthAttribute.Length})";
                    }
                    else
                    {
                        length = "(255)";
                    }
                }
                else if (type == typeof(decimal))
                {
                    var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();

                    if (rangeAttribute != null)
                    {
                        length = $"({rangeAttribute.Minimum},{rangeAttribute.Maximum})";
                    }
                    else
                    {
                        length = "(18,8)";
                    }
                }
                else
                {
                    length = string.Empty;
                }

                var nullString = (nullable || type == typeof(string)) ? "NULL" : "NOT NULL";

                return $"{property.Name} {typeName}{length} {nullString}";

            }).ToList();

            var sql = $"create table {tableType.Name} ({string.Join(",", parametersQuery)})";
            this.dbSession.Connection.Execute(sql, null, null, null, CommandType.Text);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> SelectAll()
        {
            var sql = $"SELECT * FROM {typeof(TEntity).Name}";
            return this.dbSession.Connection.Query<TEntity>(sql);
        }

        /// <inheritdoc/>
        public TEntity Select(long id)
        {
            var queryParameters = new Dictionary<string, object>()
            {
                { "id", id }
            };

            var sql = $"SELECT * FROM {typeof(TEntity).Name} WHERE Id = @id";
            var dbArgs = this.CreateQueryParameter(queryParameters);

            var entities = this.dbSession.Connection.Query<TEntity>(sql, dbArgs, dbSession.Transaction);
            return entities.FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> SelectAny(Dictionary<string, object> conditions)
        {
            if (conditions is null || !conditions.Any())
            {
                return SelectAll();
            }

            var whereClause = conditions.Any() ? $"WHERE {string.Join(",", conditions.Select(x => $"{x.Key}=@{x.Key}"))}" : string.Empty;

            var sql = $"SELECT * FROM {typeof(TEntity).Name} {whereClause}";
            var dbArgs = this.CreateQueryParameter(conditions);

            var entities = this.dbSession.Connection.Query<TEntity>(sql, dbArgs, dbSession.Transaction);
            return entities;
        }

        /// <inheritdoc/>
        public int Insert(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties().ToDictionary(x => x.Name, y => y.GetValue(entity));
            var queryParameters = properties.ToDictionary(x => $"@{x.Key}", y => y.Value);
            var columns = properties.Keys;
            var insertKeyClause = string.Join(", ", columns);
            var insertValueClause = string.Join(",", columns.Select(x => $"@{x}"));

            var sql = $@"INSERT INTO {typeof(TEntity).Name} ({insertKeyClause}) VALUES ( {insertValueClause})";
            var dbArgs = this.CreateQueryParameter(queryParameters);
            return this.dbSession.Connection.ExecuteScalar<int>(sql, dbArgs, dbSession.Transaction);
        }

        /// <inheritdoc/>
        public int Update(TEntity entity, IEnumerable<string> targetColumns, Dictionary<string, object> whereParameters)
        {
            var columns = string.Join(", ", targetColumns.Select(x => $"{x} =@{x}"));
            var whereClause = string.Join(", ", whereParameters.Select(x => $"{x.Key}=@{x.Key}"));
            var sql = $"UPDATE {typeof(TEntity).Name} SET {columns} WHERE {whereClause}";
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            var queryParameterDictionary = CreateQueryParameterDictionary(entity, targetColumns);
            result = result.Concat(queryParameterDictionary);
            result = result.Concat(whereParameters);
            var parameters = result.ToDictionary(x => x.Key, x => x.Value);
            return dbSession.Connection.ExecuteScalar<int>(sql, parameters, dbSession.Transaction);
        }

        /// <inheritdoc/>
        public int Delete(long id)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, object> CreateQueryParameterDictionary(TEntity entity, IEnumerable<string> targetColumns)
        {
            var dic = new Dictionary<string, object>();
            foreach (var targetColumn in targetColumns)
            {
                var property = typeof(TEntity).GetProperty(targetColumn);
                if (property != null)
                {
                    dic.Add(targetColumn, property.GetValue(entity));
                }
            }
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        protected DynamicParameters CreateQueryParameter(Dictionary<string, object> queryParameters)
        {
            var dbArgs = new DynamicParameters();

            if (null == queryParameters
                || !queryParameters.Any())
            {
                return dbArgs;
            }

            foreach (var queryParameter in queryParameters)
            {
                var name = queryParameter.Key;
                var value = queryParameter.Value;

                if (value != null && _dbTypeMap.ContainsKey(queryParameter.Value.GetType()))
                {
                    var dbType = _dbTypeMap[queryParameter.Value.GetType()];
                    dbArgs.Add(name, value, dbType);
                }
                else if (value != null && value is DataTable)
                {
                    var dataTable = value as DataTable;
                    var tableParameter = dataTable.AsTableValuedParameter(dataTable.TableName);
                    dbArgs.Add(name, tableParameter);
                }
                else
                {
                    dbArgs.Add(name, value);
                }
            }

            return dbArgs;
        }

        
    }
}
