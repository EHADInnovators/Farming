using Farming.Data;
using Farming.Model;
using System.Collections.Generic;

namespace Farming.Business
{
    /// <summary>
    /// Farming assistance business class contains all business logic.
    /// </summary>
    public class FarmingBusiness : IFarmingBusiness
    {

        IFarmingData data;

        public FarmingBusiness()
        {
            data = new FarmingData();
        }

        public FarmingBusiness(IFarmingData farmingData)
        {
            data = farmingData;
        }

        /// <summary>
        /// To get crop categories
        /// </summary>
        /// <param name="userId">To get the categories based on the user location</param>
        /// <returns></returns>
        public List<CropCategories> GetCropCategories(int userId)
        {
            return data.GetCropCategories(userId);
        }

        /// <summary>
        /// To get sub categories.
        /// </summary>
        /// <param name="cropName">To select sub categories under this crop </param>
        /// <returns>Crop sub categories list</returns>
        public List<CropSubCategories> GetCropSubCategories(string cropName)
        {
            return data.GetSubCropCategories(cropName);
        }

        /// <summary>
        /// To get search options for the crop sub category.
        /// </summary>
        /// <param name="cropSubCategoryName">To select search options for this sub category</param>
        /// <returns>Search options</returns>
        public List<SearchOptions> GetSearchOptions(string cropSubCategoryName)
        {
            return data.GetSearchOptions(cropSubCategoryName);
        }

        /// <summary>
        /// To get symptoms for the passed option and sub category.
        /// </summary>
        /// <param name="option">To select symptom for this option</param>
        /// <param name="subCategory">To select symptom for this sub category</param>
        /// <returns>Symptoms</returns>
        public List<Symptom> GetSymptoms(string option, string subCategory)
        {
            return data.GetSymptoms(option, subCategory);
        }

        /// <summary>
        /// To get symptoms results for the passed symptom and sub category.
        /// </summary>
        /// <param name="symptom">To select symptom results for this symptom</param>
        /// <param name="subCategory">To select symptom results for this sub category</param>
        /// <returns>Symptom results</returns>
        public string GetSymptomsResults(string symptom, string subCategory)
        {
            return data.GetSymptomsResults(symptom, subCategory);
        }


        /// <summary>
        /// To update the user queries.
        /// </summary>
        /// <param name="name">user name who raised this query</param>
        /// <param name="email">user email</param>
        /// <param name="query">user query</param>
        /// <param name="attachmentURL">Attachments</param>
        /// <returns>True if successfully updated, else false if any failure</returns>
        public bool UpdateQuery(string name, string email, string query, string attachmentURL)
        {
            return data.UpdateQuery(name, email, query, attachmentURL);
        }


    }
}
