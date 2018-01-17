using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.IRepository
{
    public interface IProductVersionMappingRepository
    {
        object GetAllVersionMapping();
        object GetProductwiseVersion();
        bool AddVersionMapping(product_version_mapping productVersion, long create_by);
        product_version_mapping GetVersionMappingById(long mappingId);
        bool EditVersionrMapping(product_version_mapping productColor, long update_by);
        bool DeleteVersionMapping(long mappingId);
        bool CheckDuplicateMapping(product_version_mapping productColor);
        bool CheckDuplicateMappingForUpdate(product_version_mapping productColor);
    }
}