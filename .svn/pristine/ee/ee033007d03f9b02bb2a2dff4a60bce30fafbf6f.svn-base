using DMSApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.Repository
{
    public class RetailerTypeRepository : IRetailerTypeRepository
    {
          private DMSEntities _entities;

          public RetailerTypeRepository()
        {
            this._entities = new DMSEntities();
        }

          public List<retailer_type> GetAllRetailerType()
          {
              try
              {
                  List<retailer_type> retailerTypes = _entities.retailer_type.OrderBy(k => k.retailer_type_name).Where(k => k.is_deleted == false).ToList();
                  return retailerTypes;
              }
              catch (Exception)
              {

                  return null;
              }
          }

          public retailer_type GetRetailerTypeByID(long ratailer_type_id)
          {
              throw new NotImplementedException();
          }

          public bool InsertRetailerType(retailer_type objRetailerType, long created_by)
          {

              try
              {
                  _entities.retailer_type.Add(objRetailerType);
                  _entities.SaveChanges();
                  return true;
              }
              catch (Exception)
              {

                  return false;

              }
          }

          public bool DeleteRetailerType(long ratailer_type_id, long updated_by)
          {
              try
              {
                  retailer_type objRetailerType = _entities.retailer_type.FirstOrDefault(k => k.retailer_type_id == ratailer_type_id);
                  _entities.retailer_type.Attach(objRetailerType);
                  _entities.retailer_type.Remove(objRetailerType);
                  _entities.SaveChanges();
                  return true;
              }
              catch (Exception)
              {

                  return false;
              }
          }

          public bool UpdateRetailerType(retailer_type objRetailerType, long updated_by)
          {
              try
              {
                  retailer_type con = _entities.retailer_type.Find(objRetailerType.retailer_type_id);
                  con.retailer_type_name = objRetailerType.retailer_type_name;
                  con.retailer_type_prefix = objRetailerType.retailer_type_prefix;
                  con.credit_limit = objRetailerType.credit_limit;
                  con.updated_by = updated_by;
                  con.updated_date = DateTime.Now;
                  con.is_active = objRetailerType.is_active;
                  con.is_deleted = objRetailerType.is_deleted = false;
                  _entities.SaveChanges();
                  return true;
              }
              catch (Exception ex)
              {
                  return false;
              }
          }

          public bool CheckDuplicatRetailerTypePrefix(string ratailer_type_prefix)
          {
              try
              {
                  var checkDuplicateRetailerTypePrefix = _entities.retailer_type.FirstOrDefault(k => k.retailer_type_prefix == ratailer_type_prefix);

                  bool return_type = checkDuplicateRetailerTypePrefix == null ? false : true;
                  return return_type;
              }
              catch (Exception)
              {

                  return false;
              }
          }

          public bool CheckDuplicatRetailerTypeType(string ratailer_type_name)
          {
              try
              {
                  var checkDuplicateRetailerType = _entities.retailer_type.FirstOrDefault(k => k.retailer_type_name == ratailer_type_name);
                  bool return_type = checkDuplicateRetailerType == null ? false : true;
                  return return_type;
              }
              catch (Exception)
              {

                  return false;
              }
          }
    }
}