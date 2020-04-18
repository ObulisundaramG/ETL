using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETL_UI.Models;

namespace ETL_UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Validate(Account _account)
        {
            return RedirectToAction("Index","Home");
        }
    }
}