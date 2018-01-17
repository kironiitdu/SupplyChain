using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.IRepository
{
    public interface IProductColorMappingRepository
    {
        object GetAllColorMapping();
        object GetAllColorMappingForTransection();
        object GetProductwiseColor();
        bool AddColorMapping(product_color_mapping productColor,long create_by);
        product_color_mapping GetColorMappingById(long mappingId);
        bool EditColorMapping(product_color_mapping productColor,long update_by);
        bool DeleteColorMapping(long mappingId);
        bool CheckDuplicateMapping(product_color_mapping productColor);
        bool CheckDuplicateMappingForUpdate(product_color_mapping productColor);
        //List<area> GetRegionwiseAreaForDropdown(long region_id);

        object GetColorByProductId(long productId);
    }
}