using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;
using coding_test_qa_api.App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_qa_api.App.Api
{
    [ApiController]
    [Route("[controller]")]
    public class CreateStubController : ControllerBase
    {
        private readonly IDbSession dbSession;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        public CreateStubController(IDbSession dbSession)
        {
            this.dbSession = dbSession;
        }

        [HttpPut]
        public IActionResult CreateStub()
        {
            dbSession.Open();

            CreateInventoryHeaderStub(dbSession);
            CreateInventoryDetailStub(dbSession);
            CreatePurchaseOrderHeaderStub(dbSession);
            CreatePurchaseOrderDetailStub(dbSession);
            CreateReceiveOrderStub(dbSession);

            CreateAreaStub(dbSession);
            CreateCompanyStub(dbSession);
            CreateItemStub(dbSession);

            return Ok();
        }

        /// <summary>
        /// <see cref="InventoryHeader"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreateInventoryHeaderStub(IDbSession dbSession)
        {
            var repository = new InventoryHeaderRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new InventoryHeader { Id = 1, ItemId = 1 });
            repository.Insert(new InventoryHeader { Id = 2, ItemId = 2 });
            repository.Insert(new InventoryHeader { Id = 3, ItemId = 3 });
        }

        /// <summary>
        /// <see cref="InventoryDetail"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreateInventoryDetailStub(IDbSession dbSession)
        {
            var repository = new InventoryDetailRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new InventoryDetail { Id = 1, InventoryHeaderId = 1, AreaId = 1, StockQuantity = 5 });
            repository.Insert(new InventoryDetail { Id = 2, InventoryHeaderId = 2, AreaId = 1, StockQuantity = 100 });
            repository.Insert(new InventoryDetail { Id = 3, InventoryHeaderId = 3, AreaId = 2, StockQuantity = 10000 });
            repository.Insert(new InventoryDetail { Id = 4, InventoryHeaderId = 1, AreaId = 3, StockQuantity = 3 });
        }


        /// <summary>
        /// <see cref="PurchaseOrderHeader"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreatePurchaseOrderHeaderStub(IDbSession dbSession)
        {
            var repository = new PurchaseOrderHeaderRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new PurchaseOrderHeader { Id = 1, CompanyId = 1 });
            repository.Insert(new PurchaseOrderHeader { Id = 2, CompanyId = 2 });
            repository.Insert(new PurchaseOrderHeader { Id = 3, CompanyId = 3 });
            repository.Insert(new PurchaseOrderHeader { Id = 4, CompanyId = 1 });
        }

        /// <summary>
        /// <see cref="PurchaseOrderDetail"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreatePurchaseOrderDetailStub(IDbSession dbSession)
        {
            var repository = new PurchaseOrderDetailRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new PurchaseOrderDetail { Id = 1, PurchaseOrderHeaderId = 1, ItemId = 1, Quantity = 1 });
            repository.Insert(new PurchaseOrderDetail { Id = 2, PurchaseOrderHeaderId = 2, ItemId = 2, Quantity = 4 });
            repository.Insert(new PurchaseOrderDetail { Id = 3, PurchaseOrderHeaderId = 3, ItemId = 3, Quantity = 100 });
            repository.Insert(new PurchaseOrderDetail { Id = 4, PurchaseOrderHeaderId = 4, ItemId = 1, Quantity = 5 });
        }

        /// <summary>
        /// <see cref="ReceiveOrder"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreateReceiveOrderStub(IDbSession dbSession)
        {
            var repository = new ReceiveOrderRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new ReceiveOrder { Id = 1, PurchaseOrderHeaderId = 1, InventoryHeaderId = 1 });
            repository.Insert(new ReceiveOrder { Id = 2, PurchaseOrderHeaderId = 2, InventoryHeaderId = 2 });
            repository.Insert(new ReceiveOrder { Id = 3, PurchaseOrderHeaderId = 3, InventoryHeaderId = 3 });
            repository.Insert(new ReceiveOrder { Id = 4, PurchaseOrderHeaderId = 1, InventoryHeaderId = 1 });
        }


        /// <summary>
        /// <see cref="Item"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreateItemStub(IDbSession dbSession)
        {
            var repository = new ItemRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new Item { Id = 1, Name = "車", UnitPrice = 1000000});
            repository.Insert(new Item { Id = 2, Name = "タイヤ", UnitPrice = 10000 });
            repository.Insert(new Item { Id = 3, Name = "ネジ", UnitPrice = 100 });
        }

        /// <summary>
        /// <see cref="Area"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private static void CreateAreaStub(IDbSession dbSession)
        {
            var repository = new AreaRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new Area { Id = 1, Name = "北海道" });
            repository.Insert(new Area { Id = 2, Name = "東京" });
            repository.Insert(new Area { Id = 3, Name = "沖縄" });
        }

        /// <summary>
        /// <see cref="Company"/>のスタブデータを作成する
        /// </summary>
        /// <param name="dbSession"></param>
        private void CreateCompanyStub(IDbSession dbSession)
        {
            var repository = new CompanyRepository(dbSession);
            repository.CreateTable();
            repository.Insert(new Company { Id = 1, Name = "株式会社 こぶた" });
            repository.Insert(new Company { Id = 2, Name = "㈱ たぬき" });
            repository.Insert(new Company { Id = 3, Name = "きつねカンパニー" });
        }
    }
}
