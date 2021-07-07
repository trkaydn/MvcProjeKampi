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
    public class TalentCardController : Controller
    {
        TalentManager tm = new TalentManager(new EfTalentDal());
       
        public ActionResult Index()
        {
            var talents = tm.GetList();
            return View(talents);
        }

        public ActionResult EditCard()
        {
            var talents = tm.GetList();
            return View(talents);
        }

        public ActionResult DeleteTalent(int id)
        {
            var talentvalue = tm.GetByID(id);
            tm.TalentDelete(talentvalue);
            return RedirectToAction("EditCard");
        }

        public ActionResult EditTalent(int id)
        {
            var talentvalue = tm.GetByID(id);
            return View(talentvalue);
        }

        [HttpPost]
        public ActionResult EditTalent(Talent p)
        {
            tm.TalentUpdate(p);
            return RedirectToAction("EditCard");
        }

        public ActionResult AddTalent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTalent(Talent p)
        {
            tm.TalentAdd(p);
            return RedirectToAction("EditCard");
        }


    }
}