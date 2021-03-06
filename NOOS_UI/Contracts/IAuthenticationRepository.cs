using NOOS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Contracts
{
    public interface IAuthenticationRepository
    {
        public Task<bool> Register(RegistrationModel user);
        public Task<bool> Login(LoginModel user);
        //public Task<bool> SellerRegister(SellerRegistrationModel seller); //might have to change it to seller
        public Task Logout();

    }
}
