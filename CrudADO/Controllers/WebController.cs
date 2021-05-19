using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudADO.Controllers
{
    public class WebController : Controller
    {
        protected IActionResult ToViewWithMessage(string view, string msg)
        {
            ViewData["Message"] = msg;
            return View(view);
        }
        protected IActionResult ToViewWithError(string view, string error)
        {
            ViewData["Error"] = error;
            return View(view);
        }
    }
}
