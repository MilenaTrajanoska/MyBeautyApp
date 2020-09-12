using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Proba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Proba.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.Najdobri5Saloni = getBest5Salons();
            ViewBag.Najnovi5Saloni = getNewest5Salons();
            ViewBag.Najgolemi5Saloni = getBiggest5Salons();

            return View();
        }
        public List<Salon> getBest5Salons()
        {
            //List<Salon> list = _context.Salons.OrderByDescending(x=>x.Rating).ThenBy(x=>x.Name).ToList();
            List<Salon> list = _context.Salons.OrderByDescending(x => x.Rating).ThenBy(x => x.Name).ToList();
            //list.Sort((s1, s2) => s1.getRating().CompareTo(s2.getRating()));

            int count = list.Count() >= 5 ? 5 : list.Count();

            return list.GetRange(0, count);
        }
        public List<Salon> getNewest5Salons()
        {
            List<Salon> list = _context.Salons.OrderByDescending(x => x.DataNaKreiranje).ThenBy(x => x.Name).ToList();
            //list.Sort((s1, s2) => s1.getRating().CompareTo(s2.getRating()));
            int count = list.Count() >= 5 ? 5 : list.Count();

            return list.GetRange(0, count);
        }
        public List<Salon> getBiggest5Salons()
        {
            List<Salon> list = _context.Salons.OrderByDescending(x => x.numChairs).ThenBy(x => x.Name).ToList();
            //list.Sort((s1, s2) => s1.getRating().CompareTo(s2.getRating()));
            int count = list.Count() >= 5 ? 5 : list.Count();

            return list.GetRange(0, count);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Uslugi()
        {
            ViewBag.Message = "Uslugi.";

            return View();
        }

        [Authorize(Roles = "Client")]
        public ActionResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();  
               
                var user = _context.Clients.Where(x => x.UserId == userId).FirstOrDefault();

                return Content("/UserImages/"+user.ImagePath);
            }
            else
            {
                return Content("/UserImages/None");
            }
            
        }
        [Authorize(Roles = "Salon")]
        public ActionResult SalonPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                var user = _context.Salons.Where(x => x.UserId == userId).FirstOrDefault();

                return Content("/UserImages/" + user.ImagePath);
            }
            else
            {
                return Content("/UserImages/None");
            }

        }
        [Authorize(Roles ="Client")]
        public ActionResult UserFullName()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                var user = _context.Clients.Where(x => x.UserId == userId).FirstOrDefault();

                return Content(user.ClientName + " " + user.ClientSurname);
            }
            else
            {
                return Content("Default");
            }
        }
        [Authorize(Roles = "Salon")]
        public ActionResult SalonName()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                var user = _context.Salons.Where(x => x.UserId == userId).FirstOrDefault();

                return Content(user.Name);
            }
            else
            {
                return Content("Default");
            }
        }
        public ActionResult searchTown(string town)
        {
            var salons = GetSalons(town);
            return PartialView(salons);

            //return PartialView(_context.Salons.ToList());
        }
        private List<Salon> GetSalons(string town)
        {
            return _context.Salons.Where(s => s.City.Contains(town)).ToList();
        }

        [Authorize]
        public ActionResult Profil()
        {

            if (User.IsInRole("Client"))
            {
                return RedirectToAction("Details", "Clients", new { id = User.Identity.GetUserId() });
            }
            else if (User.IsInRole("Salon"))
            {
                return RedirectToAction("Details", "Salons", new { id = User.Identity.GetUserId() });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

        }
    }
}