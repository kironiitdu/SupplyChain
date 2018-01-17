using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
  public  interface IAccessoriesCategoryRepository
    {
        List<accessories_category> GetAllAccessoriesCategory();
        List<accessories_category> GetAllAccessoriesCategoryForGrid();
        bool CheckDuplicateAccessoriesCategory(string accessories_category_name);
        long AddAccessoriesCategory(accessories_category objAccessoriesCategory, long created_by);
        accessories_category GetAccessoriesCategoryById(long accessories_category_id);
        bool EditAccessoriesCategory(accessories_category objAccessoriesCategory, long updated_by);
        bool DeleteAccessoriesCategory(long accessories_category_id);
      
    }
}
