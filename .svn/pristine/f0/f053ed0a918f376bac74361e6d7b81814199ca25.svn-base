using System;
using System.Collections.Generic;
using System.Linq;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly DMSEntities _entities;

        public PromotionRepository()
        {
            _entities = new DMSEntities();
        }

        public object GetAllPromotions()
        {
            var data =
                _entities.promotion_master
                    .OrderByDescending(p => p.promotion_master_id)
                    .ToList();

            return data;
        }

        public object GetAllActivePromotions()
        {
            var data =
                _entities.promotion_master.Where(p => p.is_active == true)
                    .OrderByDescending(p => p.promotion_master_id)
                    .ToList();

            return data;
        }

        public object GetPromotionInformation(long productId, long promotionMasterId, int quantity)
        {
            try
            {
                var data =
                _entities.promotion_master.FirstOrDefault(
                    p =>
                        p.product_id == productId && p.promotion_master_id == promotionMasterId &&
                        p.lifting_quantity_for_promotion <= quantity);

                if (data.promotion_type == "Product")
                {
                    if (data != null)
                    {
                        var actualGiftQuantity = (int)(quantity / data.lifting_quantity_for_promotion);

                        var xxx = (from pd in _entities.promotion_details
                                   join pro in _entities.products on pd.product_id equals pro.product_id
                                   join cat in _entities.product_category on pd.product_category_id equals cat.product_category_id
                                   //added mohi uddin(04.04.2017)
                                   join u in _entities.units on pro.unit_id equals u.unit_id into Tempu from u in Tempu.DefaultIfEmpty()
                                   join b in _entities.brands on pro.brand_id equals b.brand_id into Tempb from b in Tempb.DefaultIfEmpty()
                                   select new
                                   {
                                       promotion_details_id = pd.promotion_details_id,
                                       promotion_master_id = pd.promotion_master_id,
                                       product_id = pro.product_id,
                                       product_name = pro.product_name,
                                       product_category_id = cat.product_category_id,
                                       product_category_name = cat.product_category_name,
                                       gift_quantity = pd.gift_quantity,
                                       has_serial = pro.has_serial,


                                       //04.04.2017
                                       unit_id=pro.unit_id,
                                       unit_name=u.unit_name,
                                       brand_id = pro.brand_id,
                                       brand_name = b.brand_name

                                   }).Where(p => p.promotion_master_id == promotionMasterId).ToList();

                               


                        var promotionDetailsList = new List<PromotionDetailsModel>();

                        foreach (var item in xxx)
                        {
                            var promotionDetails = new PromotionDetailsModel();
                            promotionDetails.promotion_details_id = item.promotion_details_id;
                            promotionDetails.promotion_master_id = item.promotion_master_id;
                            promotionDetails.product_category_id = item.product_category_id;
                            promotionDetails.product_category_name = item.product_category_name;
                            promotionDetails.product_id = item.product_id;
                            promotionDetails.product_name = item.product_name;
                            promotionDetails.gift_quantity = item.gift_quantity * actualGiftQuantity;
                            promotionDetails.has_serial = item.has_serial;


                             //04.04.2017
                            promotionDetails.unit_id = (int?) item.unit_id;
                            promotionDetails.unit_name = item.unit_name;
                            promotionDetails.brand_id = (int?)item.brand_id;
                            promotionDetails.brand_name = item.brand_name;

                            promotionDetailsList.Add(promotionDetails);
                            
                           
                        }

                        return promotionDetailsList;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (data.promotion_type == "Incentive")
                {
                    return data;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetPromotionInfoDropdown(long product_id, long channel_id)
        {
            var toDate = DateTime.Now.Date;

            var data = (from promo in _entities.promotion_master
                        join pcm in _entities.promotion_channel_mapping on promo.promotion_master_id equals pcm.promotion_master_id
                        select new
                        {
                            promotion_master_id = promo.promotion_master_id,
                            promotion_name = promo.promotion_name,
                            promotion_code = promo.promotion_code,
                            promotion_start_date = promo.promotion_start_date,
                            promotion_end_date = promo.promotion_end_date,
                            is_active = promo.is_active,
                            product_id = promo.product_id,
                            channel_id = pcm.channel_id,
                            promotion_type = promo.promotion_type

                        }).Where(p => p.product_id == product_id && p.channel_id == channel_id && p.is_active == true && (p.promotion_start_date <= toDate && p.promotion_end_date >= toDate)).OrderByDescending(p => p.promotion_master_id).ToList();

            return data;

        }

        public long AddPromotion(PromotionModel promotionModel)
        {
            try
            {
                var promotionMaster = promotionModel.PromotionMasterData;
                var promotionDetailsList = promotionModel.PromotionDetailsList;
                var promotionChannelMappingList = promotionModel.PromotionChannelMappingList;

                long promotionSerial = _entities.promotion_master.Max(po => (long?)po.promotion_master_id) ?? 0;
                promotionSerial++;
                var promotionStr = promotionSerial.ToString().PadLeft(7, '0');

                string promotionNo = "PROMO-" + promotionStr;
                promotionMaster.promotion_code = promotionNo;
                promotionMaster.promotion_name = promotionModel.PromotionMasterData.promotion_name;
                promotionMaster.promotion_start_date = promotionModel.PromotionMasterData.promotion_start_date;
                promotionMaster.promotion_end_date = promotionModel.PromotionMasterData.promotion_end_date;
                promotionMaster.product_category_id = promotionModel.PromotionMasterData.product_category_id;
                promotionMaster.product_id = promotionModel.PromotionMasterData.product_id;
                promotionMaster.lifting_quantity_for_promotion = promotionModel.PromotionMasterData.lifting_quantity_for_promotion;
                promotionMaster.promotion_type = promotionModel.PromotionMasterData.promotion_type;
                promotionMaster.promotion_discount = promotionModel.PromotionMasterData.promotion_discount;
                promotionMaster.is_discount_percent = promotionModel.PromotionMasterData.is_discount_percent;
                promotionMaster.remarks = promotionModel.PromotionMasterData.remarks;
                promotionMaster.is_active = promotionModel.PromotionMasterData.is_active;
                promotionMaster.created_by = promotionModel.PromotionMasterData.created_by;
                promotionMaster.created_date = DateTime.Now;
                promotionMaster.updated_by = promotionModel.PromotionMasterData.updated_by;
                promotionMaster.updated_date = DateTime.Now;

                _entities.promotion_master.Add(promotionMaster);
                _entities.SaveChanges();

                long promotionMasterId = promotionMaster.promotion_master_id;

                if (promotionDetailsList.Count > 0)
                {
                    foreach (var item in promotionDetailsList)
                    {
                        var promotionDetails = new promotion_details
                        {
                            promotion_master_id = promotionMasterId,
                            product_id = item.product_id,
                            product_category_id = item.product_category_id,
                            gift_quantity = item.gift_quantity
                        };

                        _entities.promotion_details.Add(promotionDetails);
                        _entities.SaveChanges();
                    }
                }


                if (promotionChannelMappingList.Count > 0)
                {
                    foreach (var item in promotionChannelMappingList)
                    {
                        {
                            var promotionChannelMapping = new promotion_channel_mapping
                            {
                                promotion_master_id = promotionMasterId,
                                channel_id = item.channel_id,
                                is_active = true
                            };
                            _entities.promotion_channel_mapping.Add(promotionChannelMapping);
                            _entities.SaveChanges();
                        }
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public object GetPromotinByChannelId(long channelId)
        {
            try
            {
                var toDate = DateTime.Now.Date;
                var data = (from promo in _entities.promotion_master
                            join pcm in _entities.promotion_channel_mapping on promo.promotion_master_id equals pcm.promotion_master_id
                            select new
                            {
                                promotion_master_id = promo.promotion_master_id,
                                promotion_name = promo.promotion_name,
                                promotion_code = promo.promotion_code,
                                promotion_start_date = promo.promotion_start_date,
                                promotion_end_date = promo.promotion_end_date,
                                is_active = promo.is_active,
                                product_id = promo.product_id,
                                channel_id = pcm.channel_id

                            }).Where(p => p.channel_id == channelId && p.is_active == true && (p.promotion_start_date <= toDate && p.promotion_end_date >= toDate)).OrderByDescending(p => p.promotion_master_id).ToList();

                return data;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public object GetPromotinByPromotionId(long proId)
        {
            try
            {
                return _entities.promotion_master.SingleOrDefault(a => a.promotion_master_id == proId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get promotion setting by promotion master id 
        /// </summary>
        /// <param name="promotionMasterId"></param>
        /// <returns>returns promotion model that includes promotion master, promotion details and promotion channels for</returns>
        public PromotionModel GetPromotionSetup(long promotionMasterId)
        {
            try
            {
                var promotionModel = new PromotionModel();
                // PROMOTION MASTER
                promotionModel.PromotionMasterData = _entities.promotion_master.Find(promotionMasterId);
                
                // PROMOTION DETAILS
                var promotionDetailsList = _entities.promotion_details
                    .Join(_entities.products, jp => jp.product_id, p => p.product_id, (jp, p) => new { jp, p })
                    .Join(_entities.product_category,jcat=>jcat.jp.product_category_id,cat=>cat.product_category_id,(jcat,cat)=>new {jcat,cat})
                    .Where(w => w.jcat.jp.promotion_master_id == promotionMasterId)
                    .OrderBy(o => o.jcat.jp.promotion_details_id)
                    .Select(i=>new PromotionDetailsModel
                    {
                        promotion_details_id =i.jcat.jp.promotion_details_id,
                        promotion_master_id = i.jcat.jp.promotion_master_id,
                        product_category_id = i.jcat.p.product_category_id,
                        product_id =i.jcat.jp.product_id,
                        gift_quantity =i.jcat.jp.gift_quantity,
                        product_category_name =i.cat.product_category_name,
                        product_name =i.jcat.p.product_name,
                        has_serial =i.jcat.p.has_serial
                    }).ToList();
                promotionModel.PromotionDetailsList = promotionDetailsList;
                
                // PROMOTION CHANNEL MAPPING
                var promotionChannelList = _entities.promotion_channel_mapping
                    .Join(_entities.party_type, jc => jc.channel_id, c => c.party_type_id, (jc, c) => new {jc, c})
                    .Where(w => w.jc.promotion_master_id == promotionMasterId)
                    .OrderBy(o => o.c.party_type_id)
                    .Select(i => new PromotionChannelMappingModel
                    {
                        promotion_channel_mapping_id = i.jc.promotion_channel_mapping_id,
                        promotion_master_id = i.jc.promotion_master_id,
                        channel_id = i.jc.channel_id,
                        is_active =i.jc.is_active,
                        channel_name =i.c.party_type_name
                    }).ToList();
                promotionModel.PromotionChannelMappingList = promotionChannelList;
                return promotionModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long EditPromotion(PromotionModel promotionModel)
        {
            try
            {
                var promotionMaster = promotionModel.PromotionMasterData;
                var promotionDetailsList = promotionModel.PromotionDetailsList;
                var promotionChannelMappingList = promotionModel.PromotionChannelMappingList;
                var promotionMasterId = promotionMaster.promotion_master_id;

                // UPDATE PROMOTION MASTER DATA
                var promotionMasterOld = _entities.promotion_master.Find(promotionMasterId);
                promotionMasterOld.promotion_name = promotionModel.PromotionMasterData.promotion_name;
                promotionMasterOld.promotion_start_date = promotionModel.PromotionMasterData.promotion_start_date;
                promotionMasterOld.promotion_end_date = promotionModel.PromotionMasterData.promotion_end_date;
                promotionMasterOld.product_category_id = promotionModel.PromotionMasterData.product_category_id;
                promotionMasterOld.product_id = promotionModel.PromotionMasterData.product_id;
                promotionMasterOld.lifting_quantity_for_promotion = promotionModel.PromotionMasterData.lifting_quantity_for_promotion;
                promotionMasterOld.promotion_discount = promotionModel.PromotionMasterData.promotion_discount;
                promotionMasterOld.is_discount_percent = promotionModel.PromotionMasterData.is_discount_percent;
                promotionMasterOld.remarks = promotionModel.PromotionMasterData.remarks;
                promotionMasterOld.is_active = promotionModel.PromotionMasterData.is_active;
                promotionMasterOld.updated_by = promotionModel.PromotionMasterData.updated_by;
                promotionMasterOld.updated_date = DateTime.Now;
                _entities.SaveChanges();

                //UPDATE PROMOTION DETAILS DATA
                if (promotionDetailsList!=null &&  promotionDetailsList.Count > 0)
                {
                    foreach (var item in promotionDetailsList)
                    {
                        var promotionDetailsOld = _entities.promotion_details
                            .FirstOrDefault(pd =>pd.promotion_master_id == promotionMasterId && pd.promotion_details_id == item.promotion_details_id);
                        if (promotionDetailsOld != null)
                        {
                            promotionDetailsOld.product_id = item.product_id;
                            promotionDetailsOld.product_category_id = item.product_category_id;
                            promotionDetailsOld.gift_quantity = item.gift_quantity;
                            _entities.SaveChanges();
                        }
                        else
                        {
                            var promotionDetails = new promotion_details
                            {
                                promotion_master_id = promotionMasterId,
                                product_id = item.product_id,
                                product_category_id = item.product_category_id,
                                gift_quantity = item.gift_quantity
                            };
                            _entities.promotion_details.Add(promotionDetails);
                            _entities.SaveChanges();
                        }
                        
                    }
                }


                if (promotionChannelMappingList.Count > 0)
                {
                    foreach (var item in promotionChannelMappingList)
                    {
                        var promotionChannelOld = _entities.promotion_channel_mapping.Find(item.promotion_channel_mapping_id);
                        if (promotionChannelOld != null)
                        {
                            promotionChannelOld.is_active = item.is_active;
                            _entities.SaveChanges();
                        }
                        else
                        {
                            var promotionChannelMapping = new promotion_channel_mapping
                            {
                                promotion_master_id = promotionMasterId,
                                channel_id = item.channel_id,
                                is_active = item.is_active
                            };
                            _entities.promotion_channel_mapping.Add(promotionChannelMapping);
                            _entities.SaveChanges();
                        }
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public object DeletePromotionDetails(long promotionDetailsId, long? promotionMasterId)
        {
            try
            {
                var promotionDetails = _entities.promotion_details
                    .FirstOrDefault(d => d.promotion_details_id == promotionDetailsId && d.promotion_master_id == promotionMasterId);
                _entities.promotion_details.Attach(promotionDetails);
                _entities.promotion_details.Remove(promotionDetails);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}