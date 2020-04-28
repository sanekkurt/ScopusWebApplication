using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScopusWebApplication.Models
{
    public class AuthorInArticle
    {
        public string Surname { get; set; }
        public string CeInitials { get; set; }
        public bool MainAuthor { get; set; }
        public AuthorInArticle()
        {
            MainAuthor = false;
        }
    }
}