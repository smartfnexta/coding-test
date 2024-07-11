using coding_test_qa_api.App.Api.Inventories.Services;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_qa_api.App.Api.Inventories.Controllers
{
    /// <summary>
    /// 在庫取得コントローラ
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GetInventoryController : ControllerBase
    {
        private readonly IGetInventoryService getInventoryService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="getInventoryService"></param>
        public GetInventoryController(IGetInventoryService getInventoryService)
        {
            this.getInventoryService = getInventoryService;
        }

        /// <summary>
        /// 在庫を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetInventory([FromQuery] long id)
        {
            var result = this.getInventoryService.Get(id);
            return Ok(result);
        }

        /// <summary>
        /// 全在庫を取得する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllInventories()
        {
            var result = this.getInventoryService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// 在庫を取得する
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TotalStock")]
        public IActionResult GetTotalStock([FromQuery] long itemId)
        {
            var result = this.getInventoryService.GetTotalStock(itemId);
            return Ok(result);
        }
    }
}
