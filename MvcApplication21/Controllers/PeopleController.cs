using MvcApplication21.Models;
using People.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcApplication21.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index(string searchQuery)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            IEnumerable<Person> people;
            IndexViewModel viewModel = new IndexViewModel();
            if (String.IsNullOrEmpty(searchQuery))
            {
                people = manager.GetAll();
            }
            else
            {
                people = manager.SearchByLastName(searchQuery);
                viewModel.SearchQuery = searchQuery;
            }
            viewModel.People = people;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(string firstName, string lastName, int age)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.Add(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            });
            return Redirect("/people/index");
        }

        [HttpPost]
        public ActionResult Edit(string firstName, string lastName, int age, int id)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.Update(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Id = id
            });
            return Redirect("/people/index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.Delete(id);
            return Redirect("/people/index");
        }

    }
}
