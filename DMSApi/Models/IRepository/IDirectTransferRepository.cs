using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSApi.Models.StronglyType;

namespace DMSApi.Models.IRepository
{
    public interface IDirectTransferRepository
    {
        int AddDirectTransfer(DirectTransferModel directTransferModel);
    }
}
