using ScopusWebApplication.Models;
using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace JsonParsing
{
    class SaveDocument
    {
        public void gost(List<Article> articles)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDoc = wordApp.Documents.Add();
            var wordParag = wordDoc.Paragraphs.Add();

            //Вывод всех авторов в одну строку с верным форматированием
            string authors = "";
            int lenAuthorsArray = articles[0].authors.Length;
            for (int i = 0; i< lenAuthorsArray; i++)
            {
                if(i != lenAuthorsArray - 1)
                {
                    authors = authors.Insert(authors.Length, articles[0].authors[i].Surname + " " + articles[0].authors[i].CeInitials + ", ");
                }
                else
                {
                    authors = authors.Insert(authors.Length, articles[0].authors[i].Surname + " " + articles[0].authors[i].CeInitials + " ");
                }                
            }

            //Вывод всех авторов в одну строку, только наоборот и с дополнительными косыми знаками
            string authorsReverse = "/ ";
            for (int i = 0; i < lenAuthorsArray; i++)
            {                
                if (i != lenAuthorsArray - 1)
                {
                    authorsReverse = authorsReverse.Insert(authorsReverse.Length, articles[0].authors[i].CeInitials + " " + articles[0].authors[i].Surname + ", ");
                }
                else
                {
                    authorsReverse = authorsReverse.Insert(authorsReverse.Length, articles[0].authors[i].CeInitials + " " + articles[0].authors[i].Surname + " // ");
                }
            }

            //Вывод года
            string formatYear = " — . — ";            
            formatYear = formatYear.Insert(3, articles[0].year.Remove(4));

            //Вывод VOLUME
            string formatVolume = "";
            if (articles[0].volume!=null)
            {
                formatVolume = "Vol. , ";
                formatVolume = formatVolume.Insert(5, articles[0].volume);
            }

            //Вывод номера журнала
            string formatNumber = "";
            if (articles[0].number != null)
            {
                formatNumber = "№  .";
                formatNumber = formatNumber.Insert(2, articles[0].number);
            }

            //Вывод количества страниц
            string formatPages = "";
            if (articles[0].pages != null)
            {
                formatPages = " — P. .";
                formatPages = formatPages.Insert(6, articles[0].pages);
            }

            wordParag.Range.Font.Name = "Times New Roman";
            wordParag.Range.Font.Size = 14;
            wordParag.Range.Text = authors + articles[0].title + " " + authorsReverse + articles[0].journal + "." + formatYear + formatVolume + formatNumber + formatPages;
            

            wordDoc.SaveAs("D:\\test.doc");
            wordApp.ActiveDocument.Close();
            wordApp.Quit();
        }
    }
        
}
