using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class QuestionController : Controller
    {
        Database database = new Database();
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }
    }
}