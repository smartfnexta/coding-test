using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 受注リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IReceiveOrderRepository : IRepositoryBase<ReceiveOrder>
    {
        ReceiveOrder Select(long headerId);
    }

    /// <summary>
    /// 受注リポジトリ
    /// </summary>
    public class ReceiveOrderRepository : RepositoryBase<ReceiveOrder>, IReceiveOrderRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public ReceiveOrderRepository(IDbSession dbSession) : base(dbSession)
        {
        }
    }
}
