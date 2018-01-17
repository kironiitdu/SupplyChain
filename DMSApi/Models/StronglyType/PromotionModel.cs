using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMSApi.Models.StronglyType
{
    public class PromotionModel
    {
        public promotion_master PromotionMasterData { get; set; }
        public List<PromotionDetailsModel> PromotionDetailsList { get; set; }
        public List<PromotionChannelMappingModel> PromotionChannelMappingList { get; set; }
    }
}