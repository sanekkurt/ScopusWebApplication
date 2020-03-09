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
        public FilePathResult GetFile(List<Article> articles, string typeOutput)
        {
            string file_path = "";
            string file_type = "";
            string file_name = "";
            SaveDocument saveDocument = new SaveDocument();
            if(typeOutput == "BibTex")
            {
                saveDocument.BibTex(articles);
                file_path = "D:\\bibtex.bib";
                file_type = "application/bib";
                file_name = "Articles_BibTex.bib";
                return File(file_path, file_type, file_name);
            }
            else
            {
                switch (typeOutput)
                {
                    case "gost":
                        saveDocument.gost(articles);
                        file_name = "Articles_gost.doc";
                        break;
                    case "vak":
                        saveDocument.vak(articles);
                        file_name = "Articles_vak.doc";
                        break;
                    case "IEEE_conferences":
                        saveDocument.IEEE_conferences(articles);
                        file_name = "Articles_IEEE_conferences.doc";
                        break;
                    case "IEEE_openJournal":
                        saveDocument.IEEE_openJournal(articles);
                        file_name = "Articles_IEEE_openJournal.doc";
                        break;
                    case "harvardStyle":
                        saveDocument.harvardStyle(articles);
                        file_name = "Articles_harvardStyle.doc";
                        break;
                    case "springerLNCS":
                        saveDocument.springerLNCS(articles);
                        file_name = "Articles_springerLNCS.doc";
                        break;

                }

                file_path = "D:\\test.doc";
                file_type = "application/doc";
                return File(file_path, file_type, file_name);
            }
            
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