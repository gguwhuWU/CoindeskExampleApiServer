using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Protocols.Repositories;

namespace CoindeskExampleApiServer.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public class CopilotRepository(CoindeskDbContext dbContext) : GenericRepository<Copilot>(dbContext), ICopilotRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exist(string code)
        {
            return _dbContext.Copilots.Any(x => x.Code == code);
        }
    }
}
