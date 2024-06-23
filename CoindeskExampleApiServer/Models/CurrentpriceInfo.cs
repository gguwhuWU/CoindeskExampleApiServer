namespace CoindeskExampleApiServer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentpriceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>

        public decimal Rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CodeDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string? UpdateDateTime { get; set; }
    }
}
