using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class IstatistikController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        Context context = new Context();

        public ActionResult Index()
        {
            //soru1
            var categoryCount = cm.GetList().Count();
            ViewBag.CategoryCount = categoryCount;

            //soru2
            var yazilimBasliklariCount = context.Headings.Where(x => x.Category.CategoryName == "Yazılım").Count();
            ViewBag.yazilimBasliklariCount = yazilimBasliklariCount;

            //soru3
            var yazarlar = context.Writers.Where(x => x.WriterName.Contains("a")).Count();
            ViewBag.yazarlar = yazarlar;

            //soru4
            int count = 0;
            Category topCategory = null;
            List<Category> categories = cm.GetList();
            for (int i = 0; i < categories.Count(); i++)
            {
                int id = categories[i].CategoryID;
                var headings = context.Headings.Where(x => x.CategoryID == id).ToList();
                if (headings.Count() > count)
                {
                    count = headings.Count();
                    topCategory = categories[i];
                }
            }
            ViewBag.TopCategory = topCategory.CategoryName;

            //soru5
            var trueCategories = context.Categories.Where(x => x.CategoryStatus == true).Count();
            var falseCategories = context.Categories.Where(x => x.CategoryStatus == false).Count();
            var fark = trueCategories - falseCategories;
            ViewBag.Fark = fark;

            return View();
        }
    }
}