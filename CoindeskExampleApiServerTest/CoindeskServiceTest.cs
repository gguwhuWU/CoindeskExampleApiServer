using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Models.Enum;
using CoindeskExampleApiServer.Protocols.Repositories;
using CoindeskExampleApiServer.Protocols.Services;
using CoindeskExampleApiServer.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CoindeskExampleApiServerTest
{
    [TestFixture]
    public class CoindeskServiceTest
    {
        private Mock<ICoindeskApiService> _mockCoindeskApiService;
        private Mock<ICopilotRepository> _mockCopilotRepository;
        private Mock<ILanguageService> _mockLanguageService;
        private CoindeskService _service;
        private ILogger<CoindeskService> _logger;

        [SetUp]
        public void Setup()
        {
            _mockCoindeskApiService = new Mock<ICoindeskApiService>();
            _mockCopilotRepository = new Mock<ICopilotRepository>();
            _mockLanguageService = new Mock<ILanguageService>();
            _logger = new Logger<CoindeskService>(new NullLoggerFactory());
            _service = new CoindeskService(
                _logger, 
                _mockCoindeskApiService.Object, 
                _mockCopilotRepository.Object,
                _mockLanguageService.Object);
        }

        [Test]
        public async Task GetAllBPICurrentprice_ShouldReturnSuccessResult_WhenApiCallIsSuccessful()
        {
            // Arrange
            var mockResponse = new ServiceResult<OriginalCurrentpriceInfo>
            {
                IsSuccess = true,
                TargetResult = new OriginalCurrentpriceInfo
                {
                    time = new Time { updatedISO = DateTimeOffset.UtcNow },
                    bpi = new BPI
                    {
                        USD = new BPIInfo { code = "USD", rate_float = 50000 },
                        GBP = new BPIInfo { code = "GBP", rate_float = 40000 },
                        EUR = new BPIInfo { code = "EUR", rate_float = 45000 }
                    }
                }
            };

            _mockCoindeskApiService
                .Setup(service => service.GetBPICurrentprice())
                .ReturnsAsync(mockResponse);

            _mockLanguageService
                .Setup(service => service.GetCurrentLanguage())
                .Returns(SupportedLanguageType.Chinese_TW);

            _mockCopilotRepository.Setup(x => x.Find("USD")).Returns(new Copilot { ZWName = "美元" });
            _mockCopilotRepository.Setup(x => x.Find("GBP")).Returns(new Copilot { ZWName = "英鎊" });
            _mockCopilotRepository.Setup(x => x.Find("EUR")).Returns(new Copilot { ZWName = "歐元" });

            // Act
            var result = await _service.GetAllBPICurrentprice();
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result!.TargetResult!, Has.Count.EqualTo(3));
                Assert.That(result!.TargetResult![0].Code!, Is.EqualTo("USD"));
                Assert.That(result!.TargetResult![0].CodeDisplayName!, Is.EqualTo("美元"));
                Assert.That(result!.TargetResult![0].Rate!, Is.EqualTo(50000));
                Assert.That(result!.TargetResult![1].Code!, Is.EqualTo("GBP"));
                Assert.That(result!.TargetResult![1].Rate!, Is.EqualTo(40000));
                Assert.That(result!.TargetResult![1].CodeDisplayName!, Is.EqualTo("英鎊"));
                Assert.That(result!.TargetResult![2].Code!, Is.EqualTo("EUR"));
                Assert.That(result!.TargetResult![2].Rate!, Is.EqualTo(45000));
                Assert.That(result!.TargetResult![2].CodeDisplayName!, Is.EqualTo("歐元"));
            });
        }

        [Test]
        public async Task GetAllBPICurrentprice_ShouldReturnFailureResult_WhenApiCallFails()
        {
            // Arrange
            var mockResponse = new ServiceResult<OriginalCurrentpriceInfo>
            {
                IsSuccess = false
            };

            _mockCoindeskApiService
                .Setup(service => service.GetBPICurrentprice())
                .ReturnsAsync(mockResponse);

            _mockLanguageService
               .Setup(service => service.GetCurrentLanguage())
               .Returns(SupportedLanguageType.Chinese_TW);

            // Act
            var result = await _service.GetAllBPICurrentprice();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.Message, Is.EqualTo("GetAllBPICurrentprice 失敗"));
                Assert.That(result.TargetResult!, Is.Empty);
            });
        }
    }
}