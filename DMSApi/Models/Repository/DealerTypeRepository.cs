using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class DealerTypeRepository:IDealerTypeRepository
    {
         private DMSEntities _entities;

         public DealerTypeRepository()
        {
            this._entities = new DMSEntities();
        }

         public List<dealer_type> GetAllDealerType()
         {
             try
             {
                 List<dealer_type> dealerTypes = _entities.dealer_type.OrderBy(p => p.dealer_type_name).Where(p => p.is_deleted == false).ToList();
                 return dealerTypes;
             }
             catch (Exception)
             {

                 return null;
             }
         }

         public dealer_type GetDealerTypeByID(long dealer_type_id)
         {
             throw new NotImplementedException();
         }

         public bool InsertDealerType(dealer_type objDealerType, long created_by)
         {
             try
             {
                 _entities.dealer_type.Add(objDealerType);
                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception)
             {

                 return false;
                     
             }
         }

         public bool DeleteDealerType(long dealer_type_id, long updated_by)
         {
             try
             {
                 dealer_type objDealerType = _entities.dealer_type.FirstOrDefault(c => c.dealer_type_id == dealer_type_id);
                 _entities.dealer_type.Attach(objDealerType);
                 _entities.dealer_type.Remove(objDealerType);
                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception)
             {

                 return false;
             }
         }

         public bool UpdateDealerType(dealer_type objDealerType, long updated_by)
         {
             try
             {
                 dealer_type con = _entities.dealer_type.Find(objDealerType.dealer_type_id);
                 con.dealer_type_name = objDealerType.dealer_type_name;
                 con.dealer_type_prefix = objDealerType.dealer_type_prefix;
                 con.credit_limit = objDealerType.credit_limit;
                 con.updated_by = updated_by;
                 con.updated_date = DateTime.Now;
                 con.is_active = objDealerType.is_active;
                 con.is_deleted = objDealerType.is_deleted = false;

                 _entities.SaveChanges();
                 return true;
             }
             catch (Exception ex)
             {
                 return false;
             }
         }

         public bool CheckDuplicatDealerTypePrefix(string dealer_type_prefix)
         {
             try
             {
                 var checkDuplicateDealerTypePrefix = _entities.dealer_type.FirstOrDefault(c => c.dealer_type_prefix == dealer_type_prefix);

                 bool return_type = checkDuplicateDealerTypePrefix == null ? false : true;
                 return return_type;
             }
             catch (Exception)
             {

                 return false;
             }
         }

         public bool CheckDuplicatDealerTypeType(string dealer_type_name)
         {
             try
             {
                 var checkDuplicateDealerTypeType = _entities.dealer_type.FirstOrDefault(c => c.dealer_type_name == dealer_type_name);
                 bool return_type = checkDuplicateDealerTypeType == null ? false : true;
                 return return_type;
             }
             catch (Exception)
             {

                 return false;
             }
         }


         public decimal GetDealerTypeWiseCreditLimit(long dealer_type_id)
         {
             try
             {

                 var creditLimit = _entities.dealer_type.SingleOrDefault(p => p.dealer_type_id == dealer_type_id).credit_limit??0;
                 return creditLimit;
             }
             catch (Exception)
             {
                 
                 throw;
             }
         }
    }
}