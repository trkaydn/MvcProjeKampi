using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());

        public ActionResult Index()
        {
            var files = ifm.GetList();
            return View(files);
        }

        [HttpPost]
        public ActionResult AddImage()
        {
            string fileName = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
            string extension = Path.GetExtension(Request.Files[0].FileName);
            string yol = "/AdminLTE-3.0.4/images/" + fileName + extension;
            Request.Files[0].SaveAs(Server.MapPath(yol));

            ifm.ImageAdd(new ImageFile()
            {
                ImageName = fileName,
                ImagePath = yol
            });
            
            return RedirectToAction("Index");
        }
    }
}