using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;

namespace MvcApplication2.Controllers
{
    public class Thing
    {
        public string Word { get; set; }
    }

    public class DemoController : Controller
    {
        public ActionResult Index()
        {
            Thing t = new Thing();
            t.Word = GetRandomText();
            return View(t);
        }

        [HttpPost]
        public void Post(int value)
        {
            Response.Write("<h1>" + value + "</h1>");
        }

        private string GetRandomText()
        {
            Random rnd = new Random();
            List<char> chars = new List<char>();
            for (int i = 1; i <= 20; i++)
            {
                chars.Add((char)rnd.Next(65, 90));
            }

            return new String(chars.ToArray());
        }

    }
}
