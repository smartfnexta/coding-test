using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 発注ヘッダリポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IPurchaseOrderHeaderRepository : IRepositoryBase<PurchaseOrderHeader>
    {
    }

    /// <summary>
    /// 発注ヘッダリポジトリ
    /// </summary>
    public class PurchaseOrderHeaderRepository : RepositoryBase<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public PurchaseOrderHeaderRepository(IDbSession dbSession) : base(dbSession)
        {
            
        }

    }
}
