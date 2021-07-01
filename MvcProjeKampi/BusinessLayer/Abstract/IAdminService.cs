using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        Admin GetByUserNamePassword(string UserName, string Password);
        Admin GetByUserName(string UserName);
        List<Admin> GetList();

        void AdminAdd(Admin admin);

        Admin GetByID(int id);

        void AdminDelete(Admin admin);

        void AdminUpdate(Admin admin);

    }
}
