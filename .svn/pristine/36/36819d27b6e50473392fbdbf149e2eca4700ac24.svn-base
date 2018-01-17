using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{

    public class DesignationRepository : IDesignationRepository
    {

        private DMSEntities _entities;

        public DesignationRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<designation> GetAllDesignation()
        {
            var designations = _entities.designations.OrderBy(d=>d.sales_designation).ToList();
            return designations;
        }

        public designation GetDesignationById(long sales_designation_id)
        {
            var data = _entities.designations.Find(sales_designation_id);
            return data;
        }

        public object GetAllDesignationForGrid()
        {
            try
            {
                string gridQuery = "select t1.sales_designation_id, t1.sales_designation,t1.parent_designation_id, coalesce(t2.sales_designation,' ') as parent_designation_name ,t1.sales_type_id,st.sales_type,t1.sales_person_type_code from (select sales_designation_id, sales_designation,sales_type_id, parent_designation_id,sales_person_type_code from designation) t1 left join (select sales_designation_id , sales_designation from designation) t2 on t1.parent_designation_id=t2.sales_designation_id left join sales_type st on t1.sales_type_id=st.sales_type_id";
                
                var gridData = _entities.Database.SqlQuery<DesignationGridModel>(gridQuery).OrderBy(l => l.sales_designation).ToList().DefaultIfEmpty();
                return gridData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddDesignation(designation designation)
        {
            try
            {
                designation insertDesignation = new designation
                {
                    sales_designation = designation.sales_designation,
                    sales_person_type_code = designation.sales_person_type_code,
                    parent_designation_id = designation.parent_designation_id,
                    sales_type_id = designation.sales_type_id,
                    created_by = designation.created_by,
                    updated_by = designation.updated_by,
                    created_date = designation.created_date,
                    updated_date = designation.updated_date

                };
                _entities.designations.Add((insertDesignation));
                _entities.SaveChanges();

                long lastInsertId = insertDesignation.sales_designation_id;
                return lastInsertId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditDesignation(designation designation)
        {
            try
            {
                designation data = _entities.designations.Find(designation.sales_designation_id);
                data.sales_designation = designation.sales_designation;
                data.sales_person_type_code = designation.sales_person_type_code;
                data.parent_designation_id = designation.parent_designation_id;
                data.sales_type_id = designation.sales_type_id;
                data.updated_by = designation.updated_by;
                data.updated_date = designation.updated_date;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDesignation(long sales_designation_id)
        {
            try
            {
                designation oDesignation = _entities.designations.Find(sales_designation_id);
                _entities.designations.Remove(oDesignation);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckDuplicateDesignation(string sales_designation)
        {
            throw new NotImplementedException();
        }
    }
}