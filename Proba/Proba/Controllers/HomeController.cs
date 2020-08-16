using Microsoft.AspNet.Identity;
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
            return View();
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

        public ActionResult Saloni()
        {
            ViewBag.Message = "Salons.";

            return View();
        }

        public ActionResult Uslugi()
        {
            ViewBag.Message = "Uslugi.";

            return View();
        }


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

    }
}