using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmingBot.Services
{
    public interface IFarmingOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetCropCategories(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cropName"></param>
        /// <returns></returns>
        Task<IList<string>> GetCropSubCategories(string cropName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cropName"></param>
        /// <returns></returns>
        Task<IList<string>> GetSearchOptions(string cropName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchOption"></param>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        Task<IList<string>> GetInfo(string searchOption, string subCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symptom"></param>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        string GetSymptomResults(string symptom, string subCategory);

        /// <summary>
        /// To update the user queries.
        /// </summary>
        /// <param name="name">user name who raised this query</param>
        /// <param name="email">user email</param>
        /// <param name="query">user query</param>
        /// <param name="attachmentURL">Attachments</param>
        /// <returns>True if successfully updated, else false if any failure</returns>
        bool UpdateQueries(string name, string email, string query, string attachmentURL);
    }
}
