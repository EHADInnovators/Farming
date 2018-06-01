using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farming.Business;
using Farming.Model;
using System.Linq;

namespace FarmingBot.Services
{
    public class ServiceOperations : IUserData, IFarmingOperations
    {
        IFarmingBusiness farmingBusiness = new FarmingBusiness();

        public Task<IList<string>> GetCropCategories(int userId)
        {
            List<CropCategories> categoryList = new List<CropCategories>();
            categoryList=farmingBusiness.GetCropCategories(userId);
            return Task.FromResult<IList<string>>(categoryList.Select(category => category.CategoryName).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cropName"></param>
        /// <returns></returns>
        public Task<IList<string>> GetCropSubCategories(string cropName)
        {
            List<CropSubCategories> categoryList = new List<CropSubCategories>();
            categoryList = farmingBusiness.GetCropSubCategories(cropName);
            return Task.FromResult<IList<string>>(categoryList.Select(category => category.SubCategoryName).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cropName"></param>
        /// <returns></returns>
        public Task<IList<string>> GetSearchOptions(string cropSubCategoryName)
        {
            List<SearchOptions> searchOptions = new List<SearchOptions>();
            searchOptions = farmingBusiness.GetSearchOptions(cropSubCategoryName);
            return Task.FromResult<IList<string>>(searchOptions.Select(searchOption => searchOption.OptionDescription).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public Task<IList<string>> GetInfo(string option, string subCategory)
        {
            List<Symptom> symptoms = new List<Symptom>();
            symptoms = farmingBusiness.GetSymptoms(option, subCategory);
            return Task.FromResult<IList<string>>(symptoms.Select(symptom => symptom.SymptomDescription).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public string GetSymptomResults(string symptom, string subCategory)
        {
            string symptomsResolution = farmingBusiness.GetSymptomsResults(symptom, subCategory);
            return symptomsResolution;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserProfile> GetUserInformation(string name, string email)
        {
            await Task.Delay(1567);
            return new UserProfile { Id = 42, Name = name, EMail = email };
        }


        /// <summary>
        /// To update the user queries.
        /// </summary>
        /// <param name="name">user name who raised this query</param>
        /// <param name="email">user email</param>
        /// <param name="query">user query</param>
        /// <param name="attachmentURL">Attachments</param>
        /// <returns>True if successfully updated, else false if any failure</returns>
        public bool UpdateQueries(string name, string email, string query, string attachmentURL)
        {
            return farmingBusiness.UpdateQuery(name, email, query, attachmentURL);
        }
    }
}