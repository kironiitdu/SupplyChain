using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class TermsAndConditionRepository : ITermsAndConditionRepository
    {
        private DMSEntities _entities;

        public TermsAndConditionRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllTermsAndCondition()
        {
            List<terms_and_condition> objTermsAndConditions = _entities.terms_and_condition.OrderBy(k => k.terms_condition_header).ToList();
            return objTermsAndConditions;
        }

        public terms_and_condition GetTermsAndConditionByheader(string terms_condition_header)
        {
            var data = _entities.terms_and_condition.FirstOrDefault(t=>t.terms_condition_header==terms_condition_header && t.is_active==true);
            return data;
        }

        public bool AddTermsAndCondition(terms_and_condition objTermsAndCondition)
        {
            try
            {

                _entities.terms_and_condition.Add(objTermsAndCondition);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public terms_and_condition GetTermsAndConditionById(long terms_and_condition_id)
        {
            var terms = _entities.terms_and_condition.Find(terms_and_condition_id);
            return terms;
        }

        public bool EditTermsAndCondition(terms_and_condition termsAndCondition)
        {
            try
            {
                terms_and_condition entity = _entities.terms_and_condition.Find(termsAndCondition.terms_and_condition_id);
                entity.terms_condition_header = termsAndCondition.terms_condition_header;
                entity.terms_and_condition_description = termsAndCondition.terms_and_condition_description;
                entity.is_active = termsAndCondition.is_active;
                entity.updated_by = termsAndCondition.updated_by;
                entity.update_date = DateTime.Now;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTermsAndCondition(long terms_and_condition_id)
        {
            try
            {
                terms_and_condition objTermsAndCondition = _entities.terms_and_condition.FirstOrDefault(kk => kk.terms_and_condition_id == terms_and_condition_id);
                _entities.terms_and_condition.Remove(objTermsAndCondition);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckDuplicatetermsAndCondition(string terms_and_condition_description)
        {
            var checkDuplicate = _entities.terms_and_condition.FirstOrDefault(b => b.terms_and_condition_description == terms_and_condition_description);
            bool return_type = checkDuplicate == null ? false : true;
            return return_type;
        }
    }
}