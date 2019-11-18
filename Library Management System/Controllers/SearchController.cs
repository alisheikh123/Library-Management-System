using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        //Newspaper
        public ActionResult AdvsearchNew(string option, string search)
        {
            
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null).ToList());

        }
        public ActionResult AdvsearchNewcat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.article_category == "NewsPaper").ToList());

        }
        //Search Magazines
        public ActionResult Advsearchmag(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null).ToList());

        }
        public ActionResult Advsearchmagcat(string option, string search)
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.article_category == "Magazine").ToList());

        }
        //Search Journal

        public ActionResult AdvSearchJou(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.name.StartsWith(search) || search == null).ToList());

        }
        public ActionResult AdvSearchJoucat(string option, string search)
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_JournalsNews.Where(x => x.article_category == "Journal").ToList());

        }


        //Search Thesis

        public ActionResult AdvSearcht(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null).ToList());

        }
        public ActionResult AdvSearchtcat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.category == "Thesis").ToList());

        }

        //Search fyp reports
        public ActionResult advfypview(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null).ToList());

        }
        public ActionResult advfypviewcat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.category == "FYP Reports").ToList());

        }

        //search cd
        public ActionResult advcdview(string option, string search)
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null).ToList());

        }
        public ActionResult advcdviewcat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.category == "CD/Video Cassettes").ToList());

        }

        //search novel
        public ActionResult advnovelview(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null).ToList());

        }
        public ActionResult advnovelviewcat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.category == "Novel").ToList());

        }

        //search pamphlets
        public ActionResult advpamphlets(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.title.StartsWith(search) || search == null).ToList());

        }
        public ActionResult advpamphletscat(string option, string search)
        {

            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                        new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Book Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="Reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            return View(db.LR_AlliedMaterial.Where(x => x.category == "Pamphlets").ToList());

        }


        // ***************************** For Journal & NewsPaper **************************//

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
                List<LR_JournalsNews> req = db.LR_JournalsNews.Where( x => x.article_category== "NewsPaper").ToList();
                return PartialView("_ADVJournalSearchingList", req);
            }

            List<LR_JournalsNews> item = db.LR_JournalsNews.Where(x => x.status == Code && x.article_category=="NewsPaper").ToList();
            return PartialView("_ADVJournalSearchingList", item);



        }

        public ActionResult LogsApplyFilter1(String Code)
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
                List<LR_JournalsNews> req = db.LR_JournalsNews.Where(x => x.article_category == "Magazine").ToList();
                return PartialView("_ADVJournalSearchingList", req);
            }

            List<LR_JournalsNews> item = db.LR_JournalsNews.Where(x => x.status == Code && x.article_category == "Magazine").ToList();
            return PartialView("_ADVJournalSearchingList", item);



        }


        public ActionResult LogsApplyFilter2(String Code)
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
                List<LR_JournalsNews> req = db.LR_JournalsNews.Where(x => x.article_category == "Journal").ToList();
                return PartialView("_ADVJournalSearchingList", req);
            }

            List<LR_JournalsNews> item = db.LR_JournalsNews.Where(x => x.status == Code && x.article_category == "Journal").ToList();
            return PartialView("_ADVJournalSearchingList", item);



        }



        // ***************************** For Allied Material **************************//
        public ActionResult LogsApplyFilterthesis(String Code)
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
                List<LR_AlliedMaterial> req = db.LR_AlliedMaterial.Where(x => x.category == "Thesis").ToList();
                return PartialView("_ADVAlliedSearchingList", req);
            }

            List<LR_AlliedMaterial> item = db.LR_AlliedMaterial.Where(x => x.status == Code && x.category == "Thesis").ToList();
            return PartialView("_ADVAlliedSearchingList", item);



        }
        public ActionResult LogsApplyFilterfyp(String Code)
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
                List<LR_AlliedMaterial> req = db.LR_AlliedMaterial.Where(x => x.category == "FYP Reports").ToList();
                return PartialView("_ADVAlliedSearchingList", req);
            }

            List<LR_AlliedMaterial> item = db.LR_AlliedMaterial.Where(x => x.status == Code && x.category == "FYP Reports").ToList();
            return PartialView("_ADVAlliedSearchingList", item);



        }
        public ActionResult LogsApplyFilternovel(String Code)
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
                List<LR_AlliedMaterial> req = db.LR_AlliedMaterial.Where(x => x.category == "Novel").ToList();
                return PartialView("_ADVAlliedSearchingList", req);
            }

            List<LR_AlliedMaterial> item = db.LR_AlliedMaterial.Where(x => x.status == Code && x.category == "Novel").ToList();
            return PartialView("_ADVAlliedSearchingList", item);



        }
        public ActionResult LogsApplyFilterpam(String Code)
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
                List<LR_AlliedMaterial> req = db.LR_AlliedMaterial.Where(x => x.category == "Pamphlets").ToList();
                return PartialView("_ADVAlliedSearchingList", req);
            }

            List<LR_AlliedMaterial> item = db.LR_AlliedMaterial.Where(x => x.status == Code && x.category == "Pamphlets").ToList();
            return PartialView("_ADVAlliedSearchingList", item);



        }
        public ActionResult LogsApplyFiltercd(String Code)
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
                List<LR_AlliedMaterial> req = db.LR_AlliedMaterial.Where(x => x.category == "CD/Video Cassettes").ToList();
                return PartialView("_ADVAlliedSearchingList", req);
            }

            List<LR_AlliedMaterial> item = db.LR_AlliedMaterial.Where(x => x.status == Code && x.category == "CD/Video Cassettes").ToList();
            return PartialView("_ADVAlliedSearchingList", item);



        }






        //Advance Search for Journal News
        public ActionResult AdvanceSearchJournal(string option, string search)
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
                return View(db.LR_JournalsNews.Where(x => x.name.Contains(search) || search == null && x.article_category=="NewsPaper").ToList());
            }
            else if (option == "PublisherName")
            {
                return View(db.LR_JournalsNews.Where(x => x.publisher_name.Contains(search) || search == null).ToList());
            }
            else if (option == "ArticleCategory")
            {
                return View(db.LR_JournalsNews.Where(x => x.article_category == search || search == null).ToList());
            }
            else if (option == "Author")
            {
                return View(db.LR_JournalsNews.Where(x => x.author_name.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.LR_JournalsNews.Where(x => x.name.Contains(search) || search == null).ToList());
            }
        }


        //Advance Search for Allied Material
        public ActionResult AdvanceSearchAllied(string option, string search)
        {
            List<SelectListItem> LOgfilteritems = new List<SelectListItem>()
                    {

                      new SelectListItem{ Text="All", Value="All"},
                        new SelectListItem{ Text="Available", Value="Available"},
                        new SelectListItem{ Text="Issued", Value="Issued"},
                         new SelectListItem{ Text="Re-Issued", Value="Re-Issued"},
                          new SelectListItem{ Text="Reserved", Value="reserved"}
 };

            ViewData["LOgfilteritems"] = LOgfilteritems;
            if (option == "Name")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.LR_AlliedMaterial.Where(x => x.title.Contains(search) || search == null).ToList());
            }
            else if (option == "PublisherName")
            {
                return View(db.LR_AlliedMaterial.Where(x => x.publisher_name.Contains(search) || search == null).ToList());
            }
            else if (option == "ArticleCategory")
            {

                return View(db.LR_AlliedMaterial.Where(x => x.category == search || search == null).ToList());
            }
            else if (option == "Author")
            {
                return View(db.LR_AlliedMaterial.Where(x => x.author_name.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.LR_AlliedMaterial.Where(x => x.title.Contains(search) || search == null).ToList());
            }
        }


    }
}