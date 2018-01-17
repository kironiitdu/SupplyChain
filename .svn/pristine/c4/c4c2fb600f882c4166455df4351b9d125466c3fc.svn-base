using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface IColorRepository
    {
        List<color> GetAllColors();
        object GetAllColorsForGrnExport();
        bool CheckDuplicateColors(string color_name);
        long AddColor(color color);
        color GetColorById(long color_id);
        bool EditColor(color color);
        bool DeleteColor(long color_id);
        //object GetProductwiseColor();
    }
}
