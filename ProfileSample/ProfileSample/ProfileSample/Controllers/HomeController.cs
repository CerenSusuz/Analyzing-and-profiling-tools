using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ProfileSampleEntities())
            {
                var model = context.ImgSources
                    .Take(20)
                    .Select(item => new ImageModel
                    {
                        Name = item.Name,
                        Data = item.Data
                    })
                    .ToList();

                return View(model);
            }
        }

        public ActionResult Convert()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destinationPath = Server.MapPath($"~/UploadedImages/{fileName}");

                    System.IO.File.Copy(file, destinationPath, overwrite: true);

                    var entity = new ImgSource()
                    {
                        Name = fileName,
                        FilePath = $"/UploadedImages/{fileName}",
                    };

                    context.ImgSources.Add(entity);
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}