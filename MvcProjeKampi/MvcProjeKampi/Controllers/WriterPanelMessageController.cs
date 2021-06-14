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
            var messagelist = mm.GetListInbox();
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            var messagelist = mm.GetListSendbox();
            return View(messagelist);
        }

        public PartialViewResult MessageListMenu()
        {
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
            p.SenderMail = "admin@gmail.com"; //sesiondan alınacak
            mm.MessageAdd(p);
            return RedirectToAction("SendBox");
        }

        public ActionResult Drafts()
        {
            var draflist = mm.GetListDrafts();
            return View(draflist);
        }

    }
}