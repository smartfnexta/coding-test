using coding_test_qa_api.App.Api.Inventories.Services;
using coding_test_qa_api.App.Api.PurchaseOrders.Services;
using coding_test_qa_api.App.Api.ReceiveOrders.Services;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_qa_api.App.Api.Purchases.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPurchaseOrderController : ControllerBase
    {
        private readonly IGetPurchaseOrderService getPurchaseOrderService;

        public GetPurchaseOrderController(IGetPurchaseOrderService getPurchaseOrderService)
        {
            this.getPurchaseOrderService = getPurchaseOrderService;
        }

        /// <summary>
        /// 出荷を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPurchaseOrder([FromQuery] long id)
        {
            var result = this.getPurchaseOrderService.Get(id);
            return Ok(result);
        }

        /// <summary>
        /// 全出荷を取得する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllPurchaseOrder()
        {
            var result = this.getPurchaseOrderService.GetAll();
            return Ok(result);
        }
    }


}
