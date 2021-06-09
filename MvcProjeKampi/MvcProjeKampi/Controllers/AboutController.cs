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
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }

        public PartialViewResult AddAbout()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            abm.AboutAdd(p);
            return RedirectToAction("Index");
        }
        
        public ActionResult ActivateOrPassive(int id)
        {
            var value = abm.GetByID(id);
            if (value.IsActivated)
                value.IsActivated = false;
            else
                value.IsActivated = true;
            abm.AboutUpdate(value);
            return RedirectToAction("Index");
        }

    }
}