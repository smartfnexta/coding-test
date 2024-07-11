using coding_test_model.Api.ReceiveOrders;
using coding_test_qa_api.App.Modules;

namespace coding_test_qa_api.App.Api.ReceiveOrders.Services
{
    /// <summary>
    /// 受注取得サービスのインターフェース
    /// </summary>
    [DIComponent]
    public interface IGetReceiveOrderService
    {
        /// <summary>
        /// 全受注を取得する
        /// </summary>
        /// <returns></returns>
        GetAllReceiveOrderResponse GetAll();
        
        /// <summary>
        /// 受注を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetReceiveOrderResponse Get(long id);
    }

    /// <summary>
    /// 受注取得サービス
    /// </summary>
    public class GetReceiveOrderService : IGetReceiveOrderService
    {
        public GetReceiveOrderResponse Get(long id)
        {
            // TODO: 実装済みサービスを参考に実装をお願いします
            return null;
        }

        public GetAllReceiveOrderResponse GetAll()
        {
            // TODO: 実装済みサービスを参考に実装をお願いします
            return null;
        }
    }
}
