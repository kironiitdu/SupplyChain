using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface ITermsAndConditionRepository
    {
        object GetAllTermsAndCondition();
        terms_and_condition GetTermsAndConditionByheader(string terms_condition_header);
        bool AddTermsAndCondition(terms_and_condition objTermsAndCondition);
        terms_and_condition GetTermsAndConditionById(long terms_and_condition_id);
        bool EditTermsAndCondition(terms_and_condition termsAndCondition);
        bool DeleteTermsAndCondition(long terms_and_condition_id);
        bool CheckDuplicatetermsAndCondition(string terms_and_condition_description);
    }
}
