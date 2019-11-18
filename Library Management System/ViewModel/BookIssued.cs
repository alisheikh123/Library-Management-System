using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library_Management_System.Models;

namespace Library_Management_System.ViewModel
{
    public class BookIssued
    {
        private static List<LR_Issue> _dataPoints;
        

        public static List<LR_Issue> GetRandomDataForCategoryAxis(int count)
        {
            double y = 50;
           
            ApplicationDbContext db = new ApplicationDbContext();
            string label = "";
            var BooksIsbn = db.LR_Issue.Where(x => x.ISBN != null & x.Status == "Book Issued").GroupBy(n => n.title).ToList();
            _dataPoints = new List<LR_Issue>();


            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);
                label = BooksIsbn.ToString();

                _dataPoints.Add(new LR_Issue(y, label));
                //dateTime = dateTime.AddDays(1);
            }

            return _dataPoints;
        }
        private static Random random = new Random(DateTime.Now.Millisecond);
        
    }
}