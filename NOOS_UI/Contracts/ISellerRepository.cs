using NOOS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Contracts
{
    public interface ISellerRepository //: IBaseRepository<SellerRegister>
    {
        public Task<bool> Seller(SellerRegister seller);
    }
}
