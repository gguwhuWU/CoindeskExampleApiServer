namespace CoindeskExampleApiServer.Models
{
#pragma warning disable IDE1006 // 命名樣式
    /// <summary>
    /// 
    /// </summary>
    public class OriginalCurrentpriceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Time? time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? disclaimer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? chartName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BPI? bpi { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Time
    {
        /// <summary>
        /// 
        /// </summary>
        public string? updated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? updatedISO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? updateduk { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BPI
    {
        /// <summary>
        /// 
        /// </summary>
        public BPIInfo? USD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BPIInfo? GBP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BPIInfo? EUR { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BPIInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? symbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? rate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public decimal rate_float { get; set; }
    }
#pragma warning restore IDE1006 // 命名樣式
}
