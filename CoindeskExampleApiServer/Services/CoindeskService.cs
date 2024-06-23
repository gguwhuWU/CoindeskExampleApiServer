using CoindeskExampleApiServer.Extensions;
using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Models.Enum;
using CoindeskExampleApiServer.Protocols.Repositories;
using CoindeskExampleApiServer.Protocols.Services;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="coindeskApiService"></param>
    /// <param name="copilotRepository"></param>
    /// <param name="languageService"></param>
    public class CoindeskService(
        ILogger<CoindeskService> logger,
        ICoindeskApiService coindeskApiService,
        ICopilotRepository copilotRepository,
        ILanguageService languageService) : ICoindeskService
    {
        private readonly ILogger<CoindeskService> _logger = logger;
        private readonly ICoindeskApiService _coindeskApiService = coindeskApiService;
        private readonly ICopilotRepository _copilotRepository = copilotRepository;
        private readonly ILanguageService _languageService = languageService;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<List<CurrentpriceInfo>>> GetAllBPICurrentprice()
        {
            var result = new ServiceResult<List<CurrentpriceInfo>>
            {
                TargetResult = []
            };

            try
            {
                var getBPICurrentpriceResult = await _coindeskApiService.GetBPICurrentprice();
                if (!getBPICurrentpriceResult.IsSuccess ||
                    getBPICurrentpriceResult.TargetResult == null ||
                    getBPICurrentpriceResult.TargetResult.bpi == null)
                {
                    throw new Exception(getBPICurrentpriceResult.Message);
                }

                var time = getBPICurrentpriceResult.TargetResult.time?.updatedISO ?? DateTimeOffset.Now;
                var timeString = time.ToString("yyyy/MM/dd HH:mm:dd");
                SupportedLanguageType languageType = _languageService.GetCurrentLanguage();

                if (getBPICurrentpriceResult.TargetResult.bpi.USD != null)
                {
                    var copilot = _copilotRepository.Find("USD");
                    result.TargetResult.Add(new CurrentpriceInfo
                    {
                        UpdateDateTime = timeString,
                        CodeDisplayName = copilot?.GetDisplayName(languageType) ?? "",
                        Code = getBPICurrentpriceResult.TargetResult.bpi.USD.code ?? "",
                        Rate = getBPICurrentpriceResult.TargetResult.bpi.USD.rate_float
                    });
                }

                if (getBPICurrentpriceResult.TargetResult.bpi.GBP != null)
                {
                    var copilot = _copilotRepository.Find("GBP");
                    result.TargetResult.Add(new CurrentpriceInfo
                    {
                        UpdateDateTime = timeString,
                        CodeDisplayName = copilot?.GetDisplayName(languageType) ?? "",
                        Code = getBPICurrentpriceResult.TargetResult.bpi.GBP.code ?? "",
                        Rate = getBPICurrentpriceResult.TargetResult.bpi.GBP.rate_float
                    });
                }

                if (getBPICurrentpriceResult.TargetResult.bpi.EUR != null)
                {
                    var copilot = _copilotRepository.Find("EUR");
                    result.TargetResult.Add(new CurrentpriceInfo
                    {
                        UpdateDateTime = timeString,
                        CodeDisplayName = copilot?.GetDisplayName(languageType) ?? "",
                        Code = getBPICurrentpriceResult.TargetResult.bpi.EUR.code ?? "",
                        Rate = getBPICurrentpriceResult.TargetResult.bpi.EUR.rate_float
                    });
                }

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "GetAllBPICurrentprice 失敗";
            }

            return result;
        }
    }
}
