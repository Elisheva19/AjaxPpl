using AjaxPpl.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AjaxPpl.Data;

namespace AjaxPpl.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
           @"Data Source=.\sqlexpress;Initial Catalog=12_27MyFirstDatabase;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            List<Person> people = repo.GetPeople();
            return Json(people);
        }


        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.Add(person);
            
        }

        [HttpPost]
        public void UpdatePerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.UpdatePerson(person);
        }

        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.Delete(id);
        }
    }
}
