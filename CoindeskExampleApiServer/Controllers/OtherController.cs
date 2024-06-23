using CoindeskExampleApiServer.Protocols.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoindeskExampleApiServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="aesEncryption"></param>
    /// <param name="facadeSystem"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController
        (IAesEncryption aesEncryption,
        IFacadeSystem facadeSystem) : ControllerBase
    {
        private readonly IAesEncryption _aesEncryption = aesEncryption;
        private readonly IFacadeSystem _facadeSystem = facadeSystem;

        /// <summary>
        ///     Facade模式 B組合
        /// </summary>
        /// <returns></returns>
        [HttpGet("Facade/A")]
        [SwaggerResponse(200, Description = "Facade模式 A組合 成功", Type = typeof(string))]
        public IActionResult GetFacadeSystemMethodA()
        {
            return Ok(_facadeSystem.MethodA());
        }

        /// <summary>
        ///     Facade模式 B組合
        /// </summary>
        /// <returns></returns>
        [HttpGet("Facade/B")]
        [SwaggerResponse(200, Description = "Facade模式 B組合 成功", Type = typeof(string))]
        public IActionResult GetFacadeSystemMethodB()
        {
            return Ok(_facadeSystem.MethodB());
        }

        /// <summary>
        ///     加密字串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet("Encrypt")]
        [SwaggerResponse(200, Description = "加密字串 成功", Type = typeof(string))]
        public IActionResult EncryptString(string s)
        {
            return Ok(_aesEncryption.EncryptString(s));
        }

        /// <summary>
        ///     解密字串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet("Decrypt")]
        [SwaggerResponse(200, Description = "解密字串 成功", Type = typeof(string))]
        public IActionResult DecryptString(string s)
        {
            return Ok(_aesEncryption.DecryptString(s));
        }
    }
}
