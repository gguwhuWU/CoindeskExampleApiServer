using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace CoindeskExampleApiServerTest
{


    [TestFixture]
    public class CoindeskApiServiceTest
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<ILogger<CoindeskApiService>> _mockLogger;
        private CoindeskApiService _service;

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.coindesk.com/")
            };
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _mockLogger = new Mock<ILogger<CoindeskApiService>>();
            _service = new CoindeskApiService(_mockLogger.Object, _mockHttpClientFactory.Object);
        }

        [Test]
        public async Task GetBPICurrentprice_ShouldReturnSuccess_WhenApiResponseIsSuccessful()
        {
            // Arrange
            var jsonResponse = "{\r\n    \"time\": {\r\n        \"updated\": \"Jun 23, 2024 07:47:42 UTC\",\r\n        \"updatedISO\": \"2024-06-23T07:47:42+00:00\",\r\n        \"updateduk\": \"Jun 23, 2024 at 08:47 BST\"\r\n    },\r\n    \"disclaimer\": \"This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org\",\r\n    \"chartName\": \"Bitcoin\",\r\n    \"bpi\": {\r\n        \"USD\": {\r\n            \"code\": \"USD\",\r\n            \"symbol\": \"&#36;\",\r\n            \"rate\": \"64,357.85\",\r\n            \"description\": \"United States Dollar\",\r\n            \"rate_float\": 64357.8504\r\n        },\r\n        \"GBP\": {\r\n            \"code\": \"GBP\",\r\n            \"symbol\": \"&pound;\",\r\n            \"rate\": \"50,861.687\",\r\n            \"description\": \"British Pound Sterling\",\r\n            \"rate_float\": 50861.6874\r\n        },\r\n        \"EUR\": {\r\n            \"code\": \"EUR\",\r\n            \"symbol\": \"&euro;\",\r\n            \"rate\": \"60,189.714\",\r\n            \"description\": \"Euro\",\r\n            \"rate_float\": 60189.7142\r\n        }\r\n    }\r\n}";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            // Act
            var result = await _service.GetBPICurrentprice();
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.TargetResult!.chartName, Is.EqualTo(JsonSerializer.Deserialize<OriginalCurrentpriceInfo>(jsonResponse)!.chartName));
                Assert.That(result.Message, Is.Null);
            });
        }

        [Test]
        public async Task GetBPICurrentprice_ShouldReturnError_WhenApiResponseIsNotSuccessful()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act
            var result = await _service.GetBPICurrentprice();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.TargetResult, Is.Null);
                Assert.That(result.Message, Is.EqualTo("GetBPICurrentprice 失敗"));
            });
        }

        [Test]
        public async Task GetBPICurrentprice_ShouldReturnError_WhenJsonDeserializationFails()
        {
            // Arrange
            var invalidJsonResponse = "invalid json";

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(invalidJsonResponse)
                });

            // Act
            var result = await _service.GetBPICurrentprice();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.TargetResult, Is.Null);
                Assert.That(result.Message, Is.EqualTo("解析 JSON 錯誤"));
            });
        }

        [Test]
        public async Task GetBPICurrentprice_ShouldReturnError_WhenHttpRequestFails()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _service.GetBPICurrentprice();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.TargetResult, Is.Null);
                Assert.That(result.Message, Is.EqualTo("HTTP request 錯誤"));
            });
        }
    }
}
