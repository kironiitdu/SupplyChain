using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
    public interface ICourierRepository
    {
        List<courier> GetAllCouriers();
        bool CheckDuplicateCouriers(string courier_name);
        long AddCourier(courier courier);
        courier GetcourierById(long courier_id);
        bool Editcourier(courier courier);
        bool Deletecourier(long courier_id);

        bool CheckDuplicateCouriers(long id,string name);
    }
}
