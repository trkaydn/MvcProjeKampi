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
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        AdminManager adm = new AdminManager(new EfAdminDal());

        public ActionResult Inbox()
        {
            var p = Session["AdminUserName"].ToString();
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            var p = Session["AdminUserName"].ToString();
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }

        public ActionResult NewMessage()
        {
            Message message = new Message();
            return View(message);
        }

        //ÖDEV: message class'ına bool türünde IsDraft eklendi. Taslak ekle butonu tıklandığında IsDraft true olarak kayıt olacaktır.
        //Mesaj textini html kodu olarak db'de MessageContent alanına kaydederek renklendirme vs sağlandı. Hata almamak için ValidateInput false eklendi.
      
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
            var sender = Session["AdminUserName"].ToString();
            p.SenderMail = sender;
            mm.MessageAdd(p);
            return RedirectToAction("SendBox");
        }

        //ÖDEV: taslaklar için  getlistfordrafts (taslakları listeleyen metot),  action ve view oluşturuldu 
        public ActionResult Drafts()
        {
            var p = Session["AdminUserName"].ToString();
            var draflist = mm.GetListDrafts(p);
            return View(draflist);
        }

        //ÖDEV: view'da DB'den gelen html kod barındıran (renklendirme için) messagecontent alanı için  Html.Raw() metodu ile dB'deki kodların çalışması (renkli görünmesi) sağlandı.
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


    }
}