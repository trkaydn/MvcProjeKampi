﻿using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        AdminManager cm = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)
        {
            var adminuserinfo = cm.GetByUserNamePassword(p.AdminUserName, p.AdminPassword);
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(p.AdminUserName, false);
                Session["AdminUserName"] = p.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }

            ViewBag.Message = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }


        //TEST İÇİN HASHLEME VE ÇÖZME (Mimariye taşındı))
        //public ActionResult Hashleme()
        //{
        //    //hashleme (yeni admin kaydı gerekirse kullanılabilir)
        //    Context c = new Context();
        //    Admin a = new Admin();

        //    a.AdminPassword = Crypto.HashPassword("tarik1846");
        //    c.Admins.Add(a);

        //var olan adminleri hashle
        //    foreach (var admin in c.Admins)
        //    {
        //        admin.AdminUserName = Crypto.HashPassword(admin.AdminUserName);
        //    }
        //    var cc=0;
        //    c.SaveChanges();
        //    return View();
        //}

        //public ActionResult HashCozme()
        //{

        //    //hashleme çözümü
        //    Context c = new Context();
        //    Admin a = c.Admins.FirstOrDefault(x => x.AdminID == 1);
        //    bool EsitMi = Crypto.VerifyHashedPassword(a.AdminUserName, "admin@gmail.com");
        //    var cc = 0;
        //    return View();
        //}



    }
}