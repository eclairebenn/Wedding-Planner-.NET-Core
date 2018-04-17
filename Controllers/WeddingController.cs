using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wedding_planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;
    
        public WeddingController(WeddingContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid == null)
            {
                return RedirectToAction("Index", "User");
            }
            User user = _context.Users.Include(u => u.Attending).ThenInclude(g => g.Wedding).SingleOrDefault(person => person.UserId == userid);
            ViewBag.UserId = userid;
            ViewBag.User = user;

            List<Wedding> Weddings = _context.Weddings.Include(w => w.Attendees).ThenInclude(g => g.User).ToList();

            ViewBag.Weddings = Weddings;
            return View("Dashboard");
        }

        [HttpGet]
        [Route("plan")]
        public IActionResult Plan()
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid == null)
            {
                return RedirectToAction("Index", "User");
            }
            return View("Plan");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Wedding NewWedding)
        {
            if(ModelState.IsValid)
            {
                _context.Add(NewWedding);
                int? userid = HttpContext.Session.GetInt32("userid");
                NewWedding.UserId = (int)userid;
                _context.SaveChanges();
                return Redirect($"/wedding/{NewWedding.WeddingId}");
            }
            return View("Plan");
        }

        [HttpGet]
        [Route("wedding/{weddingid}")]
        public IActionResult Show(int weddingid)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid == null)
            {
                return RedirectToAction("Index", "User");
            }
            Wedding wedding = _context.Weddings.Include(wed => wed.Attendees).ThenInclude(g => g.User).SingleOrDefault(w => w.WeddingId == weddingid);
            ViewBag.Wedding = wedding;
            return View("Show");
        }

        [HttpGet]
        [Route("rsvp/{weddingid}")]
        public IActionResult RSVP(int weddingid)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            Guest newGuest = new Guest
            {
                UserId = (int)userid,
                WeddingId = weddingid,
            };

            _context.Add(newGuest);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("unrsvp/{weddingid}")]
        public IActionResult unRSVP(int weddingid)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            Guest guest = _context.Guests.SingleOrDefault(g => g.UserId == userid && g.WeddingId == weddingid);
            _context.Guests.Remove(guest);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("delete/{weddingid}")]
        public IActionResult Delete(int weddingid)
        {
            Wedding WedDelete = _context.Weddings.SingleOrDefault(w => w.WeddingId == weddingid);
            _context.Weddings.Remove(WedDelete);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}