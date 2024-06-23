using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoindeskExampleApiServer.Controllers
{
    /// <summary>
    ///     幣匯
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CoindeskController(
        ICoindeskApiService coindeskApiService,
        ICoindeskService coindeskService) : ControllerBase
    {
        private readonly ICoindeskApiService _coindeskApiService = coindeskApiService;
        private readonly ICoindeskService _coindeskService = coindeskService;

        /// <summary>
        ///     抓取原api 幣匯
        /// </summary>
        /// <returns></returns>
        [HttpGet("original")]
        [SwaggerResponse(200, Description = "抓取原api 幣匯 成功", Type = typeof(OriginalCurrentpriceInfo))]
        [SwaggerResponse(500, Description = "抓取原api 幣匯 失敗")]
        public async Task<IActionResult> GetOriginalBPICurrentprice()
        {
            var result = await _coindeskApiService.GetBPICurrentprice();
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return Ok(result.TargetResult);
        }

        /// <summary>
        ///      抓取轉換過的幣匯資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [SwaggerResponse(200, Description = "抓取轉換過的幣匯資料 成功", Type = typeof(List<CurrentpriceInfo>))]
        [SwaggerResponse(500, Description = "抓取轉換過的幣匯資料 失敗")]
        public async Task<IActionResult> GetAllBPICurrentprice()
        {
            var result = await _coindeskService.GetAllBPICurrentprice();
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return Ok(result.TargetResult);
        }
    }
}
