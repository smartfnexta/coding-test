using coding_test_qa_api.App.Api.Inventories.Controllers;
using coding_test_qa_api.App.Api.Inventories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace coding_test_api_test.App.Api.Inventories.Controllers
{
    /// <summary>
    /// <see cref="GetInventoryController"/>の単体テスト
    /// </summary>
    public class GetInventoryControllerTest
    {
        private Mock<IGetInventoryService> getInventoryServiceMock = new Mock<IGetInventoryService>();

        /// <summary>
        /// 正常系_GetInventory
        /// </summary>
        [Fact]
        public void OkGetInventory()
        {
            // Arrange
            var target = new GetInventoryController(
                getInventoryServiceMock.Object
                );

            long id = 1;

            // Act
            var actual = target.GetInventory(id);

            // Assert
            Assert.NotNull(actual);
            var result = actual as ObjectResult;
            var statusCode = result?.StatusCode;
            Assert.Equal(StatusCodes.Status200OK, statusCode);

        }

        /// <summary>
        /// 正常系_GetAllInventories
        /// </summary>
        [Fact]
        public void OkGetAllInventories()
        {
            // Arrange
            var target = new GetInventoryController(
                getInventoryServiceMock.Object
                );

            // Act
            var actual = target.GetAllInventories();

            // Assert
            Assert.NotNull(actual);
            var result = actual as ObjectResult;
            var statusCode = result?.StatusCode;
            Assert.Equal(StatusCodes.Status200OK, statusCode);

        }

        /// <summary>
        /// 正常系_GetTotalStock
        /// </summary>
        [Fact]
        public void OkGetTotalStock()
        {
            // Arrange
            var target = new GetInventoryController(
                getInventoryServiceMock.Object
                );

            long itemId = 1;

            // Act
            var actual = target.GetTotalStock(itemId);

            // Assert
            Assert.NotNull(actual);
            var result = actual as ObjectResult;
            var statusCode = result?.StatusCode;
            Assert.Equal(StatusCodes.Status200OK, statusCode);

        }
    }
}
