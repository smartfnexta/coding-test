using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace coding_test_qa_api.App.Modules
{
    /// <summary>
    /// DBセッションのインターフェース
    /// </summary>
    [DIComponent(DIContainerType.Singleton)]
    public interface IDbSession
    {
        /// <summary>
        /// DBコネクション
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// DBトランザクション
        /// </summary>
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// DBを開く
        /// </summary>
        void Open();
    }

    /// <summary>
    /// DBセッション
    /// </summary>
    public class DbSession : IDbSession
    {
        private readonly IConfigurationAccessor configurationAccessor;
        private SQLiteConnectionStringBuilder connectionString;
        private IDbConnection connection;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configurationAccessor"></param>
        public DbSession(IConfigurationAccessor configurationAccessor)
        {
            this.configurationAccessor = configurationAccessor;
            var dataSource = this.configurationAccessor.Get<string>("DbConnection");
            this.connectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = dataSource
            };
        }

        /// <inheritdoc/>
        public IDbConnection Connection
        {
            get
            {
                if (this.Transaction == null)
                {
                    return connection;
                }
                else
                {
                    return this.Transaction.Connection;
                }
            }
        }

        /// <inheritdoc/>
        public IDbTransaction Transaction { get; set; }

        /// <inheritdoc/>
        public void Open()
        {
            if (connection is null
                || connection.State != ConnectionState.Open) 
            {
                connection = new SQLiteConnection(this.connectionString.ToString());
                connection.Open();
            }
        }
    }
}
