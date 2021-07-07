using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager cm = new AdminManager(new EfAdminDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

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

        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //google reCapthca
            var response = Request["g-recaptcha-response"];
            const string secret = "6LeFPDYbAAAAANIPSB0RquXYbWJw9s5JchcLsdxw";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {
                ViewBag.Message = "Lütfen reCapthca doğrulayınız.";
                return View();
            }

            var writerinfo = wm.GetByMailPassword(p);
            if (writerinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writerinfo.WriterMail, false);
                Session["WriterMail"] = writerinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            ViewBag.Message = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
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