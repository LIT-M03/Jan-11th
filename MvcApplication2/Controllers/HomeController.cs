using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new PeopleDb(ConfigurationManager.ConnectionStrings["MvcApplication2.Properties.Settings.PeopleConStr"].ConnectionString);
            IEnumerable<Person> ppl = db.GetAll();
            return View(ppl);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string firstName, string lastName, int? age)
        {
            var db = new PeopleDb(ConfigurationManager.ConnectionStrings["MvcApplication2.Properties.Settings.PeopleConStr"].ConnectionString);
            db.AddPerson(firstName, lastName, age);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int pid)
        {
            var db = new PeopleDb(ConfigurationManager.ConnectionStrings["MvcApplication2.Properties.Settings.PeopleConStr"].ConnectionString);
            Person person = db.GetById(pid);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(int personId, string firstName, string lastName, int? age)
        {
            var db = new PeopleDb(ConfigurationManager.ConnectionStrings["MvcApplication2.Properties.Settings.PeopleConStr"].ConnectionString);
            Person p = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Id = personId
            };
            db.Update(p);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int pid)
        {
            var db = new PeopleDb(ConfigurationManager.ConnectionStrings["MvcApplication2.Properties.Settings.PeopleConStr"].ConnectionString);
            db.Delete(pid);
            return RedirectToAction("Index");
        }
    }
}
