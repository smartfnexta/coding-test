using coding_test_qa_api.App.Api.PurchaseOrders.Services;
using coding_test_qa_api.App.Api.Purchases.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding_test_api_test.App.Api.Purchase.Controllers
{
    /// <summary>
    /// <see cref="GetPurchaseOrderController"/>の単体テスト
    /// </summary>
    public class GetPurchaseOrderControllerTest
    {
        private Mock<IGetPurchaseOrderService> getPurchaseOrderServiceMock = new Mock<IGetPurchaseOrderService>();

        /// <summary>
        /// 正常系_GetPurchaseOrder
        /// </summary>
        [Fact]
        public void OkGetPurchaseOrder()
        {
            // Arrange
            var target = new GetPurchaseOrderController(
                getPurchaseOrderServiceMock.Object
                );

            long id = 1;

            // Act
            var actual = target.GetPurchaseOrder(id);

            // Assert
            Assert.NotNull(actual);
            var result = actual as ObjectResult;
            var statusCode = result?.StatusCode;
            Assert.Equal(StatusCodes.Status200OK, statusCode);

        }

        /// <summary>
        /// 正常系_GetAllPurchaseOrder
        /// </summary>
        [Fact]
        public void OkGetAllPurchaseOrder()
        {
            // Arrange
            var target = new GetPurchaseOrderController(
                getPurchaseOrderServiceMock.Object
                );

            // Act
            var actual = target.GetAllPurchaseOrder();

            // Assert
            Assert.NotNull(actual);
            var result = actual as ObjectResult;
            var statusCode = result?.StatusCode;
            Assert.Equal(StatusCodes.Status200OK, statusCode);

        }
    }
}
