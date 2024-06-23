using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Protocols.Repositories;
using CoindeskExampleApiServer.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CoindeskExampleApiServerTest
{
    [TestFixture]
    public class CopilotServiceTest
    {
        private Mock<ICopilotRepository> _mockCopilotRepository;
        private CopilotService _service;
        private ILogger<CopilotService> _logger;

        [SetUp]
        public void Setup()
        {
            _mockCopilotRepository = new Mock<ICopilotRepository>();
            _logger = new Logger<CopilotService>(new NullLoggerFactory());
            _service = new CopilotService(_logger, _mockCopilotRepository.Object);
        }

        [Test]
        public void GetAll_ShouldReturnAllItems()
        {
            // Arrange
            var copilotList = new List<Copilot>
            {
               new() { Code = "A" },
               new() { Code = "B" }
            };

            _mockCopilotRepository
                .Setup(repo => repo.FindAll())
                .Returns(copilotList);

            // Act
            var result = _service.GetAll();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.TargetResult!, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void Get_ShouldReturnItem_WhenItemExists()
        {
            // Arrange
            var copilot = new Copilot { Code = "A" };

            _mockCopilotRepository
                .Setup(repo => repo.Find("A"))
                .Returns(copilot);

            // Act
            var result = _service.Get("A");
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.TargetResult, Is.Not.Null);
                Assert.That(result.TargetResult!.Code, Is.EqualTo("A"));
            });

        }

        [Test]
        public void Get_ShouldReturnNotFound_WhenItemDoesNotExist()
        {
            // Arrange
#pragma warning disable CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。
            _mockCopilotRepository
                .Setup(repo => repo.Find("B"))
                .Returns((Copilot)null);
#pragma warning restore CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。

            // Act
            var result = _service.Get("B");
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("notfound"));
            });
        }

        [Test]
        public void Insert_ShouldInsertNewItem_WhenItemDoesNotExist()
        {
            // Arrange
            var copilot = new Copilot { Code = "C" };

            _mockCopilotRepository
                .Setup(repo => repo.Exist("C"))
                .Returns(false);

            // Act
            var result = _service.Insert(copilot);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            _mockCopilotRepository.Verify(repo => repo.Create(copilot), Times.Once);
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Test]
        public void Insert_ShouldReturnError_WhenItemAlreadyExists()
        {
            // Arrange
            var copilot = new Copilot { Code = "D" };

            _mockCopilotRepository
                .Setup(repo => repo.Exist("D"))
                .Returns(true);

            // Act
            var result = _service.Insert(copilot);
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("已存在，無法新增"));
            });
        }

        [Test]
        public void Update_ShouldUpdateItem_WhenItemExists()
        {
            // Arrange
            var copilot = new Copilot { Code = "E" };

            _mockCopilotRepository
                .Setup(repo => repo.Exist("E"))
                .Returns(true);

            // Act
            var result = _service.Update(copilot);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            _mockCopilotRepository.Verify(repo => repo.Update(copilot), Times.Once);
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Test]
        public void Update_ShouldReturnError_WhenItemDoesNotExist()
        {
            // Arrange
            var copilot = new Copilot { Code = "F" };

            _mockCopilotRepository
                .Setup(repo => repo.Exist("F"))
                .Returns(false);

            // Act
            var result = _service.Update(copilot);
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("不存在，無法更新"));
            });
        }

        [Test]
        public void Delete_ShouldDeleteItem_WhenItemExists()
        {
            // Arrange
            var copilot = new Copilot { Code = "G" };

            _mockCopilotRepository
                .Setup(repo => repo.Find("G"))
                .Returns(copilot);

            // Act
            var result = _service.Delete("G");

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            _mockCopilotRepository.Verify(repo => repo.Delete(copilot), Times.Once);
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_ShouldReturnError_WhenItemDoesNotExist()
        {
            // Arrange
#pragma warning disable CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。
            _mockCopilotRepository
                .Setup(repo => repo.Find("H"))
                .Returns((Copilot)null);
#pragma warning restore CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。

            // Act
            var result = _service.Delete("H");
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("不存在，無法刪除"));
            });
        }

        [Test]
        public void InsertBaseInsertData_ShouldInsertData_WhenJsonFileIsReadSuccessfully()
        {
            // Arrange

            // Act
            var result = _service.InsertBaseInsertData();

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteAll_Success()
        {
            // Arrange
            _mockCopilotRepository.Setup(repo => repo.FindAll()).Returns(
            [
                new() { Code = "USD" },
                new() {  Code = "EUR" },
                new() { Code = "GBP" }
            ]);

            // Act
            var result = _service.DeleteAll();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Null); // Ensure no error message on success
            });

            // Verify that Delete and SaveChanges methods were called once
            _mockCopilotRepository.Verify(repo => repo.Delete(It.IsAny<List<Copilot>>()), Times.Once);
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteAll_Failure()
        {
            // Arrange
            _mockCopilotRepository.Setup(repo => repo.FindAll()).Throws(new Exception("Simulated error"));

            // Act
            var result = _service.DeleteAll();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("刪除幣別全部資料 失敗"));
            });

            // Verify that SaveChanges was not called (since deletion failed)
            _mockCopilotRepository.Verify(repo => repo.SaveChanges(), Times.Never);
        }
    }
}


