using ScopusWebApplication.Models;
using ScopusWebApplication.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public string Index(Receiving receiving)
        {
            List<Article> test = new List<Article>();
            var v = new Request();
            test = v.get_article_by_author_id(receiving.authorID);
            return test.ToString();
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