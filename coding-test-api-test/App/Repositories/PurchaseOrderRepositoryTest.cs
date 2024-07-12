using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;
using coding_test_qa_api.App.Repositories;
using Dapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding_test_api_test.App.Repositories
{
    /// <summary>
    /// <see cref="PurchaseOrderRepository"/>の単体テスト
    /// </summary>
    public class PurchaseOrderRepositoryTest
    {
        private Mock<IDbSession> dbSession = new Mock<IDbSession>();
        private Mock<IDbConnection> dbConnection = new Mock<IDbConnection>();
        private Mock<IDbTransaction> dbTransaction = new Mock<IDbTransaction>();

        /// <summary>
        /// 正常系_Select
        /// </summary>
        [Fact]
        public void OkSelect()
        {
            // Arrange
            dbTransaction.Setup(x => x.Connection).Returns(dbConnection.Object);
            dbSession.Setup(x => x.Connection).Returns(dbConnection.Object);
            dbSession.Setup(x => x.Transaction).Returns(dbTransaction.Object);

            var target = new PurchaseOrderRepository(
            dbSession.Object
            );

            long id = 1;

            // Act
            var actual = target.Select(id);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<PurchaseOrder>(actual);
        }

        /// <summary>
        /// 正常系_SelectAll
        /// </summary>
        [Fact]
        public void OkSelectAll()
        {
            // Arrange
            dbTransaction.Setup(x => x.Connection).Returns(dbConnection.Object);
            dbSession.Setup(x => x.Connection).Returns(dbConnection.Object);
            dbSession.Setup(x => x.Transaction).Returns(dbTransaction.Object);

            var target = new PurchaseOrderRepository(
            dbSession.Object
            );

            // Act
            var actual = target.SelectAll();

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<List<PurchaseOrder>>(actual);
        }
    }
}
