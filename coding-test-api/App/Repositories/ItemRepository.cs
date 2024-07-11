using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 品番リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IItemRepository : IRepositoryBase<Item>
    {
    }

    /// <summary>
    /// 品番リポジトリ
    /// </summary>
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public ItemRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
