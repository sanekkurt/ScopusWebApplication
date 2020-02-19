using ScopusWebApplication.Models;
using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

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

            wordDoc.SaveAs("D:\\test.doc");
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
            //wordDoc.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"\" + "test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }
    }
        
}
