using ScopusWebApplication.Models;
using ScopusWebApplication.Parsing;
using ScopusWebApplication.Save;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ScopusWebApplication.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult EditArticle(Receiving receiving)
        {
            var v = new Request();
            List<Article> test = new List<Article>();
            test = v.get_article_by_author_id(receiving.authorID);
            //ViewBag.Test = test;
            return View(test);
        }

        [HttpPost]
        public FilePathResult GetFile(List<Article> articles)
        {
            SaveDocument saveDocument = new SaveDocument();
            saveDocument.gost(articles);
            string file_path = "D:\\test.doc";
            //string file_path = Server.MapPath("~/Fiels/test.doc");
            string file_type = "application/doc";
            string file_name = "test.doc";
            return File(file_path, file_type, file_name);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}