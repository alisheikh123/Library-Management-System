using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class BookIssueController : Controller
    {
        int chkQuam;
        int fin;
        string CAT;
        string auth;
        TimeSpan day;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //Select Category
            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                  new SelectListItem{ Text="Books", Value="1"},
                   new SelectListItem{ Text="Journals", Value="2"},
                        new SelectListItem{ Text="NewsPaper", Value="3"},
                          new SelectListItem{ Text="Magazine", Value="4"},
                        new SelectListItem{ Text="Thesis", Value="5"},
                        new SelectListItem{ Text="FYP Reports", Value="7"},
                          new SelectListItem{ Text="Pamphlets", Value="8"},
                          new SelectListItem{ Text="CD/Video Cassettes", Value="9"},
                          new SelectListItem{ Text="Novels", Value="6"},
                    };
            ViewBag.prioritylist = priority;
            List<LR_ArticleCategory> catname = db.ArticleCategory.ToList();
            ViewBag.cat = new SelectList(catname, "id", "catname");


            //Select 

            List<LR_Issue> item = db.LR_Issue.Where(x => x.Status != "Available").ToList();
            return View(item);
        }

        // ********************************************Book Issue Logs**************************
        public void IssueBookLogs(LR_Issue model)
        {
            try
            {
                LR_IssueLogs model2 = new LR_IssueLogs();
                model2.category = model.artCat_id;
                if (model.artCat_id==1)
                {
                    model.ISBN = CAT;
                }
                
               var authorname=model.author_name = auth;
              
                model2.date = DateTime.Now;
                model2.student_id = model.student_id;
                model2.isbn = CAT;
                model2.title = model.title;
                model2.remaining_quan = model.Remaining_Quanity;
                model2.IssueDate = model.Issue_Date;
                model2.ExpiryDate = model.ExpiryDate;
                model2.author = authorname;
                model2.Status = model.Status;
                
               

                //model2.category = model.Category;
                 db.LR_IssueLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET: BookIssue
        public ActionResult BookIssue()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BookIssue(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {
                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["ISBNID"];
                    auth = form["authorname"];

                    model.ISBN = CAT;
                    model.author_name = auth;
                    

                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    var cat = res.usercategory_id;
                    LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                    var counts = limit.BIssue_Limit;
                    var IssueDays = limit.FirstIDays;
                    var detailsUser = limit.id;
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue book for more than " + IssueDays + " days";
                            return View(model);
                        }

                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.ISBN == CAT && x.author_name == auth).ToList();

                            LR_Books model2 = db.LR_Books.Where(x => x.ISBN == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                            DateTime datIss = model.Issue_Date;
                            DateTime datExp = model.ExpiryDate;

                            
                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                            }
                                    
                                            var remaining = model2.Remaining_Quanity - 1;
                                            if(remaining<0)
                                            {
                                                ViewBag.Reservation = "Book Already Issued ! Want to Reserve Now?";
                                                ViewBag.ISBN = model.ISBN;
                                                
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "Book Already Issued ! Want to Reserve Now?";
                                        ViewBag.ISBN = model.ISBN;
                                        ViewBag.stu_id = model.student_id;
                                        return View();
                                    }

                                    //else
                                    //{

                                    //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                    //    return View(model);

                                    //}
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such book available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }

       
            return View(model);

        }

        // ReservationLogs

        public void ReservationLogs(LR_Reservations model)
        { 
            if(ModelState.IsValid)
            {
                try
                {
                    LR_ReservationLogs model2 = new LR_ReservationLogs();
                    model2.date = DateTime.Now;
                    model2.art_id = model.artCat_id;
                    model2.student = model.student_id;
                    model2.isbn = model.ISBN;
                    model.title = model.title;
                    model2.author = model.author_name;
                    model2.activity = "Reserved";
                    db.LR_ReservationLogs.Add(model2);
                    int i = db.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           else
            {
                ViewBag.error = "bla bla bla";
            }

        }


        //Book Reservation
        public bool Reservation(string code,string title, int id, string author, int cat)
        {

            List<LR_Reservations> reserve = db.LR_Reservations.Where(x => x.ISBN == code && x.author_name == author).ToList();
            var reserveCount = reserve.Count;
            LR_Reservations model = new LR_Reservations();
            model.student_id = id;
            model.ISBN = code;
            model.status = " Reserved";
            model.date = DateTime.Now;
            model.author_name = author;
            model.title = title;
            model.artCat_id = cat;
            if(model.artCat_id==1)
            {
                List<LR_Issue> issue = db.LR_Issue.Where(x => x.ISBN == code && x.author_name == author).ToList();
                var IssueCount= issue.Count;
               
                LR_Books books = db.LR_Books.Where(x => x.ISBN == code && x.author_name == author).FirstOrDefault();
                var quantity =   books.Quantity;
                var remainingQ = books.Remaining_Quanity;
                    if(reserveCount<quantity)
                {
                    db.LR_Reservations.Add(model);
                    db.SaveChanges();
                    ReservationLogs(model);
                  ViewBag.resList = "Reserved Successfully";
                    
                    return true;
                }

                 else
                {
                    ViewBag.reMessage = "Please wait ! All Books are Reserved... ";
                    return false;
                }
                

            }
            else if(model.artCat_id==5 || model.artCat_id == 6 || model.artCat_id == 7 || model.artCat_id == 8 || model.artCat_id == 9)
            {
                LR_AlliedMaterial allied = db.LR_AlliedMaterial.Where(x => x.title == title && x.author_name == author).FirstOrDefault();
                var quantity= allied.Quantity;
                if (reserveCount < quantity)
                {
                    db.LR_Reservations.Add(model);
                    db.SaveChanges();
      
                    ViewBag.resList = "Reserved Successfully";
                    return true;
                    
                }
              
                else
                {
                    ViewBag.reMessage = "Please wait ! All Books are Reserved... ";
                    return false;
                }
                
            }
            else if(model.artCat_id==2 || model.artCat_id == 3 || model.artCat_id == 4)
            {
                LR_JournalsNews journal = db.LR_JournalsNews.Where(x => x.name ==code && x.author_name == author).FirstOrDefault();
                var quantity = journal.Quantity;
                if (reserveCount < quantity)
                {
                    db.LR_Reservations.Add(model);
                    db.SaveChanges();
                    ViewBag.resList = "Reserved Successfully";
                    return true;
                }
                else
                {
                    ViewBag.reMessage = "Please wait ! All Books are Reserved... ";
                    return false;
                }

                
            }

            return false;
            
            
        }


        public ActionResult ReservedList()
        {
            List<LR_Reservations> list = db.LR_Reservations.ToList();
            return View(list);
        }










        //For Books Author Magic Suggest
        [HttpPost]
        public JsonResult Index9(string keyword, int courseId)
        {
            List<LR_Books> list = new List<LR_Books>();

            if (Session["BOOKsAuthorList"] == null)
            {
                UserSession9();
            }
            list = (List<LR_Books>)Session["BOOKsAuthorList"];




            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList9()
        {
            List<string> list = new List<string>();

            if (Session["BOOKAuthorList"] == null)
            {
                UserSession9();
            }
            list = (List<string>)Session["BOOKAuthorList"];
            //var result = list.Select(n => n.UserName).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession9()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_Books.Select(x => x.author_name).ToList();

            Session["BOOKAuthorList"] = list;

        }


        // For select Search
        [HttpPost]
        public JsonResult Index2(string keyword, int courseId)
        {
            List<LR_Books> list = new List<LR_Books>();

            if (Session["userList"] == null)
            {
                UserSession();
            }
            list = (List<LR_Books>)Session["userList"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList()
        {
            List<string> list = new List<string>();

            if (Session["bookList"] == null)
            {
                UserSession();
            }
            list = (List<string>)Session["bookList"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_Books.Select(x => x.name).ToList();

            Session["bookList"] = list;

        }

        [HttpPost]
        public ActionResult ReturnNewBooks(string Role)
        {
            switch (Role)
            {
                case "1":
                    return RedirectToAction("ReturnBooks", new { artId = Role });

                case "2":
                    return RedirectToAction("ReturnJournal", new { artId = Role });
                case "3":
                    return RedirectToAction("ReturnNewsPaper", new { artId = Role });
                case "4":
                    return RedirectToAction("ReturnMagazines", new { artId = Role });
                
                case "5":
                    return RedirectToAction("ReturnThesis", new { artId = Role });
                case "6":
                    return RedirectToAction("ReturnNovels", new { artId = Role });
                case "7":
                    return RedirectToAction("ReturnFYPReports", new { artId = Role });
                case "8":
                    return RedirectToAction("ReturnPamphlets", new { artId = Role });
                case "9":
                    return RedirectToAction("ReturnCds", new { artId = Role });
                default:
                    break;
            }
            return View();
        }


        // Retun Book
       
        public ActionResult ReturnBooks()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnBooks(LR_ReturnBook model, FormCollection form)
        {

            string CAT = form["ISBNID"];
            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.ISBN == CAT).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                  
                }



                ViewBag.fine = fin;

                LR_Books model2 = db.LR_Books.Where(x => x.ISBN ==CAT).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.ISBN = CAT;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;
                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Book is not Issued !";
                    return RedirectToAction("ReturnBooks");
                 }

            }
            return RedirectToAction("ReturnBooks");


        }

        // ReturnJournal
        public ActionResult ReturnJournal()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnJournal(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name==auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;

                
                LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;
                   
                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Journal is not Issued !";
                    return RedirectToAction("ReturnJournal");
                }

            }
            return RedirectToAction("ReturnJournal");


        }




        //*****************************************Return Books & other categories****************************************************//
        // ReturnMagazines
        public ActionResult ReturnMagazines()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnMagazines(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Magazine is not Issued !";
                    return RedirectToAction("ReturnMagazine");
                }

            }
            return RedirectToAction("ReturnJournal");


        }

        // ReturnNewsPaper
        public ActionResult ReturnNewsPaper()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnNewsPaper(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected NewsPaper is not Issued !";
                    return RedirectToAction("ReturnNewsPaper");
                }

            }
            return RedirectToAction("ReturnNewsPaper");


        }


        // ReturnThesis
        public ActionResult ReturnThesis()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnThesis(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Thesis is not Issued !";
                    return RedirectToAction("ReturnThesis");
                }

            }
            return RedirectToAction("ReturnThesis");


        }


        // ReturnFYPReports
        public ActionResult ReturnFYPReports()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnFYPReports(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Report is not Issued !";
                    return RedirectToAction("ReturnFYPReports");
                }

            }
            return RedirectToAction("ReturnFYPReports");


        }

        // ReturnPamphlets
        public ActionResult ReturnPamphlets()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnPamphlets(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Pamphlet is not Issued !";
                    return RedirectToAction("ReturnPamphlets");
                }

            }
            return RedirectToAction("ReturnPamphlets");


        }


        // ReturnPamphlets
        public ActionResult ReturnNovels()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnNovels(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Novel is not Issued !";
                    return RedirectToAction("ReturnNovels");
                }

            }
            return RedirectToAction("ReturnNovels");


        }
        // ReturnCD
        public ActionResult ReturnCds()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ReturnCds(LR_ReturnBook model, FormCollection form)
        {
            string CAT = form["name"];
            string auth = form["authorname"];

            if (model.student_id == null)
            {
                ViewBag.Message = "Please Enter Student Id";
            }
            else
            {
                var amount = 30;
                var res = db.LR_Issue.Where(x => x.student_id == model.student_id && x.title == CAT && x.author_name == auth).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }



                ViewBag.fine = fin;


                LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                if (model2.status == "Issued")
                {
                    model2.status = "Available";
                    db.Entry(model2).State = EntityState.Modified;
                    model.Status = "Available";
                    model.book_title = CAT;
                    model.author_name = auth;
                    model.DueDate = datR;
                    model.Issue_Date = datI;
                    model.returnedDate = datD;
                    model.fine = fin;

                    db.LR_ReturnBook.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorMessage = "Selected Pamphlet is not Issued !";
                    return RedirectToAction("ReturnCds");
                }

            }
            return RedirectToAction("ReturnCds");


        }


        //Magic Suggest for Allied Material
        [HttpPost]
        public JsonResult Index27(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsTitleList"] == null)
            {
                UserSession27();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsTitleList"];




            var result = (from a in list where a.title.ToLower().StartsWith(keyword.ToLower()) select new { a.title });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList27()
        {
            List<string> list = new List<string>();

            if (Session["titleList"] == null)
            {
                UserSession27();
            }
            list = (List<string>)Session["titleList"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession27()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.title).ToList();

            Session["titleList"] = list;

        }

        [HttpPost]
        public JsonResult Index28(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsAuthorList"] == null)
            {
                UserSession28();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsAuthorList"];




            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList28()
        {
            List<string> list = new List<string>();

            if (Session["AuthorList"] == null)
            {
                UserSession28();
            }
            list = (List<string>)Session["AuthorList"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession28()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.author_name).ToList();

            Session["AuthorList"] = list;

        }



        //**********************************************************************END****************************************************************//
        public ActionResult ViewReturnBooks()
        {
            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="Books", Value="1"},
                         new SelectListItem{ Text="Journals", Value="2"},
                        new SelectListItem{ Text="NewsPaper", Value="3"},
                          new SelectListItem{ Text="Magazine", Value="4"},
                           new SelectListItem{ Text="Thesis", Value="5"},
                        new SelectListItem{ Text="FYP Reports", Value="7"},
                          new SelectListItem{ Text="Pamphlets", Value="8"},
                          new SelectListItem{ Text="CD/Video Cassettes", Value="9"},
                          new SelectListItem{ Text="Novels", Value="6"},

                    };
            ViewBag.prioritylist = priority;
            List<LR_ReturnBook> list = db.LR_ReturnBook.ToList();
            return View(list);
        }

        public ActionResult Searching(string option, string search)
        {
            return View(db.LR_Books.Where(x => x.name.StartsWith(search) || search == null).ToList());

        }
        //Advance Search For Books
        public ActionResult AdvanceSearch(string option, string search)
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            if (option == "Name")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.LR_Books.Where(x => x.name.Contains(search) || search == null).ToList());
            }
            else if (option == "PublisherName")
            {
                return View(db.LR_Books.Where(x => x.publisher_name.Contains(search) || search == null).ToList());
            }
            else if (option == "ArticleCategory")
            {
                return View(db.LR_Books.Where(x => x.article_category == search || search == null).ToList());
            }
            else if (option == "Author")
            {
                return View(db.LR_Books.Where(x => x.author_name.Contains(search) || search == null).ToList());
            }
            else if(option=="Name" && option== "Author")
            {
                return View(db.LR_Books.Where(x => x.name.Contains(search) && x.publisher_name.Contains(search) || search == null).ToList());

            }
            else
            {
                return View(db.LR_Books.Where(x => x.name.Contains(search) || search == null).ToList());
            }
        }

      

        public ActionResult LogsApplyFilter(String Code)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                       new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;

            if (Code == "All")
            {
                List<LR_Books> req = db.LR_Books.ToList();
                return PartialView("_ADVSearchingList", req);
            }
           
            List<LR_Books> item = db.LR_Books.Where(x => x.status == Code).ToList();
            return PartialView("_ADVSearchingList", item);



        }
        public ActionResult searchJouNew(string option, string search)
        {
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null && x.article_category== "Journal").ToList());

        }
        public ActionResult searchNew(string option, string search)
        {
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null ).ToList());

        }
        public ActionResult searchNewcat(string option, string search)
        {
            return View(db.LR_JournalsNews.Where(x => x.article_category == "NewsPaper").ToList());

        }
        public ActionResult searchmagNew(string option, string search)
        {
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null && x.article_category == "Magazine").ToList());

        }
        public ActionResult SearchPop(string option, string search)
        {
            List<SelectListItem> priority = new List<SelectListItem>()
                    {
                        new SelectListItem{ Text="Newspaper", Value="2"},
                        new SelectListItem{ Text="Journal", Value="3"},
                          new SelectListItem{ Text="Magazine", Value="4"},

                    };
            ViewBag.JourNewslist = priority;
            List<LR_ArticleCategory> catname = db.ArticleCategory.ToList();
            ViewBag.cat = new SelectList(catname, "id", "catname");

            List<LR_JournalsNews> list = db.LR_JournalsNews.ToList();
            return View(list);

        }
        [HttpPost]
        public ActionResult jourNewSearch(string Role)
        {
            switch (Role)
            {
                //case "1":
                //    return RedirectToAction("AddBooks", new { artId = Role });
                //    ;
                case "2":
                    return RedirectToAction("Newspaper", new { artId = Role });
                case "3":
                    return RedirectToAction("Journal", new { artId = Role });
                case "4":
                    return RedirectToAction("Magazine", new { artId = Role });

                default:
                    break;
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Issue cat = db.LR_Issue.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LR_Issue cat)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit");
            }
            return View(cat);
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Issue cat = db.LR_Issue.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LR_Issue cat = db.LR_Issue.Find(id);
            db.LR_Issue.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_Issue book = db.LR_Issue.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        public ActionResult CreateNewBooks(string Role)
        {
            switch (Role)
            {
                case "1":
                    return RedirectToAction("BookIssue", new { artId = Role });

                case "2":
                    return RedirectToAction("IssueJournal", new { artId = Role });
                case "3":
                    return RedirectToAction("IssueNewsPaper", new { artId = Role });
                case "4":
                    return RedirectToAction("IssueMagazines", new { artId = Role });

                case "5":
                    return RedirectToAction("IssueThesis", new { artId = Role });
                case "6":
                    return RedirectToAction("IssueNovels", new { artId = Role });
                case "7":
                    return RedirectToAction("IssueFypreports", new { artId = Role });
                case "8":
                    return RedirectToAction("IssuePamphlets", new { artId = Role });
                case "9":
                    return RedirectToAction("IssueCds", new { artId = Role });
                default:
                    break;
            }
            return View();
        }


        //Crud Operations for Book Returns
        public ActionResult ReturnEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_ReturnBook cat = db.LR_ReturnBook.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnEdit(LR_Issue cat)
        {
            if (ModelState.IsValid)
            {

                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit");
            }
            return View(cat);
        }

        public ActionResult ReturnDelete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_ReturnBook cat = db.LR_ReturnBook.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }
        [HttpPost, ActionName("ReturnDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnDeleteConfirmed(int id)
        {
            LR_ReturnBook cat = db.LR_ReturnBook.Find(id);
            db.LR_ReturnBook.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("ViewReturnBooks");
        }



        public ActionResult ReturnDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LR_ReturnBook book = db.LR_ReturnBook.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public JsonResult Index3(string keyword, int courseId)
        {
            List<LR_Books> list = new List<LR_Books>();

            if (Session["userListbook2"] == null)
            {
                UserSession4();
            }
            list = (List<LR_Books>)Session["userListbook2"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList4()
        {
            List<string> list = new List<string>();

            if (Session["bookList22"] == null)
            {
                UserSession4();
            }
            list = (List<string>)Session["bookList22"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession4()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_Books.Select(x => x.ISBN).ToList();

            Session["bookList22"] = list;

        }


        //For Return Books Magic Suggest
      
        //public JsonResult Index(string keyword, int courseId)
        //{
        //    List<LR_Issue> list = new List<LR_Issue>();

        //    if (Session["userList"] == null)
        //    {
        //        UserSession3();
        //    }
        //    list = (List<LR_Issue>)Session["userList"];




        //    var result = (from a in list where a.book_title.ToLower().StartsWith(keyword.ToLower()) select new { a.book_title });



        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult UserList3()
        //{
        //    List<string> list = new List<string>();

        //    if (Session["bookList"] == null)
        //    {
        //        UserSession3();
        //    }
        //    list = (List<string>)Session["bookList"];
        //    //var result = list.Select(n => n.UserName).ToArray();



        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        //public void UserSession3()
        //{
        //    var _context = new ApplicationDbContext();
        //    //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

        //    List<string> list = db.LR_Issue.Select(x => x.ISBN).ToList();

        //    Session["bookList"] = list;

        //}

        [HttpPost]
        public JsonResult Index4(string keyword, int courseId)
        {
            List<LR_Issue> list = new List<LR_Issue>();

            if (Session["userListbook"] == null)
            {
                UserSession3();
            }
            list = (List<LR_Issue>)Session["userListbook"];




            var result = (from a in list where a.title.ToLower().StartsWith(keyword.ToLower()) select new { a.title });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList3()
        {
            List<string> list = new List<string>();

            if (Session["bookList2"] == null)
            {
                UserSession3();
            }
            list = (List<string>)Session["bookList2"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession3()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_Issue.Select(x => x.ISBN).ToList();

            Session["bookList2"] = list;

        }
        //Issue NewsPaper

        public ActionResult IssueNewsPaper()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueNewsPaper(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
            {
                //  model.date = DateTime.Now;
                CAT = form["name"];
                 auth = form["authorname"];
                model.title = CAT;
                model.author_name = auth;
                var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                if (res == null)
                {
                    ViewBag.error = "User does not exist";
                }
                
               
                else
                {

                    var cat = res.usercategory_id;
                    LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                    var counts = limit.BIssue_Limit;
                    var IssueDays = limit.FirstIDays;
                    var detailsUser = limit.id;
                    DateTime datI = model.Issue_Date;
                    DateTime datR = model.ExpiryDate;
                    day = datR.Subtract(datI);
                    // For 
                    if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                    {
                        ViewBag.InfoMessage = "You cannot issue newspaper for more than " + IssueDays + " days";
                        return View(model);
                    }
                    else
                    {
                        List<LR_Issue> chkcount = new List<LR_Issue>();
                        chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                        LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                        if (model2 != null)
                        {
                            chkQuam = model2.Quantity;

                        DateTime datIss = model.Issue_Date;
                        DateTime datExp = model.ExpiryDate;

                        
                            if (datIss < datExp)
                            {
                                //if (model2.status == "Available" || model2.status == "Book Returned")
                                if (chkcount.Count <= model2.Quantity)
                                {
                                    {
                                        if (chkcount.Count == model2.Quantity)
                                        {
                                            model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                        var remaining = model2.Remaining_Quanity - 1;
                                        if(remaining<0)
                                        {
                                            ViewBag.Reservation = "Newspaper Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.author = model.author_name;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.cat = model.artCat_id;
                                                    
                                            return View(model);
                                        }
                                        model2.Remaining_Quanity = remaining;
                                        model.Status = "Issued";
                                        model.activity = "Active";
                                        model.Remaining_Quanity = remaining;
                                        var obj = ModelState.IsValid;
                                        db.LR_Issue.Add(model);
                                        db.Entry(model2).State = EntityState.Modified;
                                        db.SaveChanges();
                                        IssueBookLogs(model);
                                         return RedirectToAction("Index");
                                    }
                                }
                                else
                                {
                                    ViewBag.Reservation = "Newspaper Already Issued ! Want to Reserve Now?";
                                }

                                //else
                                //{

                                //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                //    return View(model);

                                //}
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                return View(model);
                            }

                        }

                        else
                        {
                            ViewBag.ErrorMessage = "No such Newspaper available!";
                            return View(model);
                        }
                    }
                }
            }
                }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }

        [HttpPost]
        public JsonResult Index7(string keyword, int courseId)
        {
            List<LR_JournalsNews> list = new List<LR_JournalsNews>();

            if (Session["NewsTitleList2"] == null)
            {
                UserSession7();
            }
            list = (List<LR_JournalsNews>)Session["NewsTitleList2"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList7()
        {
            List<string> list = new List<string>();

            if (Session["titleListjournal2"] == null)
            {
                UserSession7();
            }
            list = (List<string>)Session["titleListjournal2"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession7()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_JournalsNews.Select(x => x.name).ToList();

            Session["titleListjournal2"] = list;

        }

        //For Author Magic Suggest

        [HttpPost]
        public JsonResult Index8(string keyword, int courseId)
        {
            List<LR_JournalsNews> list = new List<LR_JournalsNews>();

            if (Session["NewsAuthorList2"] == null)
            {
                UserSession8();
            }
            list = (List<LR_JournalsNews>)Session["NewsAuthorList2"];




            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList8()
        {
            List<string> list = new List<string>();

            if (Session["AuthorList2"] == null)
            {
                UserSession8();
            }
            list = (List<string>)Session["AuthorList2"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession8()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_JournalsNews.Select(x => x.author_name).ToList();

            Session["AuthorList2"] = list;

        }


        //Magic Suggest for magazine
        [HttpPost]
        public JsonResult Index77(string keyword, int courseId)
        {
            List<LR_JournalsNews> list = new List<LR_JournalsNews>();

            if (Session["NewsTitleList77"] == null)
            {
                UserSession77();
            }
            list = (List<LR_JournalsNews>)Session["NewsTitleList77"];




            var result = (from a in list where a.name.ToLower().StartsWith(keyword.ToLower()) select new { a.name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList77()
        {
            List<string> list = new List<string>();

            if (Session["titleListjournal77"] == null)
            {
                UserSession77();
            }
            list = (List<string>)Session["titleListjournal77"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession77()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_JournalsNews.Select(x => x.name).ToList();

            Session["titleListjournal77"] = list;

        }






        [HttpPost]
        public JsonResult Index88(string keyword, int courseId)
        {
            List<LR_JournalsNews> list = new List<LR_JournalsNews>();

            if (Session["NewsAuthorList88"] == null)
            {
                UserSession88();
            }
            list = (List<LR_JournalsNews>)Session["NewsAuthorList88"];




            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList88()
        {
            List<string> list = new List<string>();

            if (Session["AuthorList88"] == null)
            {
                UserSession88();
            }
            list = (List<string>)Session["AuthorList88"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession88()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_JournalsNews.Select(x => x.author_name).ToList();

            Session["AuthorList88"] = list;

        }





        //IssueJournal

        public ActionResult IssueJournal()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueJournal(LR_Issue model, FormCollection form)
        {

            if (ModelState.IsValid)
            {
                //  model.date = DateTime.Now;
                 CAT = form["name"];
                 auth = form["authorname"];
                model.title = CAT;
                model.author_name = auth;
                var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                if (res == null)
                {
                    ViewBag.error = "User does not exist";
                }
                else
                {
                    LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                var counts = limit.BIssue_Limit;
                var IssueDays = limit.FirstIDays;
                var detailsUser = limit.id;
               

                    var cat = res.usercategory_id;
                    DateTime datI = model.Issue_Date;
                    DateTime datR = model.ExpiryDate;
                    day = datR.Subtract(datI);
                    // For 
                    if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                    {
                        ViewBag.InfoMessage = "You cannot issue journal for more than " + IssueDays + " days";
                        return View(model);
                    }
                    else
                    {
                        List<LR_Issue> chkcount = new List<LR_Issue>();
                        chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                        LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                        if (model2 != null)
                        {
                            chkQuam = model2.Quantity;

                        DateTime datIss = model.Issue_Date;
                        DateTime datExp = model.ExpiryDate;

                        
                            if (datIss < datExp)
                            {
                                //if (model2.status == "Available" || model2.status == "Book Returned")
                                if (chkcount.Count <= model2.Quantity)
                                {
                                    {
                                        var remaining = model2.Remaining_Quanity - 1;
                                        if (chkcount.Count == model2.Quantity && remaining<0)
                                        {
                                            model2.status = "Issued";
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            ViewBag.Reservation = "Journal Already Issued ! Want to Reserve Now?";
                                            ViewBag.title = model.title;
                                            ViewBag.stu_id = model.student_id;
                                            ViewBag.author = model.author_name;
                                            ViewBag.cat = model.artCat_id;
                                            return View();
                                        }

                                       
                                       
                                           
                                        
                                        model2.Remaining_Quanity = remaining;
                                        model.Status = "Issued";
                                        model.activity = "Active";
                                        model.Remaining_Quanity = remaining;
                                        var obj = ModelState.IsValid;
                                        db.LR_Issue.Add(model);
                                        db.Entry(model2).State = EntityState.Modified;
                                        db.SaveChanges();
                                        IssueBookLogs(model);
                                        return RedirectToAction("Index");
                                    }
                                }
                                else
                                {
                                    ViewBag.Reservation = "Journal Already Issued ! Want to Reserve Now?";
                                    ViewBag.title = model.title;
                                    ViewBag.stu_id = model.student_id;
                                    ViewBag.author = model.author_name;
                                    ViewBag.cat = model.artCat_id;
                                    return View();
                                }

                                //else
                                //{

                                //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                //    return View(model);

                                //}
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                return View(model);
                            }

                        }

                        else
                        {
                            ViewBag.ErrorMessage = "No such Journal available!";
                            return View(model);
                        }
                    }
                }
            }
       
            
       
            return View(model);

}

        public ActionResult IssueThesis()
        {
            return View();
        }
[HttpPost]
        public ActionResult IssueThesis(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["name"];
                    auth = form["authorname"];
                    model.title = CAT;
                    model.author_name = auth;
                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        var cat = res.usercategory_id;
                        LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                        var counts = limit.BIssue_Limit;
                        var IssueDays = limit.FirstIDays;
                        var detailsUser = limit.id;

                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue thesis for more than " + IssueDays + " days";
                            return View(model);
                        }
                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                            LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                                DateTime datIss = model.Issue_Date;
                                DateTime datExp = model.ExpiryDate;


                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            var remaining = model2.Remaining_Quanity - 1;
                                            if (remaining < 0)
                                            {
                                                ViewBag.Reservation = "Thesis Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "Thesis Already Issued ! Want to Reserve Now?";
                                    }

                                    //else
                                    //{

                                    //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                    //    return View(model);

                                    //}
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such thesis available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }



        //IssueMagazines

        public ActionResult IssueMagazines()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueMagazines(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
            {
                //  model.date = DateTime.Now;
                 CAT = form["name"];
                 auth = form["authorname"];
                model.title = CAT;

                    model.author_name = auth;
                var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                if (res == null)
                {
                    ViewBag.error = "User does not exist";
                }
                else
                {

                    var cat = res.usercategory_id;
                LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                var counts = limit.BIssue_Limit;
                var IssueDays = limit.FirstIDays;
                var detailsUser = limit.id;
              

                    DateTime datI = model.Issue_Date;
                    DateTime datR = model.ExpiryDate;
                    day = datR.Subtract(datI);
                    // For 
                    if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                    {
                        ViewBag.InfoMessage = "You cannot issue magazine for more than " + IssueDays + " days";
                        return View(model);
                    }
                    else
                    {
                        List<LR_Issue> chkcount = new List<LR_Issue>();
                        chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                        LR_JournalsNews model2 = db.LR_JournalsNews.Where(x => x.name == CAT && x.author_name == auth).FirstOrDefault();
                        if (model2 != null)
                        {
                            chkQuam = model2.Quantity;

                            DateTime datIss = model.Issue_Date;
                            DateTime datExp = model.ExpiryDate;


                            if (datIss < datExp)
                            {
                                if (chkcount.Count <= model2.Quantity)
                                {
                                    {
                                        if (chkcount.Count == model2.Quantity)
                                        {
                                            model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                        var remaining = model2.Remaining_Quanity - 1;
                                        if (remaining < 0)
                                        {
                                            ViewBag.Reservation = "Magazine Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                        model2.Remaining_Quanity = remaining;
                                        model.Status = "Issued";
                                        model.activity = "Active";
                                        model.Remaining_Quanity = remaining;
                                        var obj = ModelState.IsValid;
                                        db.LR_Issue.Add(model);
                                        db.Entry(model2).State = EntityState.Modified;
                                        db.SaveChanges();
                                        IssueBookLogs(model);
                                        return RedirectToAction("Index");
                                    }
                                }
                                else
                                {
                                    ViewBag.Reservation = "Magazine Already Issued ! Want to Reserve Now?";
                                }

                                
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                return View(model);
                            }

                        }

                        else
                        {
                            ViewBag.ErrorMessage = "No such Magazine available!";
                            return View(model);
                        }
                    }
                }
            }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }



            return View(model);

        }


        [HttpPost]
        public JsonResult Index17(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsTitleList17"] == null)
            {
                UserSession17();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsTitleList17"];




            var result = (from a in list where a.title.ToLower().StartsWith(keyword.ToLower()) select new { a.title });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList17()
        {
            List<string> list = new List<string>();

            if (Session["titleList17"] == null)
            {
                UserSession17();
            }
            list = (List<string>)Session["titleList17"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession17()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.title).ToList();

            Session["titleList17"] = list;

        }

        //For Author Magic Suggest

        [HttpPost]
        public JsonResult Index18(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsAuthorList1"] == null)
            {
                UserSession18();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsAuthorList1"];

            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList18()
        {
            List<string> list = new List<string>();

            if (Session["AuthorList1"] == null)
            {
                UserSession18();
            }
            list = (List<string>)Session["AuthorList1"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession18()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.author_name).ToList();

            Session["AuthorList1"] = list;

        }


        // Magic Suggest for Issue CD


        [HttpPost]
        public JsonResult Index117(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsTitleList117"] == null)
            {
                UserSession117();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsTitleList117"];




            var result = (from a in list where a.title.ToLower().StartsWith(keyword.ToLower()) select new { a.title });



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList117()
        {
            List<string> list = new List<string>();

            if (Session["titleList117"] == null)
            {
                UserSession117();
            }
            list = (List<string>)Session["titleList117"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession117()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.title).ToList();

            Session["titleList117"] = list;

        }

        //For Author Magic Suggest

        [HttpPost]
        public JsonResult Index118(string keyword, int courseId)
        {
            List<LR_AlliedMaterial> list = new List<LR_AlliedMaterial>();

            if (Session["NewsAuthorList11"] == null)
            {
                UserSession118();
            }
            list = (List<LR_AlliedMaterial>)Session["NewsAuthorList11"];

            var result = (from a in list where a.author_name.ToLower().StartsWith(keyword.ToLower()) select new { a.author_name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserList118()
        {
            List<string> list = new List<string>();

            if (Session["AuthorList1"] == null)
            {
                UserSession118();
            }
            list = (List<string>)Session["AuthorList11"];
            //var result = list.Select(n => n.UserName).ToArray();



            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void UserSession118()
        {
            var _context = new ApplicationDbContext();
            //  var UserManager = new UserManager<ApplicationUser>(new UserStore<LR_Books>(_context));

            List<string> list = db.LR_AlliedMaterial.Select(x => x.author_name).ToList();

            Session["AuthorList11"] = list;

        }


        //IssueFypreports

        public ActionResult IssueFypreports()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueFypreports(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["name"];
                     auth = form["authorname"];
                    model.title = CAT;
                    model.author_name = auth;
                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        var cat = res.usercategory_id;
                        LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                        var counts = limit.BIssue_Limit;
                        var IssueDays = limit.FirstIDays;
                        var detailsUser = limit.id;

                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue FYP Report for more than " + IssueDays + " days";
                            return View(model);
                        }
                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                            LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                                DateTime datIss = model.Issue_Date;
                                DateTime datExp = model.ExpiryDate;


                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            var remaining = model2.Remaining_Quanity - 1;
                                            if (remaining < 0)
                                            {
                                                ViewBag.Reservation = "FYP Report Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "FYP Report Already Issued ! Want to Reserve Now?";
                                    }

                                    //else
                                    //{

                                    //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                    //    return View(model);

                                    //}
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such FYP Report available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }


        //IssueCds

        public ActionResult IssueCds()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueCds(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["name"];
                     auth = form["authorname"];
                    model.title = CAT;
                    model.author_name = auth;
                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        var cat = res.usercategory_id;
                        LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                        var counts = limit.BIssue_Limit;
                        var IssueDays = limit.FirstIDays;
                        var detailsUser = limit.id;

                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue Cd for more than " + IssueDays + " days";
                            return View(model);
                        }
                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                            LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                                DateTime datIss = model.Issue_Date;
                                DateTime datExp = model.ExpiryDate;


                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            var remaining = model2.Remaining_Quanity - 1;
                                            if (remaining < 0)
                                            {
                                                ViewBag.Reservation = "CD Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "CD Already Issued ! Want to Reserve Now?";
                                    }
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such CD available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }

        //IssueNovels

        public ActionResult IssueNovels()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueNovels(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["name"];
                     auth = form["authorname"];
                    model.title = CAT;
                    model.author_name = auth;
                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        var cat = res.usercategory_id;
                        LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                        var counts = limit.BIssue_Limit;
                        var IssueDays = limit.FirstIDays;
                        var detailsUser = limit.id;

                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue Novel for more than " + IssueDays + " days";
                            return View(model);
                        }
                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                            LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                                DateTime datIss = model.Issue_Date;
                                DateTime datExp = model.ExpiryDate;


                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            var remaining = model2.Remaining_Quanity - 1;
                                            if (remaining < 0)
                                            {
                                                ViewBag.Reservation = "Novel Already Issued ! Want to Reserve Now?";
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "Novel Already Issued ! Want to Reserve Now?";
                                    }

                                    //else
                                    //{

                                    //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                    //    return View(model);

                                    //}
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such Novel available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }


        //IssuePamphlets

        public ActionResult IssuePamphlets()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssuePamphlets(LR_Issue model, FormCollection form)
        {
            if (db.LR_AssignRolestoUsers.Any(p => p.user == model.student_id))
            {

                if (ModelState.IsValid)
                {
                    //  model.date = DateTime.Now;
                     CAT = form["name"];
                     auth = form["authorname"];
                    model.title = CAT;
                    model.author_name = auth;
                    var res = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
                    if (res == null)
                    {
                        ViewBag.error = "User does not exist";
                    }
                    else
                    {


                        var cat = res.usercategory_id;
                        LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                        var counts = limit.BIssue_Limit;
                        var IssueDays = limit.FirstIDays;
                        var detailsUser = limit.id;

                        DateTime datI = model.Issue_Date;
                        DateTime datR = model.ExpiryDate;
                        day = datR.Subtract(datI);
                        // For 
                        if (day.Days > IssueDays && res.usercategory_id == detailsUser)
                        {
                            ViewBag.InfoMessage = "You cannot issue Pamphlet for more than " + IssueDays + " days";
                            return View(model);
                        }
                        else
                        {
                            List<LR_Issue> chkcount = new List<LR_Issue>();
                            chkcount = db.LR_Issue.Where(x => x.title == CAT && x.author_name == auth).ToList();

                            LR_AlliedMaterial model2 = db.LR_AlliedMaterial.Where(x => x.title == CAT && x.author_name == auth).FirstOrDefault();
                            if (model2 != null)
                            {
                                chkQuam = model2.Quantity;

                                DateTime datIss = model.Issue_Date;
                                DateTime datExp = model.ExpiryDate;


                                if (datIss < datExp)
                                {
                                    //if (model2.status == "Available" || model2.status == "Book Returned")
                                    if (chkcount.Count <= model2.Quantity)
                                    {
                                        {
                                            if (chkcount.Count == model2.Quantity)
                                            {
                                                model2.status = "Issued";
                                                db.Entry(model2).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }

                                            var remaining = model2.Remaining_Quanity - 1;
                                            if (remaining < 0)
                                            {
                                                ViewBag.Reservation = "Pamphlet Already Issued ! Want to Reserve Now?";
                                               
                                                ViewBag.title = model.title;
                                                ViewBag.stu_id = model.student_id;
                                                ViewBag.author = model.author_name;
                                                ViewBag.cat = model.artCat_id;
                                                return View();
                                            }
                                            model2.Remaining_Quanity = remaining;
                                            model.Status = "Issued";
                                            model.activity = "Active";
                                            model.Remaining_Quanity = remaining;
                                            var obj = ModelState.IsValid;
                                            db.LR_Issue.Add(model);
                                            db.Entry(model2).State = EntityState.Modified;
                                            db.SaveChanges();
                                            IssueBookLogs(model);
                                            return RedirectToAction("Index");
                                      }
                                    }
                                    else
                                    {
                                        ViewBag.Reservation = "Pamphlet Already Issued ! Want to Reserve Now?";
                                    }

                                    //else
                                    //{

                                    //    ViewBag.ErrorMessage = "The selected book is already Issued!";
                                    //    return View(model);

                                    //}
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = "Expiry date is less then issue date";
                                    return View(model);
                                }

                            }

                            else
                            {
                                ViewBag.ErrorMessage = "No such Pamphlet available!";
                                return View(model);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("student_id", "Student_Id does not exists.");
            }




            return View(model);

        }


        //**************************************New Interface for Return Books****************************************************************//

        public ActionResult Return(string option, string search)
        {    


             var id = Convert.ToInt32(search);
            LR_Issue model = new LR_Issue();
            
            return View(db.LR_Issue.Where(x => x.student_id == id /*&& x.activity=="Active"*/|| id == 0).ToList());

            
         

        }

        public void SaveReturnLogs(LR_ReturnBook model)
        {
            try
            {
                LR_ReturnLogs model2 = new LR_ReturnLogs();
                model2.returnedDate = DateTime.Now;
                model2.student = model.student_id;
                model2.isbn = model.ISBN;
                model2.title = model.book_title;
                model2.author = model.author_name;
                model2.Status = "Return";
                model2.art_cat = model.artCat_id;

                db.LR_ReturnLogs.Add(model2);
                int i = db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult Return(LR_ReturnBook model,FormCollection form)
        {
            return View();

        }

        public bool ReturnMaterial(int code)
        {
            LR_Issue model2 = db.LR_Issue.Where(x => x.id==code).FirstOrDefault();
            if (model2.artCat_id == 1)
            {
                if (ModelState.IsValid)
                {

                    var amount = 30;
                var res = db.LR_Issue.Where(x => x.id == code).FirstOrDefault();
                TimeSpan day;

                DateTime datI = res.Issue_Date;
                DateTime datR = res.ExpiryDate;
                DateTime datD = DateTime.Now;
                if (datR < datD)
                {
                    day = datD.Subtract(datR);
                    int d = day.Days;
                    fin = d * amount;

                    //return View(model);
                }

                ViewBag.fine = fin;
                 LR_ReturnBook model = new LR_ReturnBook();
                    model.id = code;
                    model.ISBN = model2.ISBN;
                    model.Issue_Date = model2.Issue_Date;
                    model.DueDate = model2.ExpiryDate;
                    model.fine = fin;
                    model.returnedDate = DateTime.Now;
                    model.Status = "Available";
                    model.student_id = model2.student_id;
                    model.author_name = model2.author_name;
                    model.book_title = model2.title;
                    model.artCat = model2.artCat;
                    model.activity = "Deactive";
                    model2.activity = "Deactive";
                        var isbn = model.ISBN;
                    LR_Books book = db.LR_Books.Where(x => x.ISBN == isbn).FirstOrDefault();
                     book.status = "Available";
                     db.Entry(book).State = EntityState.Modified;
                    model2.Status = "Available";
                    db.Entry(model2).State = EntityState.Modified;

                    
                        db.LR_ReturnBook.Add(model);
                    //db.Entry(model).State = EntityState.Modified;
                   int i = db.SaveChanges();
                    SaveReturnLogs(model);
                    if (i > 0)
                    {
                        return true;
                    }
                    
            }
            }

            else if (model2.artCat_id == 2 || model2.artCat_id==3 || model2.artCat_id == 4)
            {

                if (ModelState.IsValid)
                {

                    var amount = 30;
                    var res = db.LR_Issue.Where(x => x.id == code).FirstOrDefault();
                    TimeSpan day;

                    DateTime datI = res.Issue_Date;
                    DateTime datR = res.ExpiryDate;
                    DateTime datD = DateTime.Now;
                    if (datR < datD)
                    {
                        day = datD.Subtract(datR);
                        int d = day.Days;
                        fin = d * amount;

                        //return View(model);
                    }

                    ViewBag.fine = fin;
                    LR_ReturnBook model = new LR_ReturnBook();
                    model.id = code;
                   
                    model.Issue_Date = model2.Issue_Date;
                    model.DueDate = model2.ExpiryDate;
                    model.fine = fin;
                    model.returnedDate = DateTime.Now;
                    model.Status = "Available";
                    model.student_id = model2.student_id;
                    model.author_name = model2.author_name;
                    model.book_title = model2.title;
                    model.artCat = model2.artCat;
                    model.activity = "Deactive";
                    var name = model.book_title;
                    var auth = model.author_name;
                    LR_JournalsNews book = db.LR_JournalsNews.Where(x => x.name == name && x.author_name==auth).FirstOrDefault();
                    book.status = "Available";
                    db.Entry(book).State = EntityState.Modified;
                    model2.Status = "Available";
                    model2.activity = "Deactive";
                    db.Entry(model2).State = EntityState.Modified;


                    db.LR_ReturnBook.Add(model);
                    //db.Entry(model).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    SaveReturnLogs(model);
                    if (i > 0)
                    {
                        return true;
                    }

                }

            }

            else if (model2.artCat_id == 5 || model2.artCat_id == 6 || model2.artCat_id == 7 || model2.artCat_id == 8 || model2.artCat_id == 9)
            {

                if (ModelState.IsValid)
                {

                    var amount = 30;
                    var res = db.LR_Issue.Where(x => x.id == code).FirstOrDefault();
                    TimeSpan day;

                    DateTime datI = res.Issue_Date;
                    DateTime datR = res.ExpiryDate;
                    DateTime datD = DateTime.Now;
                    if (datR < datD)
                    {
                        day = datD.Subtract(datR);
                        int d = day.Days;
                        fin = d * amount;

                        //return View(model);
                    }

                    ViewBag.fine = fin;
                    LR_ReturnBook model = new LR_ReturnBook();
                    model.id = code;

                    model.Issue_Date = model2.Issue_Date;
                    model.DueDate = model2.ExpiryDate;
                    model.fine = fin;
                    model.returnedDate = DateTime.Now;
                    model.Status = "Available";
                    model.student_id = model2.student_id;
                    model.author_name = model2.author_name;
                    model.book_title = model2.title;
                    model.artCat = model2.artCat;
                    model.activity = "Deactive";
                    var name = model.book_title;
                    var auth = model.author_name;
                    LR_AlliedMaterial book = db.LR_AlliedMaterial.Where(x => x.title == name && x.author_name == auth).FirstOrDefault();
                    book.status = "Available";
                    db.Entry(book).State = EntityState.Modified;
                    model2.Status = "Available";
                    model2.activity = "Deactive";
                    db.Entry(model2).State = EntityState.Modified;


                    db.LR_ReturnBook.Add(model);
                    //db.Entry(model).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    SaveReturnLogs(model);
                    if (i > 0)
                    {
                        return true;
                    }

                }

            }

            return false;

        }


        public bool CheckIssueLimit(int code)
        {
           
            var res = db.LR_AssignRolestoUsers.Where(x => x.user == code).FirstOrDefault();
            if (res != null)
            {
                LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == res.usercategory_id).FirstOrDefault();
                if (limit != null)
                {
                    var counts = limit.BIssue_Limit;
                    var detailsUser = limit.id;
                    var cat = res.usercategory_id;
                    List<LR_Issue> issue = db.LR_Issue.Where(x => x.student_id == code).ToList();
                    //For underGraduates
                    if (cat == detailsUser)
                    {
                        if (issue.Count >= counts)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {


                        return false;
                    }
                }
                else
                {
                    return false;
                }

                ////for post graduates
                //else if(cat==2)
                //{
                //    if (issue.Count >= counts)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                ////for staff
                //else if (cat == 3)
                //{
                //    if (issue.Count >= counts)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                ////for senior Faculty
                //else if (cat == 4)
                //{
                //    if (issue.Count >= counts)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                ////for junior faculty
                //else if (cat == 5)
                //{
                //    if (issue.Count >=counts)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}

            }
            else
            {
                return false;
            }
        }

        //********************************************************END****************************************************************//


        public ActionResult ReIssue( FormCollection form)
        {
            
            LR_ReIssue_Records model = new LR_ReIssue_Records();
            //Get Values from form
            DateTime expirydate = Convert.ToDateTime(form["ExpiryDate"]);
            DateTime reissue = Convert.ToDateTime(form["reIssueDate"]);
            int code = Convert.ToInt32(form["item_id"]);
            LR_Issue issue = db.LR_Issue.Where(x => x.id == code).FirstOrDefault();
            model.student_id = issue.student_id;
            model.author_name = issue.author_name;
            model.artCat = issue.artCat;
            model.ExpiryDate = expirydate;
            model.ReIssue_Date = reissue;


            // Find number of days between reissued and expiry date

            var newres = db.LR_AssignRolestoUsers.Where(x => x.user == model.student_id).FirstOrDefault();
            var cat = newres.usercategory_id;
            LR_UserCategory limit = db.LR_UserCategory.Where(x => x.id == newres.usercategory_id).FirstOrDefault();
            var counts = limit.BIssue_Limit;
            var IssueDays = limit.ReIDays;
            var detailsUser = limit.id;
            DateTime dateI = model.ReIssue_Date;
            DateTime dateR = model.ExpiryDate;
           TimeSpan dayRe = dateR.Subtract(dateI);
          
            //Calculate Fine
            var amount = 30;
            var student_id = issue.student_id;
            var ISBN = issue.ISBN;
            var res = db.LR_Issue.Where(x => x.student_id == student_id && x.ISBN == ISBN).FirstOrDefault();
            TimeSpan day;
            DateTime datI = res.Issue_Date;
            DateTime datR = res.ExpiryDate;
            DateTime datD = DateTime.Now;
            if (datR < datD)
            {
                day = datD.Subtract(datR);
                int d = day.Days;
                fin = d * amount;
            }
            model.fine = fin;
            model.title = issue.title;
            //For Books
            if (issue.artCat_id == 1)
            {
                List<LR_ReIssue_Records> records = db.LR_ReIssue_Records.Where(x => x.ISBN == issue.ISBN).ToList();
                if (records.Count > limit.attempts)

                {
                    TempData["Imp"] = "You’ve reached the maximum number of ReIssue attempts.";
                }
                else
                {
                    if (dayRe.Days > IssueDays && newres.usercategory_id == detailsUser)
                    {
                        TempData["ImpMsg"] = "You cannot issue books for more than " + IssueDays + " days";

                    }
                    else
                    {
                        model.ISBN = issue.ISBN;
                        LR_Books book = db.LR_Books.Where(x => x.ISBN == ISBN).FirstOrDefault();
                        LR_Issue model2 = db.LR_Issue.Where(X => X.ISBN == ISBN).FirstOrDefault();
                        model2.Issue_Date = reissue;
                        model2.ExpiryDate = expirydate;
                        db.Entry(model2).State = EntityState.Modified;
                        book.status = "Re-Issued";
                        issue.Status = "Re-Issued";
                        issue.activity = "Active";
                        model.Status = "Re-Issued";
                        model.activity = "Active";
                        model.date = DateTime.Now;
                        var obj = ModelState.IsValid;
                        db.LR_ReIssue_Records.Add(model);
                        db.Entry(book).State = EntityState.Modified;
                        db.Entry(issue).State = EntityState.Modified;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                }
            }
            //For Journl News
            else if(issue.artCat_id==2 || issue.artCat_id == 3 || issue.artCat_id == 4)
            {
                List<LR_ReIssue_Records> records = db.LR_ReIssue_Records.Where(x => x.author_name == issue.author_name && x.title==issue.title).ToList();
                if (records.Count > limit.attempts)

                {
                    TempData["Imp"] = "You’ve reached the maximum number of ReIssue attempts.";
                }
                else
                {

                    if (dayRe.Days > IssueDays && newres.usercategory_id == detailsUser)
                    {
                        TempData["ImpMsg"] = "You cannot ReIssue, for more than " + IssueDays + " days";
                    }
                    else
                    {
                        LR_JournalsNews book = db.LR_JournalsNews.Where(x => x.name == issue.title && x.author_name == issue.author_name).FirstOrDefault();
                        LR_Issue model2 = db.LR_Issue.Where(x => x.title == issue.title && x.author_name == issue.author_name).FirstOrDefault();
                        model2.Issue_Date = reissue;
                        model2.ExpiryDate = expirydate;
                        db.Entry(model2).State = EntityState.Modified;
                        book.status = "Re-Issued";
                        issue.Status = "Re-Issue";
                        issue.activity = "Active";
                        model.Status = "Re-Issued";
                        model.activity = "Active";
                        model.date = DateTime.Now;
                        var obj = ModelState.IsValid;
                        db.LR_ReIssue_Records.Add(model);
                        db.Entry(book).State = EntityState.Modified;
                        db.Entry(issue).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
           
            
            //For Allied Material
            else if (issue.artCat_id == 5 || issue.artCat_id == 6 || issue.artCat_id == 7 || issue.artCat_id == 8 || issue.artCat_id == 9)
            {
                List<LR_ReIssue_Records> records = db.LR_ReIssue_Records.Where(x => x.author_name == issue.author_name && x.title == issue.title).ToList();
                if (records.Count > limit.attempts)

                {
                    TempData["Imp"] = "You’ve reached the maximum number of ReIssue attempts.";
                }
                else
                {
                    if (dayRe.Days > IssueDays && newres.usercategory_id == detailsUser)
                    {
                        TempData["ImpMsg"] = "You cannot ReIssue, for more than " + IssueDays + " days";
                    }
                    else
                    {
                        LR_AlliedMaterial book = db.LR_AlliedMaterial.Where(x => x.title == issue.title && x.author_name == issue.author_name).FirstOrDefault();

                        LR_Issue model2 = db.LR_Issue.Where(x => x.title == issue.title && x.author_name == issue.author_name).FirstOrDefault();
                        model2.Issue_Date = reissue;
                        model2.ExpiryDate = expirydate;
                        db.Entry(model2).State = EntityState.Modified; book.status = "Re-Issued";
                        issue.Status = "Re-Issued";
                        issue.activity = "Active";
                        model.Status = "Re-Issued";
                        model.activity = "Active";
                        model.date = DateTime.Now;
                        var obj = ModelState.IsValid;
                        db.LR_ReIssue_Records.Add(model);
                        db.Entry(book).State = EntityState.Modified;
                        db.Entry(issue).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            
           
            return RedirectToAction("Return"); 
        }
       
    }
}