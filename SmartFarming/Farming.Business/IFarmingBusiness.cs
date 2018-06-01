using Farming.Model;
using System.Collections.Generic;

namespace Farming.Business
{
    /// <summary>
    /// Farming assistance business interface contains all business logic.
    /// </summary>
    public interface IFarmingBusiness
    {
        /// <summary>
        /// To get crop categories
        /// </summary>
        /// <param name="userId">To get the categories based on the user location</param>
        /// <returns></returns>
        List<CropCategories> GetCropCategories(int userId);

        /// <summary>
        /// To get sub categories.
        /// </summary>
        /// <param name="cropName">To select sub categories under this crop </param>
        /// <returns>Crop sub categories list</returns>
        List<CropSubCategories> GetCropSubCategories(string cropName);

        /// <summary>
        /// To get search options for the crop sub category.
        /// </summary>
        /// <param name="cropSubCategoryName">To select search options for this sub category</param>
        /// <returns>Search options</returns>
        List<SearchOptions> GetSearchOptions(string cropSubCategoryName);

        /// <summary>
        /// To get symptoms for the passed option and sub category.
        /// </summary>
        /// <param name="option">To select symptom for this option</param>
        /// <param name="subCategory">To select symptom for this sub category</param>
        /// <returns>Symptoms</returns>
        List<Symptom> GetSymptoms(string option, string subCategory);

        /// <summary>
        /// To get symptoms results for the passed symptom and sub category.
        /// </summary>
        /// <param name="symptom">To select symptom results for this symptom</param>
        /// <param name="subCategory">To select symptom results for this sub category</param>
        /// <returns>Symptom results</returns>
        string GetSymptomsResults(string symptom, string subCategory);


        /// <summary>
        /// To update the user queries.
        /// </summary>
        /// <param name="name">user name who raised this query</param>
        /// <param name="email">user email</param>
        /// <param name="query">user query</param>
        /// <param name="attachmentURL">Attachments</param>
        /// <returns>True if successfully updated, else false if any failure</returns>
        bool UpdateQuery(string name, string email, string query, string attachmentURL);


    }
}
