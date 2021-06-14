using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading()
        {
            var values = hm.GetListByWriter();
            return View(values);
        }

        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }
                                                  ).ToList();
            ViewBag.vlc = valuecategory;

            return View();
        } 
        
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = 4;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }

        public ActionResult EditHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }
                                                  ).ToList();
            ViewBag.vlc = valuecategory;
            return View(headingvalue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            var headingvalue = hm.GetByID(p.HeadingID);
            headingvalue.CategoryID = p.CategoryID;
            headingvalue.HeadingName = p.HeadingName;
            hm.HeadingUpdate(headingvalue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            headingvalue.HeadingStatus = false;
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("MyHeading");
        }

   
    }
}