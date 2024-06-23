namespace CoindeskExampleApiServer.Protocols.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAesEncryption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptString(string plainText);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string DecryptString(string cipherText);
    }
}
