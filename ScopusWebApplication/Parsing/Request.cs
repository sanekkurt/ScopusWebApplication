﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Web;
using ScopusWebApplication.Models;
using QuickType;
using System.Xml.Serialization;

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
            string text = "";
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                text = sr.ReadToEnd();
                resp.Close();
                sr.Close();
                using (var sw = new StreamWriter("D://page1.html"))
                    sw.Write(text);
            }
            catch (WebException)
            {
                text = null;
            }
            
            
            return text;

        }

        public MainAuthorInfo Get_Main_Author_Info_By_author_id(string id)
        {
            /*var reb = new Request();

            var mainAuthor = reb.get_http("https://api.elsevier.com/content/search/author?query=AU-ID(" + id + ")&field=surname,given-name,prism:url,eid,orcid,document-count,affiliation-name,affiliation-city,affiliation-country,dc:identifier");
            MainAuthorInfo mainAuthorInfo = new MainAuthorInfo();
            if (mainAuthor != null)
            {
                var jsonWebDataMainAuthorInfo = JsonWebDataMainAuthorInfo.FromJson(mainAuthor);
                try
                {
                    mainAuthorInfo.Surname = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].PreferredName.Surname;
                    mainAuthorInfo.GivenName = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].PreferredName.GivenName;
                    mainAuthorInfo.Eid = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].Eid;
                    mainAuthorInfo.Orcid = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].Orcid;
                    mainAuthorInfo.PrismUrl = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].PrismUrl;
                    mainAuthorInfo.DocumentCount = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].DocumentCount.ToString();
                    mainAuthorInfo.AffiliationCity = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].AffiliationCurrent.AffiliationCity;
                    mainAuthorInfo.AffiliationCountry = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].AffiliationCurrent.AffiliationCountry;
                    mainAuthorInfo.AffiliationName = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].AffiliationCurrent.AffiliationName;
                    mainAuthorInfo.Id = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].DcIdentifier.Remove(0, 10);
                }
                catch (NullReferenceException)
                {
                    mainAuthorInfo.Surname = "-";
                    mainAuthorInfo.GivenName = "-";
                    mainAuthorInfo.Eid = "-";
                    mainAuthorInfo.Orcid = "-";
                    mainAuthorInfo.PrismUrl = "-";
                    mainAuthorInfo.DocumentCount = "-";
                    mainAuthorInfo.AffiliationCity = "-";
                    mainAuthorInfo.AffiliationCountry = "-";
                    mainAuthorInfo.AffiliationName = "-";
                    mainAuthorInfo.Id = "-";
                }                

            }*/
            XmlSerializer formatter = new XmlSerializer(typeof(MainAuthorInfo));

            MainAuthorInfo mainAuthorInfoXml = new MainAuthorInfo();
            using (FileStream fs = new FileStream("D://safiullinInfo.xml", FileMode.OpenOrCreate))
            {
                mainAuthorInfoXml = (MainAuthorInfo)formatter.Deserialize(fs);
            }
            return mainAuthorInfoXml;
            //return mainAuthorInfo;
        }

        public List<Article> Get_article_by_author_id(string id)
        {
            /*int len; // количество ссылок на статьи в идентификаторе автора
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

            var mainAuthor = reb.get_http("https://api.elsevier.com/content/search/author?query=AU-ID(" + id + ")&field=surname,given-name,prism:url,eid,orcid,document-count,affiliation-name,affiliation-city,affiliation-country,dc:identifier");

            var jsonWebDataMainAuthorInfo = JsonWebDataMainAuthorInfo.FromJson(mainAuthor);

            mainAuthor = jsonWebDataMainAuthorInfo.SearchResults.Entry[0].PreferredName.Surname;

            List<Article> articles = new List<Article>(len);


            for (int i = 0; i < len; i++)
            {
                data = reb.get_http("http://api.elsevier.com/content/abstract/scopus_id/" + dcIdentifier[i] + "?field=authors,title,publicationName,volume,issueIdentifier,prism:pageRange,coverDate,article-number,doi,citedby-count,prism:aggregationType");

                var jsonWebDataArticle = JsonWebDataArticle.FromJson(data);

                var article = new Article();

                int count = jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author.Length;

                article.authors = new AuthorInArticle[count];

                for (int j = 0; j < count; j++)
                {
                    article.authors[j] = new AuthorInArticle();
                    article.authors[j].CeInitials = jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author[j].CeInitials;
                    article.authors[j].Surname = jsonWebDataArticle.AbstractsRetrievalResponse.Authors.Author[j].CeSurname;
                    if (article.authors[j].Surname == mainAuthor)
                    {
                        article.authors[j].MainAuthor = true;
                    }
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
            }*/
            XmlSerializer formatter = new XmlSerializer(typeof(List<Article>));


            List<Article> art = new List<Article>();
            using (FileStream fs = new FileStream("D://safiullin.xml", FileMode.OpenOrCreate))
            {
                art = (List<Article>)formatter.Deserialize(fs);
            }
            return art;
            //return articles;
        }
    }
}
