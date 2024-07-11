using coding_test_qa_api.App.Api.Inventories.Services;
using coding_test_qa_api.App.Api.ReceiveOrders.Services;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_qa_api.App.Api.ReceiveOrders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetReceiveOrderController : ControllerBase
    {
        private readonly IGetReceiveOrderService getReceiveOrderService;

        public GetReceiveOrderController(IGetReceiveOrderService getReceiveOrderService)
        {
            this.getReceiveOrderService = getReceiveOrderService;
        }

        /// <summary>
        /// 受注を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetReceiveOrder([FromQuery] long id)
        {
            var result = this.getReceiveOrderService.Get(id);
            return Ok(result);
        }

        /// <summary>
        /// 全受注を取得する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllReceiveOrder()
        {
            var result = this.getReceiveOrderService.GetAll();
            return Ok(result);
        }
    }


}
