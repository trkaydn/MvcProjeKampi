using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();


        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }

        //ÖDEV : partial'da mesaj,taslak sayılarının görünmesi sağlandı.
        public PartialViewResult MessageListMenu()
        {
            var p = Session["AdminUserName"].ToString();
            ViewBag.Contact = cm.GetList().Count();
            ViewBag.Inbox = mm.GetListInbox(p).Count();
            ViewBag.Sendbox = mm.GetListSendbox(p).Count();
            ViewBag.Draft = mm.GetListDrafts(p).Count();
            //okundu-okunmadı mesaj sayısı:
            ViewBag.Readed = mm.GetListInbox(p).Where(x => x.IsReaded).Count();
            ViewBag.UnReaded = mm.GetListInbox(p).Where(x => x.IsReaded==false).Count();
            return PartialView();
        }
    }
}