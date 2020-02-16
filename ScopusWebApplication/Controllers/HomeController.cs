using ScopusWebApplication.Models;
using ScopusWebApplication.Parsing;
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
        public ActionResult Index(Receiving receiving)
        {
            List<Article> test = new List<Article>();
            var v = new Request();
            test = v.get_article_by_author_id(receiving.authorID);
            ViewBag.Test = test;
            return View("EditArticle");
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