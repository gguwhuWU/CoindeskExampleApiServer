using CoindeskExampleApiServer.Models;

namespace CoindeskExampleApiServer.Protocols.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICoindeskApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ServiceResult<OriginalCurrentpriceInfo>> GetBPICurrentprice();
    }
}
