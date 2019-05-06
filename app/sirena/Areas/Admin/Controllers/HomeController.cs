using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using sirena.Helpers;
using sirena.Infrustructure;
using sirena.Models;
using sirena.Models.DBModels;

namespace sirena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        SqlContext _db;

        public HomeController(SqlContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync("mykytchenko77@gmail.com", "My Subject", "Testing email service!!! Date: " + DateTime.Now);

            return RedirectToAction(nameof(Index));
        }
    }
}