using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Protocols.Services;
using System.Text.Json;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_logger"></param>
    /// <param name="clientFactory"></param>
    public class CoindeskApiService(
        ILogger<CoindeskApiService> _logger,
        IHttpClientFactory clientFactory) : ICoindeskApiService
    {
        private readonly ILogger<CoindeskApiService> _logger = _logger;

        /// <summary>
        /// 
        /// </summary>
        public HttpClient Client { get; } = clientFactory.CreateClient("Coindesk");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<OriginalCurrentpriceInfo>> GetBPICurrentprice()
        {
            var result = new ServiceResult<OriginalCurrentpriceInfo>();

            try
            {
                HttpResponseMessage response = await Client.GetAsync("v1/bpi/currentprice.json");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"StatusCode:{response.StatusCode}");
                }

                string content = await response.Content.ReadAsStringAsync();

                result.TargetResult = JsonSerializer.Deserialize<OriginalCurrentpriceInfo>(content);
                result.IsSuccess = true;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "解析 JSON 錯誤");
                result.Message = "解析 JSON 錯誤";
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request 錯誤");
                result.Message = "HTTP request 錯誤";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "GetBPICurrentprice 失敗";
            }

            return result;
        }
    }
}
