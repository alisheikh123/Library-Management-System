using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<LR_Books> LR_Books { get; set; }
        public DbSet<LR_BooksLogs> LR_BooksLogs { get; set; }
        public DbSet<LR_Category> LR_Category { get; set; }
        public DbSet<LR_BookCategory> LR_BookCategory { get; set; }
        public DbSet<LR_Fine> LR_Fine { get; set; }
        public DbSet<LR_Issue> LR_Issue { get; set; }
        public DbSet<AddBook> AddBook { get; set; }
        //public DbSet<LR_Event> LR_Event { get; set; }
      public DbSet<LR_ArticleCategory> ArticleCategory { get; set; }
        public DbSet<LR_ReturnBook> LR_ReturnBook { get; set; }
        public DbSet<LR_JournalsNews> LR_JournalsNews { get; set; }
        public DbSet<LR_RegisteredStudents> LR_RegisteredStudents { get; set; }
        public DbSet<LR_AlliedMaterial> LR_AlliedMaterial { get; set; }
        public DbSet<LR_UserCategory> LR_UserCategory { get; set; }
        public DbSet<LR_AssignRolestoUsers> LR_AssignRolestoUsers { get; set; }
        public DbSet<LR_ReIssue_Records> LR_ReIssue_Records { get; set; }
        public DbSet<LR_Reservations> LR_Reservations { get; set; }

        //Logs
        public DbSet<LR_AlliedLogs> LR_AlliedLogs { get; set; }
        public DbSet<LR_BookCategoryLogs> LR_BookCategoryLogs { get; set; }
        public DbSet<LR_IssueLogs> LR_IssueLogs { get; set; }
        public DbSet<LR_JournalNewsLogs> LR_JournalNewsLogs { get; set; }
        public DbSet<LR_ReservationLogs> LR_ReservationLogs { get; set; }
        public DbSet<LR_ReturnLogs> LR_ReturnLogs { get; set; }
        public DbSet<LR_UserCategoryLogs> LR_UserCategoryLogs { get; set; }
        public DbSet<LR_BookAssignCaTLogs> LR_BookAssignCaTLogs { get; set; }


    }
}