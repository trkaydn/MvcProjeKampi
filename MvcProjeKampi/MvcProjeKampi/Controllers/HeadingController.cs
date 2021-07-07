using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());


        public ActionResult Index()
        {
            var headingvalues = hm.GetList();
            return View(headingvalues);
        }

        public ActionResult HeadingReport()
        {
            var headingvalues = hm.GetList();
            return View(headingvalues);
        }

        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }
                                                  ).ToList();

            List<SelectListItem> valuewriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurName,
                                                    Value = x.WriterID.ToString()
                                                }
                                                 ).ToList();

            ViewBag.vlw = valuewriter;
            ViewBag.vlc = valuecategory;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAdd(p);
            return RedirectToAction("Index");
        }

        public ActionResult ContentByHeading()
        {
            return View();

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
            hm.HeadingUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            headingvalue.HeadingStatus = false;
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("Index");
        }

        public ActionResult ActivateHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            headingvalue.HeadingStatus = true;
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("Index");
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult GetCalendarEvents()
        {
            List<CalendarEvent> eventItems = new List<CalendarEvent>();
            var headings = hm.GetList();

            foreach (var item in headings)
            {
                eventItems.Add(new CalendarEvent()
                {
                    id = item.Writer.WriterName + " " +item.Writer.WriterSurName,
                    start =item.HeadingDate.ToString("s"),
                    end = item.HeadingDate.ToString("s"),
                    allDay = true,
                    color = "blue",
                    title = item.HeadingName
                });
            }
            return Json(eventItems, JsonRequestBehavior.AllowGet);
        }

    }
}