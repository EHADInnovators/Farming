using Farming.Model;
using System.Collections.Generic;
using System.Linq;

namespace Farming.Data
{
    /// <summary>
    /// Farming assistance data class contains all database operations logic.
    /// </summary>
    public class FarmingData : IFarmingData
    {

        /// <summary>
        /// To get crop categories.
        /// </summary>
        /// <param name="userId">To get the categories based on the user location</param>
        /// <returns>Farm categories</returns>
        public List<CropCategories> GetCropCategories(int userId)
        {
            List<CropCategories> cropCategories = new List<CropCategories>();
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<MASTER_FARM> dbCategories = sqlDBVirutualFormAssistEntities.MASTER_FARM.ToList();
                dbCategories.ToList().ForEach(dbCategory => cropCategories.Add(new CropCategories { CategoryID = dbCategory.MASTER_FARM_ID, CategoryName = dbCategory.MASTER_FARM_NAME }));

            }
            return cropCategories;
        }

        /// <summary>
        /// To get search options for the crop sub category.
        /// </summary>
        /// <param name="cropSubCategoryName">To select search options for this sub category</param>
        /// <returns>Search options</returns>
        public List<SearchOptions> GetSearchOptions(string cropSubCategoryName)
        {
            List<SearchOptions> searchOptions = new List<SearchOptions>();
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<SEARCH_OPTIONS> dbSearchOptions = sqlDBVirutualFormAssistEntities.SEARCH_OPTIONS.Where(searchOption => searchOption.CHILD_FARM.CHILD_FARM_NAME == cropSubCategoryName).ToList();
                dbSearchOptions.ToList().ForEach(dbSearchOption => searchOptions.Add(new SearchOptions { OptionId = dbSearchOption.SEARCH_OPTIONS_ID, OptionDescription = dbSearchOption.OPTIONS }));

            }
            return searchOptions;
        }


        /// <summary>
        /// To get symptoms for the passed option and sub category.
        /// </summary>
        /// <param name="option">To select symptom for this option</param>
        /// <param name="subCategory">To select symptom for this sub category</param>
        /// <returns>Symptoms</returns>
        public List<Symptom> GetSymptoms(string option, string subCategory)
        {
            List<Symptom> symptoms = new List<Symptom>();
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<SYMPTOM> dbCategories = sqlDBVirutualFormAssistEntities.SYMPTOMS.Where(symptom => symptom.SEARCH_OPTIONS.CHILD_FARM.CHILD_FARM_NAME == subCategory && symptom.SEARCH_OPTIONS.OPTIONS == option).ToList();
                dbCategories.ToList().ForEach(dbCategory => symptoms.Add(new Symptom { SymptomId = dbCategory.SYMPTOMS_ID, SymptomDescription = dbCategory.SYMPTOMS_DESC }));

            }
            return symptoms;
        }


        /// <summary>
        /// To get symptoms results for the passed symptom and sub category.
        /// </summary>
        /// <param name="symptom">To select symptom results for this symptom</param>
        /// <param name="subCategory">To select symptom results for this sub category</param>
        /// <returns>Symptom results</returns>
        public string GetSymptomsResults(string symptom, string subCategory)
        {
            string symptoms = string.Empty;
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<SYMPTOM_RESULTS> dbCategories = sqlDBVirutualFormAssistEntities.SYMPTOM_RESULTS.Where(symptomResult => symptomResult.SYMPTOM.SYMPTOMS_DESC == symptom
                                                        && symptomResult.SYMPTOM.SEARCH_OPTIONS.CHILD_FARM.CHILD_FARM_NAME == subCategory).ToList();
                //var dbSymptom = dbCategories.FirstOrDefault(dbCategory => dbCategory.SYMPTOMS_DESC == symptom);

                if(dbCategories != null)
                {
                    symptoms = dbCategories.FirstOrDefault().SYMPTOMS_DESC;
                }

            }
            return symptoms;
        }


        /// <summary>
        /// To get sub categories.
        /// </summary>
        /// <param name="cropName">To select sub categories under this crop </param>
        /// <returns>Crop sub categories list</returns>
        public List<CropSubCategories> GetSubCropCategories(string cropName)
        {
            List<CropSubCategories> cropSubCategories = new List<CropSubCategories>();
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<CHILD_FARM> dbCategories = sqlDBVirutualFormAssistEntities.CHILD_FARM.Where(childFarm => childFarm.MASTER_FARM.MASTER_FARM_NAME == cropName).ToList();
                dbCategories.ToList().ForEach(dbCategory => cropSubCategories.Add(new CropSubCategories { SubCategoryID = dbCategory.CHILD_FARM_ID, SubCategoryName = dbCategory.CHILD_FARM_NAME }));

            }
            return cropSubCategories;
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
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                var userQuery = sqlDBVirutualFormAssistEntities.USER_QUERIES.Add(new USER_QUERIES { USER_NAME = name, USER_EMAIL = email, USER_QUERY = query, ATTACHMENT = attachmentURL });
                sqlDBVirutualFormAssistEntities.SaveChanges();

                //if inserted successfully query id will get retrieved from db.
                return userQuery.USER_QUERY_ID > 0;
            }
        }
    }
}
