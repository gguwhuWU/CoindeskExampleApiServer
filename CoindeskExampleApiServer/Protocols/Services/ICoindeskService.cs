using CoindeskExampleApiServer.Models;

namespace CoindeskExampleApiServer.Protocols.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICoindeskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ServiceResult<List<CurrentpriceInfo>>> GetAllBPICurrentprice();
    }
}
