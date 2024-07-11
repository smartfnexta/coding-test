using coding_test_model.Api.PurchaseOrders;
using coding_test_qa_api.App.Modules;
using coding_test_qa_api.App.Repositories;

namespace coding_test_qa_api.App.Api.PurchaseOrders.Services
{
    /// <summary>
    /// 発注取得サービスのインターフェース
    /// </summary>
    [DIComponent]
    public interface IGetPurchaseOrderService
    {
        /// <summary>
        /// 全発注を取得する
        /// </summary>
        /// <returns></returns>
        GetAllPurchaseOrderResponse GetAll();

        /// <summary>
        /// 発注を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetPurchaseOrderResponse Get(long id);
    }

    /// <summary>
    /// 発注取得サービス
    /// </summary>
    public class GetPurchaseOrderService : IGetPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository purchaseOrderRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="purchaseOrderRepository"></param>
        public GetPurchaseOrderService(IDbSession dbSession, IPurchaseOrderRepository purchaseOrderRepository)
        {
            dbSession.Open();
            this.purchaseOrderRepository = purchaseOrderRepository;
        }        

        /// <inheritdoc/>
        public GetAllPurchaseOrderResponse GetAll()
        {
            var purchaseOrders = this.purchaseOrderRepository.SelectAll();

            return new GetAllPurchaseOrderResponse()
            {
                PurchaseOrders = purchaseOrders
            };
        }

        /// <inheritdoc/>
        public GetPurchaseOrderResponse Get(long id)
        {
            var purchaseOrder = this.purchaseOrderRepository.Select(id);

            return new GetPurchaseOrderResponse()
            {
                PurchaseOrder = purchaseOrder
            };
        }
    }
}
