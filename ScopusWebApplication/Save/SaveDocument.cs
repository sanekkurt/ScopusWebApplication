using ScopusWebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using BibTeX;

namespace ScopusWebApplication.Save
{
    class SaveDocument
    {
        public void gost(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку с верным форматированием
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + " " + articles[j].authors[i].CeInitials + ", ");
                    }
                    else
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + " " + articles[j].authors[i].CeInitials + " ");
                    }
                }

                //Вывод всех авторов в одну строку, только наоборот и с дополнительными косыми знаками
                string authorsReverse = "/ ";
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authorsReverse = authorsReverse.Insert(authorsReverse.Length, articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + ", ");
                    }
                    else
                    {
                        authorsReverse = authorsReverse.Insert(authorsReverse.Length, articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + " // ");
                    }
                }

                //Вывод года
                string formatYear = " — . — ";
                formatYear = formatYear.Insert(3, articles[j].year.Remove(4));

                //Вывод VOLUME
                string formatVolume = "";
                if (articles[j].volume != null)
                {
                    if (articles[j].number == null)
                    {
                        formatVolume = "Vol. .";
                    }
                    else
                    {
                        formatVolume = "Vol. , ";
                    }

                    formatVolume = formatVolume.Insert(5, articles[j].volume);
                }

                //Вывод номера журнала
                string formatNumber = "";
                if (articles[j].number != null)
                {
                    formatNumber = "№  .";
                    formatNumber = formatNumber.Insert(2, articles[j].number);
                }

                //Вывод количества страниц
                string formatPages = "";
                if (articles[j].pages != null)
                {
                    if (articles[j].number == null && articles[j].volume == null)
                    {
                        formatPages = "P. .";
                        formatPages = formatPages.Insert(3, articles[j].pages);
                    }
                    else
                    {
                        formatPages = " — P. .";
                        formatPages = formatPages.Insert(6, articles[j].pages);
                    }
                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "—");
                    }
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + articles[j].title + " " + authorsReverse + articles[j].journal + "." + formatYear + formatVolume + formatNumber + formatPages;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void vak(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку с верным форматированием
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + " " + articles[j].authors[i].CeInitials + ", ");
                    }
                    else
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + " " + articles[j].authors[i].CeInitials);
                    }
                }



                //Вывод года
                string formatJournal = articles[j].journal;
                if (articles[j].year != null)
                {

                    formatJournal = formatJournal + ", " + articles[j].year.Remove(4) + ".";
                }
                else
                {
                    formatJournal = formatJournal + ".";
                }


                //Вывод VOLUME
                string formatVolume = "";
                if (articles[j].volume != null)
                {

                    formatVolume = "Vol. . ";

                    formatVolume = formatVolume.Insert(5, articles[j].volume);
                }

                //Вывод номера журнала
                string formatNumber = "";
                if (articles[j].number != null)
                {
                    formatNumber = "Is. . ";
                    formatNumber = formatNumber.Insert(4, articles[j].number);
                }

                //Вывод количества страниц
                string formatPages = "";
                if (articles[j].pages != null)
                {
                    formatPages = "Pp. .";
                    formatPages = formatPages.Insert(4, articles[j].pages);

                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "–");
                    }
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + "et. al. " + articles[j].title + " // " + formatJournal + " " + formatVolume + formatNumber + formatPages;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void IEEE_conferences(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку, только наоборот и с дополнительными косыми знаками
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + ", ");
                    }
                    else
                    {
                        authors = authors.Insert(authors.Length, "and " + articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + ", ");
                    }
                }

                //Вывод VOLUME
                string formatVolume = "";
                if (articles[j].volume != null)
                {

                    formatVolume = "vol. , ";

                    formatVolume = formatVolume.Insert(5, articles[j].volume);
                }

                //Вывод месяца и года
                string month = "";
                string year = "";
                if (articles[j].year != null)
                {
                    month = numberMonthInFullWord(articles[j].year.Substring(5, 2));
                    year = " ";
                    year = year.Insert(1, articles[j].year.Remove(4));
                }

                //Вывод количества страниц
                string formatPages = "";
                if (articles[j].pages != null)
                {
                    if (articles[j].year != null)
                    {
                        formatPages = "pp. , ";
                    }
                    else
                    {
                        formatPages = "pp. . ";
                    }
                    formatPages = formatPages.Insert(4, articles[j].pages);

                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "–");
                    }
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + "“" + articles[j].title + "," + "” " + articles[j].journal + ", " + formatVolume + formatPages + month + year;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void IEEE_openJournal(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку, только наоборот и с дополнительными косыми знаками
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + ", ");
                    }
                    else
                    {
                        authors = authors.Insert(authors.Length, "and " + articles[j].authors[i].CeInitials + " " + articles[j].authors[i].Surname + ", ");
                    }
                }


                //Вывод VOLUME
                string formatVolume = "";
                if (articles[j].volume != null)
                {

                    formatVolume = "vol. , ";

                    formatVolume = formatVolume.Insert(5, articles[j].volume);
                }

                //Вывод месяца и года
                string month = "";
                string year = "";
                if (articles[j].year != null)
                {
                    month = numberMonthInHalfWord(articles[j].year.Substring(5, 2));
                    year = " .";
                    year = year.Insert(1, articles[j].year.Remove(4));
                }

                //Вывод количества страниц
                string formatPages = "";
                if (articles[j].pages != null)
                {
                    if (articles[j].year != null)
                    {
                        formatPages = "pp. , ";
                    }
                    else
                    {
                        formatPages = "pp. . ";
                    }
                    formatPages = formatPages.Insert(4, articles[j].pages);

                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "–");
                    }
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + "“" + articles[j].title + "," + "” " + articles[j].journal + ", " + formatVolume + formatPages + month + year;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void harvardStyle(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку с верным форматированием + год, если он есть
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {

                    if (i != lenAuthorsArray - 1 || articles[j].year == null)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + ", " + articles[j].authors[i].CeInitials.Replace(".", "") + ", ");
                    }
                    else
                    {
                        if (authors != "")
                        {
                            authors = authors.Remove(authors.Length - 2);
                        }
                        authors = authors.Insert(authors.Length, " & " + articles[j].authors[i].Surname + ", " + articles[j].authors[i].CeInitials.Replace(".", "") + " " + articles[j].year.Remove(4) + ", ");
                    }
                }

                //Вывод VOLUME и форматирование названия журнала в зависимости от того, есть ли том или нет
                string formatVolume = "";
                string formatJournal = "";
                if (articles[j].volume != null)
                {
                    formatJournal = articles[j].journal + ", ";

                    formatVolume = "том. . ";

                    formatVolume = formatVolume.Insert(5, articles[j].volume);
                }
                else
                {
                    formatJournal = articles[j].journal + ". ";
                }

                //Вывод ссылки на DOI
                string formatDoi = "";
                if (articles[j].doi != null)
                {
                    formatDoi = "https://dx.doi.org/" + articles[j].doi;
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + "“" + articles[j].title + "”, " + formatJournal + formatVolume + formatDoi;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void springerLNCS(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            for (int j = 0; j < articles.Count; j++)
            {
                //Вывод всех авторов в одну строку с верным форматированием
                string authors = "";
                int lenAuthorsArray = articles[j].authors.Length;
                for (int i = 0; i < lenAuthorsArray; i++)
                {
                    if (i != lenAuthorsArray - 1)
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + ", " + articles[j].authors[i].CeInitials + " ");
                    }
                    else
                    {
                        authors = authors.Insert(authors.Length, articles[j].authors[i].Surname + ", " + articles[j].authors[i].CeInitials + ": ");
                    }
                }


                //Вывод имени журнала с пробелом или без, в зависимости от того есть ли доп поля или нет
                string formatJournal = "";
                if (articles[j].volume != null || articles[j].number != null)
                {
                    formatJournal = articles[j].journal + " ";
                }
                else
                {
                    formatJournal = articles[j].journal;
                }


                //Вывод VOLUME + номер журнала
                string formatVolume = "";
                if (articles[j].volume != null)
                {
                    formatVolume = articles[j].volume;
                }

                string formatNumber = "";
                if (articles[j].number != null)
                {
                    if (articles[j].volume != null)
                    {
                        formatNumber = "()";
                        formatNumber = formatNumber.Insert(1, articles[j].number);
                    }
                    else
                    {
                        formatNumber = articles[j].number;
                    }

                }

                //Вывод количества страниц
                string formatPages = "";
                if (articles[j].pages != null)
                {

                    formatPages = articles[j].pages;
                    formatPages = formatPages.Insert(formatPages.Length, " ");

                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "–");
                    }
                }

                //Вывод года
                string formatYear = "";
                if (articles[j].year != null)
                {
                    formatYear = "().";
                    formatYear = formatYear.Insert(1, articles[j].year.Remove(4));
                }

                wordParag.Range.Font.Name = "Times New Roman";
                wordParag.Range.Font.Size = 14;
                wordParag.Range.Text = authors + articles[j].title + ". " + formatJournal + formatVolume + formatNumber + ", " + formatPages + formatYear;
                wordDoc.Paragraphs.Add();
            }


            wordDoc.SaveAs("D://test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }

        public void BibTex(List<Article> articles)
        {
            var database = new BibTeXDatabase();
            BibTeXArticle[] articlesBibtex = new BibTeXArticle[articles.Count];

            for (int i = 0; i < articles.Count; i++)
            {
                string authors = "";
                int lenAuthorsArray = articles[i].authors.Length;
                for (int j = 0; j < lenAuthorsArray; j++)
                {

                    authors = authors.Insert(authors.Length, articles[i].authors[j].Surname + ", " + articles[i].authors[j].CeInitials + " ");

                }

                string formatPages = "";
                if (articles[i].pages != null)
                {

                    formatPages = articles[i].pages;

                    if (formatPages.IndexOf("-") != -1)
                    {
                        formatPages = formatPages.Replace("-", "–");
                    }
                }

                string month = "";
                string year = "";
                if (articles[i].year != null)
                {
                    year = articles[i].year.Remove(4);
                    month = articles[i].year.Substring(5, 2);
                }

                string volume = "";
                if (articles[i].volume != null)
                {
                    volume = articles[i].volume;
                }

                string number = "";
                if (articles[i].number != null)
                {
                    number = articles[i].number;
                }

                articlesBibtex[i] = new BibTeXArticle
                {
                    Title = articles[i].title,
                    Year = year,
                    Volume = volume,
                    Journal = articles[i].journal,
                    Number = number,
                    Author = authors,
                    Pages = formatPages,
                    Month = numberMonthInBibtex(month),
                    CitationKey = i + GetCitationKey(articles[i])
                };
            }

            foreach (var i in articlesBibtex)
            {
                database.Entries.Add(i);
            }

            var text = BibTeXUtilities.ConvertBibTeXDatabaseToText(database);

            string writePath = @"D:\bibtex.bib";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }


                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Метод для получения CitationKey в BibTex
        public string GetCitationKey(Article article)
        {
            string citationKey = "";
            for (int i = 0; i < article.authors.Length; i++)
            {
                citationKey = citationKey + article.authors[i].Surname.Substring(0, 3).ToUpper();
            }
            citationKey = citationKey + article.year.Remove(4) + article.month.Substring(5, 2);
            return citationKey;
        }
        //Метод для возвращения названий месяцев по числам
        public static string numberMonthInFullWord(string number)
        {
            switch (number)
            {
                case "01": return "January";
                case "02": return "February";
                case "03": return "March";
                case "04": return "April";
                case "05": return "May";
                case "06": return "June";
                case "07": return "July";
                case "08": return "August";
                case "09": return "September";
                case "10": return "October";
                case "11": return "November";
                case "12": return "December";
                default: return "";
            }
        }

        public static BibTeXMonth numberMonthInBibtex(string number)
        {
            switch (number)
            {
                case "01": return BibTeXMonth.January;
                case "02": return BibTeXMonth.February;
                case "03": return BibTeXMonth.March;
                case "04": return BibTeXMonth.April;
                case "05": return BibTeXMonth.May;
                case "06": return BibTeXMonth.June;
                case "07": return BibTeXMonth.July;
                case "08": return BibTeXMonth.August;
                case "09": return BibTeXMonth.September;
                case "10": return BibTeXMonth.October;
                case "11": return BibTeXMonth.November;
                case "12": return BibTeXMonth.December;
                default: return BibTeXMonth.None;
            }
        }

        public static string numberMonthInHalfWord(string number)
        {
            switch (number)
            {
                case "01": return "Jan.";
                case "02": return "Feb.";
                case "03": return "Mar.";
                case "04": return "Apr.";
                case "05": return "May";
                case "06": return "Jun.";
                case "07": return "Jul.";
                case "08": return "Aug.";
                case "09": return "Sep.";
                case "10": return "Oct.";
                case "11": return "Nov.";
                case "12": return "Dec.";
                default: return "";
            }
        }
    }
        
}
