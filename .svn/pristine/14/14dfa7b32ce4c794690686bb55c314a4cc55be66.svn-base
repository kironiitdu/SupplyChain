using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class ColorRepository : IColorRepository
    {
        private DMSEntities _entities;

        public ColorRepository()
        {
            this._entities = new DMSEntities();
        }

        public List<color> GetAllColors()
        {
            var color = _entities.colors.Where(u=>u.color_id !=0).OrderBy(u => u.color_name).ToList();
            return color;
        }

        public object GetAllColorsForGrnExport()
        {
            var color = _entities.colors.OrderByDescending(u => u.color_id).ToList();
            return color;
        }

        public bool CheckDuplicateColors(string color_name)
        {
            var checkDuplicatecolors = _entities.colors.FirstOrDefault(c => c.color_name == color_name);
            bool return_type = checkDuplicatecolors == null ? false : true;
            return return_type;
        }

        public long AddColor(color color)
        {
            try
            {
                color insert_color = new color
                {
                    color_name = color.color_name

                };
                _entities.colors.Add(insert_color);
                _entities.SaveChanges();
                long last_insert_id = insert_color.color_id;
                return last_insert_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public color GetColorById(long color_id)
        {
            var color = _entities.colors.Find(color_id);
            return color;
        }

        public bool EditColor(color color)
        {
            try
            {

                color emp = _entities.colors.Find(color.color_id);
                emp.color_name = color.color_name;
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteColor(long color_id)
        {
            try
            {
                color oColor = _entities.colors.FirstOrDefault(c => c.color_id == color_id);
                _entities.colors.Attach(oColor);
                _entities.colors.Remove(oColor);
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