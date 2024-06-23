using CoindeskExampleApiServer.Protocols.Services;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class FacadeSystem
        (ISubSystem sub1, ISubSystem sub2, ISubSystem sub3, ISubSystem sub4) : IFacadeSystem
    {
        private readonly ISubSystem _sub1 = sub1;
        private readonly ISubSystem _sub2 = sub2;
        private readonly ISubSystem _sub3 = sub3;
        private readonly ISubSystem _sub4 = sub4;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string MethodA()
        {
            return $"組合A = {_sub1.Output()}/{_sub3.Output()}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string MethodB()
        {
            return $"組合B = {_sub2.Output()}/{_sub4.Output()}";
        }
    }
}
