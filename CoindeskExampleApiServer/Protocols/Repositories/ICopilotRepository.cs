using CoindeskExampleApiServer.Models.DB;

namespace CoindeskExampleApiServer.Protocols.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICopilotRepository : IGenericRepository<Copilot>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exist(string code);
    }
}
