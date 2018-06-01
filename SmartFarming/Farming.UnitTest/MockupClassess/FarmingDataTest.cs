using Farming.Data;
using System.Collections.Generic;
using Farming.Model;

namespace Farming.UnitTest.MockupClassess
{
    public class FarmingDataTest : IFarmingData
    {
        public List<CropCategories> GetCropCategories(int userId)
        {
            List<CropCategories> categories = new List<CropCategories>();
            categories.Add(new CropCategories { CategoryID = 1, CategoryName = "Fruits" });
            categories.Add(new CropCategories { CategoryID = 2, CategoryName = "Vegitables" });
            return categories;
        }

        public List<SearchOptions> GetSearchOptions(string cropSubCategoryName)
        {
            List<SearchOptions> searchOptions = new List<SearchOptions>();
            searchOptions.Add(new SearchOptions { OptionId=1, OptionDescription = "Test option" });
            return searchOptions;
        }

        public List<CropSubCategories> GetSubCropCategories(string cropName)
        {
            List<CropSubCategories> subCategories = new List<CropSubCategories>();
            subCategories.Add(new CropSubCategories { SubCategoryID= 1, SubCategoryName = "Banana" });
            subCategories.Add(new CropSubCategories { SubCategoryID = 2, SubCategoryName = "Apple" });
            return subCategories;
        }

        public List<Symptom> GetSymptoms(string option, string subCategory)
        {
            List<Symptom> symptoms = new List<Symptom>();
            symptoms.Add(new Symptom { SymptomId = 1, SymptomDescription = "Dark Green in leaves" });
            symptoms.Add(new Symptom { SymptomId = 1, SymptomDescription = "Black dots in banana" });
            return symptoms;
        }

        public string GetSymptomsResults(string symptom, string subCategory)
        {
            return "This disease can affect most parts of the plant including the trunk, branches, shoots, buds, flowers, leaves and fruit.";
        }

        public bool UpdateQuery(string name, string email, string query, string attachmentURL)
        {
            return true;
        }
    }
}
