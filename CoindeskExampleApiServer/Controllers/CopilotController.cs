using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace CoindeskExampleApiServer.Controllers
{
    /// <summary>
    ///     幣別
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CopilotController(
        ICopilotService copilotService,
        IStringLocalizer<SharedMessageResource> localizer) : ControllerBase
    {
        private readonly ICopilotService _copilotService = copilotService;
        private readonly IStringLocalizer<SharedMessageResource> _localizer = localizer;

        /// <summary>
        ///     測試讀取本地語言
        /// </summary>
        /// <returns></returns>
        [HttpGet("localized-message")]
        [SwaggerResponse(200, Description = "讀取本地語言 成功", Type = typeof(string))]
        public IActionResult GetLocalizedMessage()
        {
            return Ok(_localizer["HelloWorld"]);
        }

        /// <summary>
        ///     讀取全部幣別
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [SwaggerResponse(200, Description = "讀取全部 成功", Type = typeof(List<Copilot>))]
        [SwaggerResponse(500, Description = "讀取全部 失敗")]
        public IActionResult GetAll()
        {
            var result = _copilotService.GetAll();
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return Ok(result.TargetResult);
        }

        /// <summary>
        ///     讀取單筆幣別
        /// </summary>
        /// <returns></returns>
        [HttpGet("{code}")]
        [SwaggerResponse(200, Description = "讀取單筆 成功", Type = typeof(Copilot))]
        [SwaggerResponse(404, Description = "找不到此單筆資料")]
        [SwaggerResponse(500, Description = "讀取單筆 失敗")]
        public IActionResult Get(string code)
        {
            var result = _copilotService.Get(code);
            if (!result.IsSuccess)
            {
                if (result.Message == "notfound")
                {
                    return NotFound();
                }

                return StatusCode(500, result.Message);
            }

            return Ok(result.TargetResult);
        }

        /// <summary>
        ///     寫入單筆幣別
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        [SwaggerResponse(201, Description = "寫入單筆 成功")]
        [SwaggerResponse(500, Description = "寫入單筆 失敗")]
        public IActionResult Insert(Copilot copilot)
        {
            var result = _copilotService.Insert(copilot);
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return StatusCode(201);
        }

        /// <summary>
        ///     更新單筆幣別
        /// </summary>
        /// <returns></returns>
        [HttpPut("")]
        [SwaggerResponse(204, Description = "更新單筆 成功")]
        [SwaggerResponse(500, Description = "更新單筆 失敗")]
        public IActionResult Update(Copilot copilot)
        {
            var result = _copilotService.Update(copilot);
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        ///     刪除單筆幣別
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{code}")]
        [SwaggerResponse(204, Description = "刪除單筆 成功")]
        [SwaggerResponse(404, Description = "找不到此單筆資料")]
        [SwaggerResponse(500, Description = "刪除單筆 失敗")]
        public IActionResult Delete(string code)
        {
            var result = _copilotService.Delete(code);
            if (!result.IsSuccess)
            {
                if (result.Message == "不存在，無法刪除")
                {
                    return NotFound();
                }

                return StatusCode(500, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        ///     刪除全部幣別
        /// </summary>
        /// <returns></returns>
        [HttpDelete("")]
        [SwaggerResponse(204, Description = "刪除全部 成功")]
        [SwaggerResponse(500, Description = "刪除全部 失敗")]
        public IActionResult DeleteAll()
        {
            var result = _copilotService.DeleteAll();
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return NoContent();
        }

        /// <summary>
        ///     寫入基本json資料 幣別 清單
        /// </summary>
        /// <returns></returns>
        [HttpPost("BaseInsertList")]
        [SwaggerResponse(204, Description = "寫入基本 成功")]
        [SwaggerResponse(500, Description = "寫入基本 失敗")]
        public IActionResult InsertBaseInsertData()
        {
            var result = _copilotService.InsertBaseInsertData();
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.Message);
            }

            return NoContent();
        }
    }
}
