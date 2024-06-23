using CoindeskExampleApiServer.Protocols.Services;

namespace CoindeskExampleApiServer.Services.SubSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class SubSystem1 : ISubSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            return "子系統1-功能";
        }
    }
}
