using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using coding_test_qa_api.App.Api.Inventories.Services;
using coding_test_qa_api.App.Repositories;
using coding_test_qa_api.App.Modules;
using coding_test_model.Entities;
using coding_test_model.Api.Inventories;
using Microsoft.AspNetCore.Mvc;

namespace coding_test_api_test.App.Api.Inventories.Services
{
    /// <summary>
    /// <see cref="GetInventoryService"/>の単体テスト
    /// </summary>
    public class GetInventoryServiceTest
    {
        private Mock<IDbSession> dbSession = new Mock<IDbSession>();
        private Mock<IInventoryHeaderRepository> inventoryHeaderRepository = new Mock<IInventoryHeaderRepository>();
        private Mock<IInventoryDetailRepository> inventoryDetailRepository = new Mock<IInventoryDetailRepository>();
        private Mock<IItemRepository> itemRepository = new Mock<IItemRepository>();
        private Mock<IAreaRepository> areaRepository = new Mock<IAreaRepository>();

        /// <summary>
        /// 正常系_Get
        /// </summary>
        [Fact]
        public void OkGet()
        {
            inventoryHeaderRepository.Setup(x => x.Select(It.IsAny<long>())).Returns(new InventoryHeader());
            itemRepository.Setup(x => x.Select(It.IsAny<long>())).Returns(new Item());

            // Arrange
            var target = new GetInventoryService(
                dbSession.Object,
                inventoryHeaderRepository.Object,
                inventoryDetailRepository.Object,
                itemRepository.Object,
                areaRepository.Object
                );

            long id = 1;

            // Act
            var actual = target.Get(id);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<GetInventoryResponse>(actual);

            var response = actual as GetInventoryResponse;

            Assert.NotNull(response);
            Assert.Equal(0, response.Inventory.TotalStock);
        }

        /// <summary>
        /// 正常系_GetAll
        /// </summary>
        [Fact]
        public void OkGetAll()
        {
            // Arrange
            var target = new GetInventoryService(
                dbSession.Object,
                inventoryHeaderRepository.Object,
                inventoryDetailRepository.Object,
                itemRepository.Object,
                areaRepository.Object
                );

            // Act
            var actual = target.GetAll();

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<GetAllInventoriesResponse>(actual);

            var response = actual as GetAllInventoriesResponse;
            Assert.Equal(0, response.Inventories.Count());
        }

        /// <summary>
        /// 正常系_GetTotalStock
        /// </summary>
        [Fact]
        public void OkGetTotalStock()
        {
            // Arrange
            var target = new GetInventoryService(
                dbSession.Object,
                inventoryHeaderRepository.Object,
                inventoryDetailRepository.Object,
                itemRepository.Object,
                areaRepository.Object
                );

            long itemId = 1;

            // Act
            var actual = target.GetTotalStock(itemId);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<GetGetTotalStockResopnse>(actual);

            var response = actual as GetGetTotalStockResopnse;
            Assert.Equal(0, response.TotalStock);
        }
    }
}
