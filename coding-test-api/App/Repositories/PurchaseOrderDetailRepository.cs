using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Repositories
{
    /// <summary>
    /// 発注詳細リポジトリのインターフェース
    /// </summary>
    [DIComponent]
    public interface IPurchaseOrderDetailRepository : IRepositoryBase<PurchaseOrderDetail>
    {
    }

    /// <summary>
    /// 発注詳細リポジトリ
    /// </summary>
    public class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public PurchaseOrderDetailRepository(IDbSession dbSession) : base(dbSession)
        {
            
        }

    }
}
