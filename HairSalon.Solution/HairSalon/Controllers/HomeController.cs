using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon.ViewModels;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            return View(model);
        }

        [HttpPost("/stylists/add")]
        public ActionResult AddStylist()
        {
            string name = Request.Form["stylist-name"];
            string phone = Request.Form["stylist-phone"];
            string email = Request.Form["stylist-email"];
            Stylist stylist = new Stylist(name, phone, email);
            stylist.Save();
            return Redirect("/");
        }

    }
}
