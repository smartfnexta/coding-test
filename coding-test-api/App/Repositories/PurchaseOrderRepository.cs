using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;
using Dapper;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 発注リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IPurchaseOrderRepository
    {
        /// <summary>
        /// 全レコードを取得する
        /// </summary>
        /// <returns></returns>
        IEnumerable<PurchaseOrder> SelectAll();

        /// <summary>
        /// IDが一致するレコードを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PurchaseOrder Select(long id);
    }

    /// <summary>
    /// 発注リポジトリ
    /// </summary>
    public class PurchaseOrderRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly IDbSession dbSession;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public PurchaseOrderRepository(IDbSession dbSession) : base(dbSession) 
        {
            this.dbSession = dbSession;
        }

        /// <inheritdoc/>
        public new PurchaseOrder Select(long headerId)
        {
            var queryParameters = new Dictionary<string, object>()
            {
                { "Id", headerId }
            };

            return this.SelectInner(queryParameters).FirstOrDefault();
        }

        /// <inheritdoc/>
        public new IEnumerable<PurchaseOrder> SelectAll()
        {
            var queryParameters = new Dictionary<string, object>();
            return this.SelectInner(queryParameters);
        }

        private IEnumerable<PurchaseOrder> SelectInner(Dictionary<string, object> conditions)
        {

            var whereClause = conditions.Any() ? $"WHERE {string.Join(",", conditions.Select(x => $"{x.Key}=@{x.Key}"))}" : string.Empty;

            var sql = @$"
SELECT
    *
FROM
(
    SELECT
        PurchaseOrderHeader.Id As Id,
        Company.Name AS CompanyName,
        Item.Name AS ItemName,
        PurchaseOrderDetail.Quantity AS Quantity,
        Item.UnitPrice AS UnitPrice,
        (Item.UnitPrice * PurchaseOrderDetail.Quantity) AS Price
    FROM
        PurchaseOrderHeader
        LEFT JOIN PurchaseOrderDetail ON PurchaseOrderDetail.PurchaseOrderHeaderId = PurchaseOrderHeader.Id
        LEFT JOIN Company ON Company.Id = PurchaseOrderHeader.CompanyId
        LEFT JOIN Item ON Item.Id = PurchaseOrderDetail.ItemId
) TEMP {whereClause}";

            var dbArgs = this.CreateQueryParameter(conditions);
            var entities = this.dbSession.Connection.Query<PurchaseOrder>(sql, dbArgs, dbSession.Transaction);
            return entities;
        }

    }
}
