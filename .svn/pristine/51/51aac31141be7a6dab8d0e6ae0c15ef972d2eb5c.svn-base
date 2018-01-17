using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IBrandRepository
    {
        List<brand> GetAllBrands();
        bool CheckDuplicateBrands(string brand_name);
        long Addbrand(brand brand);
        brand GetbrandById(long brand_id);
        bool Editbrand(brand brand);
        bool Deletebrand(long brand_id);
    }
}
