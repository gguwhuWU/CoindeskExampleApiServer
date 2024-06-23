using CoindeskExampleApiServer.Models;
using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Protocols.Repositories;
using CoindeskExampleApiServer.Protocols.Services;
using System.Text.Json;

namespace CoindeskExampleApiServer.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="copilotRepository"></param>
    public class CopilotService(
        ILogger<CopilotService> logger,
        ICopilotRepository copilotRepository) : ICopilotService
    {
        private readonly ILogger<CopilotService> _logger = logger;
        private readonly ICopilotRepository _copilotRepository = copilotRepository;
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<Copilot>> GetAll()
        {
            var result = new ServiceResult<List<Copilot>>();

            try
            {
                result.TargetResult = [.. _copilotRepository.FindAll().OrderBy(x => x.Code)];
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "抓取幣別全部資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult<Copilot> Get(string code)
        {
            var result = new ServiceResult<Copilot>();

            try
            {
                result.TargetResult = _copilotRepository.Find(code);
                if (result.TargetResult != null)
                {
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "notfound";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "抓取幣別單筆資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult Insert(Copilot copilot)
        {
            var result = new ServiceResult();

            try
            {
                if (_copilotRepository.Exist(copilot.Code))
                {
                    result.Message = "已存在，無法新增";
                    return result;
                }

                _copilotRepository.Create(copilot);
                _copilotRepository.SaveChanges();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "寫入幣別單筆資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult Update(Copilot copilot)
        {
            var result = new ServiceResult();

            try
            {
                if (!_copilotRepository.Exist(copilot.Code))
                {
                    result.Message = "不存在，無法更新";
                    return result;
                }

                _copilotRepository.Update(copilot);
                _copilotRepository.SaveChanges();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "更新幣別單筆資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult Delete(string code)
        {
            var result = new ServiceResult();

            try
            {
                var dbCopilot = _copilotRepository.Find(code);
                if (dbCopilot == null)
                {
                    result.Message = "不存在，無法刪除";
                    return result;
                }

                _copilotRepository.Delete(dbCopilot);
                _copilotRepository.SaveChanges();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "刪除幣別單筆資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult DeleteAll()
        {
            var result = new ServiceResult();

            try
            {
                _copilotRepository.Delete(_copilotRepository.FindAll());
                _copilotRepository.SaveChanges();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "刪除幣別全部資料 失敗";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult InsertBaseInsertData()
        {
            var result = new ServiceResult();

            try
            {
                var jsonCopilots = ReadJsonFileToList<Copilot>("BaseInsertData.json") ?? throw new Exception("讀取幣json資料失敗");
                _copilotRepository.Create(jsonCopilots);
                _copilotRepository.SaveChanges();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                result.Message = "InsertBaseInsertData 失敗";
            }

            return result;
        }

        private static List<T>? ReadJsonFileToList<T>(string filePath)
        {
            using StreamReader reader = new(filePath);
            var jsonString = reader.ReadToEnd();
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
