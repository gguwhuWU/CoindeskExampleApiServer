using CoindeskExampleApiServer.Models.Enum;
using CoindeskExampleApiServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Moq;
using System.Globalization;

namespace CoindeskExampleApiServerTest
{
    [TestFixture]
    public class LanguageServiceTest
    {
        [TestCase("en-US", SupportedLanguageType.English_US)]
        [TestCase("zh-TW", SupportedLanguageType.Chinese_TW)]
        [TestCase("ja-JP", SupportedLanguageType.Japanese_JP)]
        [TestCase("fr-FR", SupportedLanguageType.English_US)]
        public void GetCurrentLanguage_ReturnsCorrectLanguage(string cultureName, SupportedLanguageType expectedLanguage)
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();
            var featuresMock = new Mock<IFeatureCollection>();
            var requestCultureFeatureMock = new Mock<IRequestCultureFeature>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

            requestCultureFeatureMock.Setup(x => x.RequestCulture).Returns(new RequestCulture(new CultureInfo(cultureName)));

            featuresMock.Setup(x => x.Get<IRequestCultureFeature>()).Returns(requestCultureFeatureMock.Object);
            httpContextMock.SetupGet(x => x.Features).Returns(featuresMock.Object);

            var service = new LanguageService(httpContextAccessorMock.Object);

            // Act
            var result = service.GetCurrentLanguage();

            // Assert
            Assert.That(result, Is.EqualTo(expectedLanguage));
        }

        [Test]
        public void GetCurrentLanguage_NullHttpContext_ReturnsEnglishUS()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
#pragma warning disable CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
#pragma warning restore CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。

            var service = new LanguageService(httpContextAccessorMock.Object);

            // Act
            var result = service.GetCurrentLanguage();

            // Assert
            Assert.That(result, Is.EqualTo(SupportedLanguageType.English_US));
        }
    }
}
