using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        AdminManager cm = new AdminManager(new EfAdminDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryChart() 
        {
            return Json(BlogList(),JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> ct = new List<CategoryClass>();
            ct.Add(new CategoryClass()
            {
                CategoryName = "Yazılım Kategorisi",
                CategoryCount=8
            });

            ct.Add(new CategoryClass()
            {
                CategoryName = "Seyehat",
                CategoryCount = 4
            });

            ct.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });

            ct.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 1
            });

            return ct;
        }

        //ödev
        public ActionResult Chart2()
        {
            List<UserClass> ct = new List<UserClass>();

            ct.Add(new UserClass()
            {
                Role = "Admin",
                Count = cm.GetList().Count
            });

            ct.Add(new UserClass()
            {
                Role = "Yazar",
                Count = wm.GetList().Count
            });
            return View(ct);
        }

        public ActionResult Chart3()
        {
            List<UserClass> ct = new List<UserClass>();
            var writers = wm.GetList();

           foreach(var item in writers)
            {
                ct.Add(new UserClass()
                {
                    Role = item.WriterName + " " + item.WriterSurName,
                    Count = hm.GetList().Where(x => x.WriterID == item.WriterID).Count()
                });
            }
            return View(ct);
        }

    }
}