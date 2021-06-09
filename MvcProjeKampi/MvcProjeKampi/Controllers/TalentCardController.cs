﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
    }
}