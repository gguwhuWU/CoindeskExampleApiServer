using CoindeskExampleApiServer.Models.Enum;

namespace CoindeskExampleApiServer.Protocols.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SupportedLanguageType GetCurrentLanguage();
    }
}
