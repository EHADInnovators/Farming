using Farming.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farming.Data
{
    public class FarmingData
    {
        public List<CropCategories> GetCropCategories(int userId)
        {
            List<CropCategories> cropCategories = new List<CropCategories>();
            using (SQLDBVirtualFarmerAssitEntities sqlDBVirutualFormAssistEntities = new SQLDBVirtualFarmerAssitEntities())
            {
                List<MASTER_FARM> dbCategories = sqlDBVirutualFormAssistEntities.MASTER_FARM.ToList();
                dbCategories.ToList().ForEach(dbCategory => cropCategories.Add(new CropCategories { CategoryID = dbCategory.MASTER_FARM_ID, CategoryName = dbCategory.MASTER_FARM_NAME }));

            }
            throw new NotImplementedException();
        }
    }


}
