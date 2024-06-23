using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoindeskExampleApiServer.Controllers
{
    /// <summary>
    ///     ����
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
        ///     �����api ����
        /// </summary>
        /// <returns></returns>
        [HttpGet("original")]
        [SwaggerResponse(200, Description = "�����api ���� ���\", Type = typeof(OriginalCurrentpriceInfo))]
        [SwaggerResponse(500, Description = "�����api ���� ����")]
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
        ///      ����ഫ�L�����׸��
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [SwaggerResponse(200, Description = "����ഫ�L�����׸�� ���\", Type = typeof(List<CurrentpriceInfo>))]
        [SwaggerResponse(500, Description = "����ഫ�L�����׸�� ����")]
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
