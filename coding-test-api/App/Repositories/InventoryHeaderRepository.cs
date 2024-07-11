using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 在庫ヘッダリポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IInventoryHeaderRepository : IRepositoryBase<InventoryHeader>
    {
    }

    /// <summary>
    /// 在庫ヘッダリポジトリ
    /// </summary>
    public class InventoryHeaderRepository : RepositoryBase<InventoryHeader>, IInventoryHeaderRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public InventoryHeaderRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
