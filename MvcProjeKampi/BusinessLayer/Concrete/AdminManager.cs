using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public Admin GetByUserName(string UserName)
        {
            Context c = new Context();
            foreach (var admin in c.Admins)
            {
                bool userNameControl = Crypto.VerifyHashedPassword(admin.AdminUserName, UserName);

                if (userNameControl)
                    return admin;

            }
            return null;
        }

        public Admin GetByUserNamePassword(string UserName, string Password)
        {
            Context c = new Context();
            foreach (var admin in c.Admins)
            {
                bool userNameControl = Crypto.VerifyHashedPassword(admin.AdminUserName, UserName);

                if (userNameControl)
                {
                    bool passwordControl = Crypto.VerifyHashedPassword(admin.AdminPassword, Password);
                    if (passwordControl)
                        return admin;
                }
            }
            return null;
        }
    }
}
