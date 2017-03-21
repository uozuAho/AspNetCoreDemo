using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ArticleContext db;

        public AuthorsController(ArticleContext context)
        {
            db = context;
        }

        // GET: Contacts/Index
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName","LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }
    }
}