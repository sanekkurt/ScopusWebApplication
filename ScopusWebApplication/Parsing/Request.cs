﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Web;
using ScopusWebApplication.Models;
using QuickType;

namespace ScopusWebApplication.Parsing
{
    public class Request
    {
        public string get_http(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";
            req.Headers.Add("X-ELS-APIKey", "a2cf9f5c8b129f08875fc06823810ffc");
            req.Accept = "application/json";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            string text = sr.ReadToEnd();
            resp.Close();
            sr.Close();
            using (var sw = new StreamWriter("D:\\page1.html"))
                sw.Write(text);
            return text;

        }

        public List<Article> get_article_by_author_id(string id)
        {
            int len; // количество ссылок на статьи в идентификаторе автора
            string data;
            var reb = new Request();

            data = reb.get_http("http://api.elsevier.com/content/search/scopus?query=AU-ID(" + id + ")&field=dc:identifier&count=100");

            var jsonWebData = JsonWebData.FromJson(data);
            len = jsonWebData.SearchResults.Entry.Length;

            string[] dcIdentifier = new string[len]; // массив, который будет содержать идентификаторы

            for (int i = 0; i < len; i++)
            {
                dcIdentifier[i] = jsonWebData.SearchResults.Entry[i].DcIdentifier.Remove(0, 10);
            }

            List<Article> articles = new List<Article>(len);


            for (int i = 0; i < len; i++)
            {
                data = reb.get_http("http://api.elsevier.com/content/abstract/scopus_id/" + dcIdentifier[i] + "?field=authors,title,publicationName,volume,issueIdentifier,prism:pageRange,coverDate,article-number,doi,citedby-count,prism:aggregationType");

                var jsonWebDataArticle = JsonWebDataArticle.FromJson(data);

                var article = new Article();

                int count = jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author.Length;

                article.authors = new string[count];

                for (int j = 0; j < count; j++)
                {
                    article.authors[j] = jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author[j].CeSurname;
                    article.authors[j] = article.authors[j].Insert(article.authors[j].Length, " ");
                    article.authors[j] = article.authors[j].Insert(article.authors[j].Length, jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author[j].CeInitials);
                }

                try
                {
                    article.title = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["dc:title"];
                }
                catch (KeyNotFoundException)
                {
                    article.title = null;
                }

                try
                {
                    article.journal = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:publicationName"];
                }
                catch (KeyNotFoundException)
                {
                    article.journal = null;
                }

                try
                {
                    article.volume = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:volume"];
                }
                catch (KeyNotFoundException)
                {
                    article.volume = null;
                }

                try
                {
                    article.month = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:coverDate"];
                }
                catch (KeyNotFoundException)
                {
                    article.month = null;
                }

                try
                {
                    article.pages = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:pageRange"];
                }
                catch (KeyNotFoundException)
                {
                    article.pages = null;
                }

                try
                {
                    article.year = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:coverDate"];
                }
                catch (KeyNotFoundException)
                {
                    article.year = null;
                }

                try
                {
                    article.doi = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:doi"];
                }
                catch (KeyNotFoundException)
                {
                    article.doi = null;
                }

                try
                {
                    article.number = jsonWebDataArticle.AbstractsRetrievalResponse.Coredata["prism:issueIdentifier"];
                }
                catch (KeyNotFoundException)
                {
                    article.number = null;
                }

                articles.Add(article);
            }
            return articles;
        }
    }
}
