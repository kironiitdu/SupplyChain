using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.crystal_models
{
    public class RequisitionCombinedModel
    {
        public List<RequisitionReportModel> RequisitionReportModels { get; set; }
        public List<RebateReportModel> RebateReportModels { get; set; }
    }
}