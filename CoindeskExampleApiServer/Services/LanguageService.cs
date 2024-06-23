using CoindeskExampleApiServer.Models.Enum;
using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LanguageService(IHttpContextAccessor httpContextAccessor) : ILanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SupportedLanguageType GetCurrentLanguage()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return SupportedLanguageType.English_US;
            }

            var requestCulture = httpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture;
            var currentCulture = requestCulture?.Culture ?? CultureInfo.CurrentCulture;

            return currentCulture.Name switch
            {
                "en-US" => SupportedLanguageType.English_US,
                "zh-TW" => SupportedLanguageType.Chinese_TW,
                "ja-JP" => SupportedLanguageType.Japanese_JP,
                _ => SupportedLanguageType.English_US,
            };
        }
    }
}
