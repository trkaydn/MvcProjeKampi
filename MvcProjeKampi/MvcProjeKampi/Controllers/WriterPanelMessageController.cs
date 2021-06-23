using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string p = Session["WriterMail"].ToString();
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string p = Session["WriterMail"].ToString();
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }

        public PartialViewResult MessageListMenu()
        {
            var p = Session["WriterMail"].ToString();
            ViewBag.Inbox = mm.GetListInbox(p).Count();
            ViewBag.Sendbox = mm.GetListSendbox(p).Count();
            ViewBag.Draft = mm.GetListDrafts(p).Count();
            //okundu-okunmadı mesaj sayısı:
            ViewBag.Readed = mm.GetListInbox(p).Where(x => x.IsReaded).Count();
            ViewBag.UnReaded = mm.GetListInbox(p).Where(x => x.IsReaded == false).Count();
            return PartialView();
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            //açılan mesajı okundu olarak işaretleme:
            values.IsReaded = true;
            mm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult NewMessage()
        {
            Message message = new Message();
            return View(message);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messageValidator.Validate(p);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View();
            }

            //eğer taslak kaydetme isteği geldiyse (button name'ine göre)
            if (Request.Form["draft"] != null)
                p.IsDraft = true;

            //eğer gönderme isteği geldiyse
            else
                p.IsDraft = false;


            p.MessageDate = DateTime.Now;
            var sender = Session["WriterMail"].ToString();
            p.SenderMail = sender;
            mm.MessageAdd(p);
            return RedirectToAction("SendBox");
        }

        public ActionResult Drafts()
        {
            string p = Session["WriterMail"].ToString();
            var draflist = mm.GetListDrafts(p);
            return View(draflist);
        }

    }
}