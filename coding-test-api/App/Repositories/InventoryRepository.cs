using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;
using Dapper;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 在庫リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IInventoryRepository
    {
        decimal GetTotalStock(long itemId);
    }

    /// <summary>
    /// 在庫リポジトリ
    /// </summary>
    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        private readonly IDbSession dbSession;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public InventoryRepository(IDbSession dbSession) : base(dbSession)
        {
            this.dbSession = dbSession;
        }

        public decimal GetTotalStock(long itemId)
        {
            var queryParameters = new Dictionary<string, object>()
            {
                { "itemId", itemId }
            };

            var sql = @"
SELECT
    SUM(StockQuantity) AS TotalStockQuantity
FROM
    Inventory
GROUP BY
    ItemId
HAVING
    ItemId = @itemId
";

            var dbArgs = this.CreateQueryParameter(queryParameters);
            return this.dbSession.Connection.ExecuteScalar<decimal>(sql, dbArgs, dbSession.Transaction);
        }
    }
}
