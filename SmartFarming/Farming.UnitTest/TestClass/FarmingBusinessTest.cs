using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farming.Business;
using Farming.Data;
using Farming.UnitTest.MockupClassess;
using System.Linq;

namespace Farming.UnitTest.TestClass
{
    [TestClass]
    public class FarmingBusinessTest
    {
        /// <summary>
        /// To test crop categories in business class
        /// </summary>
        [TestMethod]
        public void GetCropCategoriesTest()
        {
            IFarmingData data = new FarmingDataTest();
            FarmingBusiness farmingBusiness = new FarmingBusiness(data);
            var categoryList = farmingBusiness.GetCropCategories(1);
            Assert.AreEqual("Fruits", categoryList.First().CategoryName);

        }

        /// <summary>
        /// To test Get sub categories in business class
        /// </summary>
        [TestMethod]
        public void GetSubCropCategoriesTest()
        {
            IFarmingData data = new FarmingDataTest();
            FarmingBusiness farmingBusiness = new FarmingBusiness(data);
            var categoryList = farmingBusiness.GetCropSubCategories("Fruits");
            Assert.AreEqual("Banana", categoryList.First().SubCategoryName);

        }


        /// <summary>
        /// To test search options in business class
        /// </summary>
        [TestMethod]
        public void GetSearchOptionsTest()
        {
            IFarmingData data = new FarmingDataTest();
            FarmingBusiness farmingBusiness = new FarmingBusiness(data);
            var categoryList = farmingBusiness.GetSearchOptions("Test option");
            Assert.AreEqual("Test option", categoryList.First().OptionDescription);

        }


        /// <summary>
        /// To test get symptoms method in business class
        /// </summary>
        [TestMethod]
        public void GetSymptomsTest()
        {
            IFarmingData data = new FarmingDataTest();
            FarmingBusiness farmingBusiness = new FarmingBusiness(data);
            var categoryList = farmingBusiness.GetSymptoms("Test option", "Banana");
            Assert.AreEqual("Dark Green in leaves", categoryList.First().SymptomDescription);

        }





    }
}
