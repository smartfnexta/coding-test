using coding_test_model.Api.PurchaseOrders;
using coding_test_model.Entities;
using coding_test_qa_api.App.Api.PurchaseOrders.Services;
using coding_test_qa_api.App.Modules;
using coding_test_qa_api.App.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding_test_api_test.App.Api.Purchase.Services
{
    /// <summary>
    /// <see cref="GetPurchaseOrderService"/>の単体テスト
    /// </summary>
    public class GetPurchaseOrderServiceTest
    {
        private Mock<IDbSession> dbSession = new Mock<IDbSession>();
        private Mock<IPurchaseOrderRepository> purchaseOrderRepository = new Mock<IPurchaseOrderRepository>();

        /// <summary>
        /// 正常系_GetAll
        /// </summary>
        [Fact]
        public void OkGetAll()
        {
            purchaseOrderRepository.Setup(x => x.SelectAll()).Returns(new List<PurchaseOrder>()
            {
                new PurchaseOrder(),
                new PurchaseOrder(),
                new PurchaseOrder()
            });

            // Arrange
            var target = new GetPurchaseOrderService(
                dbSession.Object,
                purchaseOrderRepository.Object
                );

            // Act
            var actual = target.GetAll();

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<GetAllPurchaseOrderResponse>(actual);

            var response = actual as GetAllPurchaseOrderResponse;

            Assert.NotNull(response);
            Assert.Equal(3, response.PurchaseOrders.Count());
        }

        /// <summary>
        /// 正常系_Get
        /// </summary>
        [Fact]
        public void OkGet()
        {
            // Arrange
            var target = new GetPurchaseOrderService(
                dbSession.Object,
                purchaseOrderRepository.Object
                );

            long id = 1;

            // Act
            var actual = target.Get(id);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<GetPurchaseOrderResponse>(actual);

            var response = actual as GetPurchaseOrderResponse;

            Assert.Null(response.PurchaseOrder);
        }

    }
}
