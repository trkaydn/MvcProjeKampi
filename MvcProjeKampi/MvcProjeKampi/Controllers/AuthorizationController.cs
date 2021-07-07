using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class AuthorizationController : Controller
    {
        AdminManager adminmanager = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            var adminvalues = adminmanager.GetList();
            return View(adminvalues);
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            p.AdminPassword = Crypto.HashPassword(p.AdminPassword);
            p.AdminUserName = Crypto.HashPassword(p.AdminUserName);
            adminmanager.AdminAdd(p);
            return View();
        }

        public ActionResult EditAdmin(int id)
        {
            var adminvalue = adminmanager.GetByID(id);
            return View(adminvalue);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            adminmanager.AdminUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var adminvalue = adminmanager.GetByID(id);
            adminmanager.AdminDelete(adminvalue);
            return RedirectToAction("Index");
        }

    }
}