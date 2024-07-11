using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 在庫詳細リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IInventoryDetailRepository : IRepositoryBase<InventoryDetail>
    {
    }

    /// <summary>
    /// 在庫リポジトリ
    /// </summary>
    public class InventoryDetailRepository : RepositoryBase<InventoryDetail>, IInventoryDetailRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public InventoryDetailRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
