using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Models.DB;

namespace CoindeskExampleApiServer.Protocols.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICopilotService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<Copilot>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ServiceResult<Copilot> Get(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copilot"></param>
        /// <returns></returns>
        public ServiceResult Insert(Copilot copilot);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copilot"></param>
        /// <returns></returns>
        public ServiceResult Update(Copilot copilot);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ServiceResult Delete(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult DeleteAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult InsertBaseInsertData();
    }
}
