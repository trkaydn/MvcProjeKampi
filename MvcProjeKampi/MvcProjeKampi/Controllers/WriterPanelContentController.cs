using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult MyContent()
        {
            string WriterMail = (string)Session["WriterMail"];
            int writerid = wm.GetIDByMail(WriterMail);
            var contentvalues = cm.GetListByWriter(writerid);
            return View(contentvalues);
        }
    }
}