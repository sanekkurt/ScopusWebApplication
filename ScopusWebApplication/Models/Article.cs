using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScopusWebApplication.Models
{
    public class Article
    {
        public AuthorInArticle[] authors { get; set; } // Имена авторов
        public string title { get; set; } // Название работы
        public string journal { get; set; } // Название журнала, содержащего статью
        public string year { get; set; } // Год публикации (если не опубликовано — создания)
        public string volume { get; set; } // Том журнала или книги
        public string number { get; set; } // Номер журнала
        public string pages { get; set; } // Номера страниц, разделённые запятыми или двойным дефисом. Для книги — общее количество страниц.
        public string month { get; set; } // Месяц публикации (может содержать дату). Если не опубликовано — создания.
        public string doi { get; set; } // Цифровой идентификатор объекта
    }
}