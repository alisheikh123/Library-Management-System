using System;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Library_Management_System.Models;

namespace Library_Management_System.ViewModel
{
    public class BookIssued
    {
      

        public static List<LR_Issue> GetRandomDataForCategoryAxis(int count,string name)
        {
           

            double y = 50;
          
         ApplicationDbContext db = new ApplicationDbContext();
           
            string label = "";
           
            _dataPoints = new List<LR_Issue>();


           for (int i = 0; i < count; i++)
            {
              
                
                    label = name;
                    y = count;

               _dataPoints.Add(new chart(Y, label));
               
              

                
            }

            return _dataPoints ;
        }

        private static List<LR_Issue> _dataPoints;

        
    }
}