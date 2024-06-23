namespace CoindeskExampleApiServer.Models
{
    /// <summary>
    ///     回傳結果
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     回傳訊息
        /// </summary>
        public string? Message { get; set; }
    }

    /// <summary>
    ///     Service回傳結果，有帶物件
    /// </summary>
    /// <typeparam name="T">Service傳回物件</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        ///     Service傳回物件
        /// </summary>
        public T? TargetResult { get; set; }
    }
}
