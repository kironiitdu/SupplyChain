using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IPromotionRepository
    {
        object GetAllPromotions();
        object GetAllActivePromotions();
        object GetPromotionInformation(long productId, long promotionMasterId, int quantity);
        object GetPromotionInfoDropdown(long product_id, long channel_id);
        long AddPromotion(PromotionModel promotionModel);

        object GetPromotinByChannelId(long channelId);
        object GetPromotinByPromotionId(long proId);

        PromotionModel GetPromotionSetup(long promotionMasterId);

        long EditPromotion(PromotionModel promotionModel);
        object DeletePromotionDetails(long promotionDetailsId, long? promotionMasterId);
    }
}
