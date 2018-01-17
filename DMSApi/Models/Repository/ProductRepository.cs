using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DMSEntities _entities;

        public ProductRepository()
        {
            this._entities = new DMSEntities();
        }

        public object GetAllProducts()
        {
            var products = (from prod in _entities.products
                            join uni in _entities.units on prod.unit_id equals uni.unit_id
                            join bra in _entities.brands on prod.brand_id equals bra.brand_id
                            join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                            select new
                            {

                                product_id = prod.product_id,
                                product_name = prod.product_name,
                                product_code = prod.product_code,
                                unit_id = prod.unit_id,
                                unit_name = uni.unit_name,
                                brand_id = prod.brand_id,
                                brand_name = bra.brand_name,
                                product_category_id = prod.product_category_id,
                                product_category_name = pc.product_category_name,
                                current_balance = prod.current_balance,

                                // batch_number = prod.batch_number,

                                product_image_url = prod.product_image_url,
                                has_serial = prod.has_serial,
                                has_warrenty = prod.has_warrenty,
                                warrenty_type = prod.warrenty_type,
                                warrenty_value = prod.warrenty_value,
                                vat_percentage = prod.vat_percentage,
                                tax_percentage = prod.tax_percentage,
                                rp_price = prod.rp_price,
                                md_price = prod.md_price,
                                mrp_price = prod.mrp_price,
                                bs_price = prod.bs_price

                            }).OrderBy(e => e.product_name).ToList();

            return products;
        }

        public object GetAllAccessories()
        {
            var products = (from prod in _entities.products
                            join uni in _entities.units on prod.unit_id equals uni.unit_id
                            join bra in _entities.brands on prod.brand_id equals bra.brand_id
                            join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                            where pc.product_category_name == "Accessories"
                            select new
                            {

                                product_id = prod.product_id,
                                product_name = prod.product_name,
                                product_code = prod.product_code,
                                unit_id = prod.unit_id,
                                unit_name = uni.unit_name,
                                brand_id = prod.brand_id,
                                brand_name = bra.brand_name,
                                product_category_id = prod.product_category_id,
                                product_category_name = pc.product_category_name,
                                current_balance = prod.current_balance,

                                // batch_number = prod.batch_number,

                                product_image_url = prod.product_image_url,
                                has_serial = prod.has_serial,
                                has_warrenty = prod.has_warrenty,
                                warrenty_type = prod.warrenty_type,
                                warrenty_value = prod.warrenty_value,
                                vat_percentage = prod.vat_percentage,
                                tax_percentage = prod.tax_percentage,
                                rp_price = prod.rp_price,
                                md_price = prod.md_price,
                                mrp_price = prod.mrp_price,
                                bs_price = prod.bs_price

                            }).OrderBy(e => e.product_name).ToList();

            return products;
        }

        //public object GetProductCategorywiseProduct(long product_category_id)
        public object GetProductCategorywiseProduct()
        {
            try
            {
                //var products = (from p in _entities.products
                //                join pc in _entities.product_category on p.product_category_id equals pc.product_category_id
                //                select new
                //                {
                //                    product_id = p.product_id,
                //                    product_name = p.product_name
                //                }).ToList();
                //return products;

                var products = (from prod in _entities.products
                                join uni in _entities.units on prod.unit_id equals uni.unit_id
                                join bra in _entities.brands on prod.brand_id equals bra.brand_id
                                join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                                select new
                                {
                                    product_id = prod.product_id,
                                    product_name = prod.product_name,
                                    product_code = prod.product_code,
                                    unit_id = prod.unit_id,
                                    unit_name = uni.unit_name,
                                    brand_id = prod.brand_id,
                                    brand_name = bra.brand_name,
                                    product_category_id = prod.product_category_id,
                                    product_category_name = pc.product_category_name,
                                    current_balance = prod.current_balance,

                                    // batch_number = prod.batch_number,

                                    product_image_url = prod.product_image_url,
                                    has_serial = prod.has_serial,
                                    has_warrenty = prod.has_warrenty,
                                    warrenty_type = prod.warrenty_type,
                                    warrenty_value = prod.warrenty_value,
                                    vat_percentage = prod.vat_percentage,
                                    tax_percentage = prod.tax_percentage,
                                    rp_price = prod.rp_price,
                                    md_price = prod.md_price,
                                    mrp_price = prod.mrp_price,
                                    bs_price = prod.bs_price

                                    //}).Where(w => w.product_category_id == product_category_id).OrderByDescending(e => e.product_id).ToList();
                                }).OrderBy(e => e.product_name).ToList();

                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //15.02.2017
        public object GetProductsWiseFirstColorVersion(long product_id)
        {
            try
            {
                var productFromPricing = (from pp in _entities.product_price_mapping
                                          join p in _entities.products on pp.product_id equals p.product_id
                                          join c in _entities.colors on pp.color_id equals c.color_id into temC
                                          from c in temC.DefaultIfEmpty()
                                          join v in _entities.product_version on pp.product_version_id equals v.product_version_id into temVer
                                          from v in temVer.DefaultIfEmpty()
                                          where pp.product_id == product_id && v.is_active == true
                                          select new
                                          {
                                              product_id = pp.product_id,
                                              product_name = p.product_name,
                                              color_id = pp.color_id,
                                              color_name = c.color_name,
                                              has_serial = p.has_serial,
                                              product_version_id = pp.product_version_id,
                                              product_version_name = v.product_version_name,
                                              dealer_cost = pp.dealer_cost,
                                              /////////////22.02.2017///////////////
                                              b2b_cost = pp.b2b_cost,
                                              retailer_cost = pp.retailer_cost,
                                              mrp_cost = pp.mrp_cost,
                                              internal_cost = pp.internal_cost
                                              //}).ToList().FirstOrDefault();
                                          }).ToList().OrderBy(o => o.product_version_id).FirstOrDefault();

                return productFromPricing;
            }
            catch (Exception)
            {
                throw;
            }

        }
        //18.02.2017
        public object GetProductsWiseFirstColorVersionForDemo(long product_id)
        {
            try
            {
                var productFromPricing = (from pp in _entities.product_price_mapping
                                          join p in _entities.products on pp.product_id equals p.product_id
                                          join c in _entities.colors on pp.color_id equals c.color_id into cTemp
                                          from c in cTemp.DefaultIfEmpty()
                                          join v in _entities.product_version on pp.product_version_id equals v.product_version_id into verTemp
                                          from v in verTemp.DefaultIfEmpty()
                                          where pp.product_id == product_id
                                          select new
                                          {
                                              product_id = pp.product_id,
                                              product_name = p.product_name,
                                              color_id = pp.color_id,
                                              color_name = c.color_name,
                                              product_version_id = pp.product_version_id,
                                              product_version_name = v.product_version_name
                                              //comments on 26.04.2017-mohi uddin
                                              //dealer_cost = (pp.retailer_cost / 2)

                                          }).ToList().OrderBy(o => o.product_version_id).FirstOrDefault();

                return productFromPricing;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetProductsWiseFirstColorVersionForB2b(long product_id)
        {
            var productFromPricing = (from pp in _entities.product_price_mapping
                                      join p in _entities.products on pp.product_id equals p.product_id
                                      join c in _entities.colors on pp.color_id equals c.color_id into cTemp
                                      from c in cTemp.DefaultIfEmpty()
                                      join v in _entities.product_version on pp.product_version_id equals v.product_version_id into vTemp
                                      from v in vTemp.DefaultIfEmpty()
                                      where pp.product_id == product_id
                                      select new
                                      {
                                          product_id = pp.product_id,
                                          product_name = p.product_name,
                                          color_id = pp.color_id,
                                          color_name = c.color_name,
                                          product_version_id = pp.product_version_id,
                                          product_version_name = v.product_version_name,
                                          dealer_cost = pp.dealer_cost,
                                          mrp_cost = pp.mrp_cost,
                                          b2b_cost = pp.b2b_cost

                                          //}).ToList().FirstOrDefault();
                                      }).ToList().OrderBy(o => o.product_version_id).FirstOrDefault();

            return productFromPricing;
        }



        public object GetAllProductsNPrice()
        {
            try
            {
                var products = (from ppm in _entities.product_price_mapping
                                join p in _entities.products on ppm.product_id equals p.product_id
                                select new
                                {
                                    product_id = ppm.product_id,
                                    product_name = p.product_name,
                                    product_code = p.product_code,
                                    product_version_id = ppm.product_version_id,
                                    dealer_cost = ppm.dealer_cost,
                                    b2b_cost = ppm.b2b_cost,
                                    telco_cost = ppm.telco_cost,
                                    corporate_cost = ppm.corporate_cost,
                                    internal_cost = ppm.internal_cost,
                                    gift_cost = ppm.gift_cost,
                                    emi_cost = ppm.emi_cost
                                }).OrderByDescending(e => e.product_id).Distinct().ToList();

                return products;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public object GetAllProductsForGiftDropdown()
        {
            var products = (from prod in _entities.products
                            join uni in _entities.units on prod.unit_id equals uni.unit_id
                            join bra in _entities.brands on prod.brand_id equals bra.brand_id
                            join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                            select new
                            {

                                product_id = prod.product_id,
                                product_name = prod.product_name,
                                product_code = prod.product_code,
                                unit_id = prod.unit_id,
                                unit_name = uni.unit_name,
                                brand_id = prod.brand_id,
                                brand_name = bra.brand_name,
                                product_category_id = prod.product_category_id,
                                product_category_name = pc.product_category_name,
                                current_balance = prod.current_balance,
                                is_active = prod.is_active,
                                is_deleted = prod.is_deleted,

                                // batch_number = prod.batch_number,


                                product_image_url = prod.product_image_url,
                                has_serial = prod.has_serial,
                                has_warrenty = prod.has_warrenty,
                                warrenty_type = prod.warrenty_type,
                                warrenty_value = prod.warrenty_value,
                                vat_percentage = prod.vat_percentage,
                                tax_percentage = prod.tax_percentage,
                                rp_price = prod.rp_price,
                                md_price = prod.md_price,
                                mrp_price = prod.mrp_price,
                                bs_price = prod.bs_price
                            }).OrderByDescending(e => e.product_id).ToList();

            List<GiftProduct> gftList = new List<GiftProduct>();

            foreach (var item in products)
            {
                GiftProduct aa = new GiftProduct();
                aa.gift_product_id = item.product_id;
                aa.gift_product_name = item.product_name;
                gftList.Add(aa);
            }

            return gftList;
        }

        public long AddProduct(product product)
        {
            try
            {
                long modelCode = _entities.products.Max(po => (long?)po.product_id) ?? 0;

                if (modelCode != 0)
                {
                    modelCode++;

                }
                else
                {
                    modelCode++;
                }
                var modelString = modelCode.ToString().PadLeft(7, '0');

                var getProductCtegoryName = _entities.product_category.FirstOrDefault(p => p.product_category_id == product.product_category_id).product_category_code;

                string modelCodeNo = getProductCtegoryName + "-" + modelString;
                product insert_product = new product
                {
                    product_name = product.product_name,
                    product_code = modelCodeNo,
                    unit_id = product.unit_id,
                    brand_id = product.brand_id,
                    product_category_id = product.product_category_id,
                    current_balance = product.current_balance,
                    product_image_url = product.product_image_url,
                    has_serial = product.has_serial,
                    has_warrenty = product.has_warrenty,
                    warrenty_type = product.warrenty_type,
                    warrenty_value = product.warrenty_value,
                    vat_percentage = product.vat_percentage,
                    tax_percentage = product.tax_percentage,
                    rp_price = product.rp_price,
                    md_price = product.md_price,
                    mrp_price = product.mrp_price,
                    bs_price = product.bs_price,
                    created_by = product.created_by,
                    created_date = DateTime.Now,
                    updated_by = product.created_by,
                    updated_date = DateTime.Now,
                    specification = product.specification,
                    remarks = product.remarks,
                    eol_date = product.eol_date,
                    supplier_id = product.supplier_id,
                    launce_date = product.launce_date,
                    accessories_category_id = product.accessories_category_id,
                    is_active = true,
                    is_deleted = false
                };
                _entities.products.Add(insert_product);
                _entities.SaveChanges();
                long last_insert_id = insert_product.product_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public product GetProductById(long product_id)
        {
            var product = _entities.products.Find(product_id);
            return product;
        }

        public bool EditProduct(product product)
        {
            try
            {
                product editProduct = _entities.products.Find(product.product_id);
                editProduct.product_name = product.product_name;
                editProduct.product_code = product.product_code;
                editProduct.unit_id = product.unit_id;
                editProduct.brand_id = product.brand_id;
                editProduct.product_category_id = product.product_category_id;
                editProduct.current_balance = product.current_balance;
                editProduct.product_image_url = product.product_image_url;
                editProduct.has_serial = product.has_serial;
                editProduct.has_warrenty = product.has_warrenty;
                editProduct.warrenty_type = product.warrenty_type;
                editProduct.warrenty_value = product.warrenty_value;
                editProduct.vat_percentage = product.vat_percentage;
                editProduct.tax_percentage = product.tax_percentage;
                editProduct.rp_price = product.rp_price;
                editProduct.md_price = product.md_price;
                editProduct.mrp_price = product.mrp_price;
                editProduct.bs_price = product.bs_price;
                editProduct.updated_by = product.updated_by;
                editProduct.updated_date = DateTime.Now;
                editProduct.specification = product.specification;
                editProduct.remarks = product.remarks;
                editProduct.eol_date = product.eol_date;
                editProduct.launce_date = product.launce_date;
                editProduct.supplier_id = product.supplier_id;
                editProduct.accessories_category_id = product.accessories_category_id;
                editProduct.is_active = product.is_active;

                _entities.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(long product_id)
        {
            try
            {
                product oProduct = _entities.products.FirstOrDefault(c => c.product_id == product_id);
                _entities.products.Attach(oProduct);
                _entities.products.Remove(oProduct);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<product> GetAllProductList()
        {
            var products = _entities.products.OrderByDescending(p => p.product_id).ToList();
            return products;
        }


        public bool CheckDuplicateProductName(string product_name)
        {
            var checkDuplicateProduct = _entities.products.FirstOrDefault(p => p.product_name == product_name);
            bool return_product = checkDuplicateProduct == null ? false : true;
            return return_product;
        }




        public object GetProductWithoutAssocories()
        {
            var products = (from prod in _entities.products
                            join uni in _entities.units on prod.unit_id equals uni.unit_id
                            join bra in _entities.brands on prod.brand_id equals bra.brand_id
                            join pc in _entities.product_category on prod.product_category_id equals pc.product_category_id
                            where pc.product_category_name != "Accessories"
                            select new
                            {

                                product_id = prod.product_id,
                                product_name = prod.product_name,
                                product_code = prod.product_code,
                                unit_id = prod.unit_id,
                                unit_name = uni.unit_name,
                                brand_id = prod.brand_id,
                                brand_name = bra.brand_name,
                                product_category_id = prod.product_category_id,
                                product_category_name = pc.product_category_name,
                                current_balance = prod.current_balance,

                                // batch_number = prod.batch_number,

                                product_image_url = prod.product_image_url,
                                has_serial = prod.has_serial,
                                has_warrenty = prod.has_warrenty,
                                warrenty_type = prod.warrenty_type,
                                warrenty_value = prod.warrenty_value,
                                vat_percentage = prod.vat_percentage,
                                tax_percentage = prod.tax_percentage,
                                rp_price = prod.rp_price,
                                md_price = prod.md_price,
                                mrp_price = prod.mrp_price,
                                bs_price = prod.bs_price

                            }).OrderBy(e => e.product_name).ToList();

            return products;
        }

        public object GetProductStockNBookedQuantity(long product_id, long color_id, long product_version_id)
        {
            try
            {

                var nDeliverQty = 0;
                var remainingQty = 0;
                var bookedQty = 0;
                var stockQuantity = 0;
                var availableQuantity = 0;


                //stock quantity
                if (color_id == 0 && product_version_id == 0)
                {
                    //for accessories
                    stockQuantity =
                       (int)(_entities.inventory_stock.Where(w => w.product_id == product_id && w.warehouse_id == 1).FirstOrDefault().stock_quantity ?? 0);
                }
                else
                {
                    //for serial product
                    stockQuantity =
                   _entities.receive_serial_no_details.Count(w => w.product_id == product_id && w.color_id == color_id &&
                           w.product_version_id == product_version_id && w.current_warehouse_id == 1);
                }


                //not delivered quantity
                if (color_id == 0 && product_version_id == 0)
                {
                    //for accessories
                    string notDeliveredQty = "select isnull(sum(rd.quantity),0) as quantity  from requisition_master rm "
                                       +
                                       " inner join requisition_details rd on rm.requisition_master_id=rd.requisition_master_id "
                                       + " where rd.product_id=" + product_id + "  "
                                       + " and rm.delivery_status='Not Delivered' ";
                    var data1 = _entities.Database.SqlQuery<NotDeliveredQty>(notDeliveredQty).FirstOrDefault();

                    nDeliverQty = data1.quantity;
                }
                else
                {
                    //for serial product
                    string notDeliveredQty = "select isnull(sum(rd.quantity),0) as quantity  from requisition_master rm "
                                        +
                                        " inner join requisition_details rd on rm.requisition_master_id=rd.requisition_master_id "
                                        + " where rd.product_id=" + product_id + " and rd.color_id=" + color_id + " and rd.product_version_id=" + product_version_id + " "
                                        + " and rm.delivery_status='Not Delivered' ";
                    var data1 = _entities.Database.SqlQuery<NotDeliveredQty>(notDeliveredQty).FirstOrDefault();

                    nDeliverQty = data1.quantity;
                }



                //remaining quantity after partial delivery
                if (color_id == 0 && product_version_id == 0)
                {
                    //for accessories
                    string remainingQuantityAfterPartialDelivery = "select isnull(sum(rd.quantity),0) as quantity  from requisition_master rm "
                                     +
                                     " inner join requisition_details rd on rm.requisition_master_id=rd.requisition_master_id "
                                     + " where rd.product_id=" + product_id + " "
                                     + " and rm.delivery_status='Not Delivered' ";
                    var data2 = _entities.Database.SqlQuery<RemainingQtyAfterPartialDelivered>(remainingQuantityAfterPartialDelivery).FirstOrDefault();

                    remainingQty = data2.remaining_qty_after_partial_delivered;
                }
                else
                {
                    //for serial product
                    string remainingQuantityAfterPartialDelivery = "select isnull(sum(rd.quantity),0) as quantity  from requisition_master rm "
                                       +
                                       " inner join requisition_details rd on rm.requisition_master_id=rd.requisition_master_id "
                                       + " where rd.product_id=" + product_id + " and rd.color_id=" + color_id + " and rd.product_version_id=" + product_version_id + " "
                                       + " and rm.delivery_status='Not Delivered' ";
                    var data2 = _entities.Database.SqlQuery<RemainingQtyAfterPartialDelivered>(remainingQuantityAfterPartialDelivery).FirstOrDefault();

                    remainingQty = data2.remaining_qty_after_partial_delivered;
                }




                bookedQty = nDeliverQty + remainingQty;
                availableQuantity = stockQuantity - bookedQty;


                StockQuantityNBookedQuantity abc = new StockQuantityNBookedQuantity();
                abc.stock_quantity = stockQuantity;
                abc.booked_quantity = bookedQty;
                abc.available_quantity = availableQuantity;

                return abc;

                //return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}