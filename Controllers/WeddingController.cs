using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wedding.Models;
using Microsoft.AspNetCore.Http;
// using wedding.Filters;


namespace wedding.Controllers
{
    [Route("wedding")]
    // localhost:5000/wedding
    public class WeddingController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        private MyContext dbContext;
        public WeddingController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            var x = dbContext.Weddings
                .Include(p => p.Creator)
                .Include(p => p.Answer)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.yes)
                .ToList();
            ViewBag.UserId = SessionUser;
            return View(x);

        }



        [HttpGet("new")]
        public IActionResult New()
        {
            var users = dbContext.Users.ToList();
            ViewBag.Users = users;
            return View();
        }


        [HttpPost("create")]
        public IActionResult CreatePost(Wedding newWed)
        {
            if(ModelState.IsValid)
            {
                newWed.UserId = (int)SessionUser;
                dbContext.Weddings.Add(newWed);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewBag.UserId = SessionUser;
            return View("New", dbContext.Weddings.ToList());
        }


        [HttpGet("show")]
        public IActionResult Show()
        {
            var x = dbContext.Weddings
                .Include(p => p.Creator)
                .Include(p => p.Answer)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.yes)
                .ToList();
            ViewBag.UserId = SessionUser;
            return View(x);
        }



        [HttpGet("delete/{wedId}")]
        public IActionResult Delete(int wedId)
        {
            // query for thing
            Wedding toDel = dbContext.Weddings.FirstOrDefault(p => p.WedId == wedId);
            dbContext.Weddings.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


    }
}