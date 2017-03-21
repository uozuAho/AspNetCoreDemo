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

        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }
    }
}