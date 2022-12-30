using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnicalTest.OpenAPIs;
using TechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TechnicalTest.Controllers
{
    public class HomeController : Controller
    {
       public static List<Person> _list;
        public HomeController() {
            LoadData();
        }

        public IActionResult Index()
        {
            if (_list == null)
                _list = new List<Person>();
            LoadData();

            return View(_list);
        }       

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (_list == null)
            {
                _list = new List<Person>();
                LoadData();
            }

            var personToEdit = _list.Find(p => p.Id == id);
            if (personToEdit==null)
            {
                personToEdit = new Person();
            }

            return View(personToEdit);
        }

        public IActionResult Details(int id)
        {
            if (_list == null)
            {
                _list = new List<Person>();
                LoadData();
            }

            var personToView = _list.Find(p => p.Id == id);
            if (personToView == null)
            {
                personToView = new Person();
            }

            return View(personToView);
        }

        public IActionResult Delete(int id)
        {
            if (_list == null)
            {
                _list = new List<Person>();
                LoadData();
            }

            var personToDelete = _list.Find(p => p.Id == id);
            if (personToDelete == null)
            {
                personToDelete = new Person();
            }

            return View(personToDelete);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Create(Person person)
        {
            var response = CreateAsync(person);
            ViewBag.Status = response.Status.ToString() + " - " + response.Result.UserInfo.ToString() + " - " + response.Id.ToString();
            Person personclear = new Person();
            ModelState.Clear();
            return View(personclear);
        }
        
        [HttpPost]
        public IActionResult Edit(Person person)
        {
            var response = EditAsync(person);
            ViewBag.Status = response.Status.ToString() + " - " + response.Result.UserInfo.ToString() + " - " + response.Id.ToString();
            var personToEdit = _list.Find(p => p.Id == person.Id);
            return View(personToEdit);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var response = DeleteAsync(id);
            ViewBag.Status = response.Status.ToString() + " - " + response.Result.UserInfo.ToString() + " - " + response.Id.ToString();
            LoadData();
            return RedirectToAction("Index");
        }


        #region AsyncMethods
        protected async void LoadData()
        {
            _list = await Api.GetPersonAsync("https://localhost:44345/api/person");
        }

        protected async Task<Uri> CreateAsync(Person person)
        {
            try
            {
                var status = await Api.CreatePersonAsync(person);
                LoadData();
                return status;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<Uri> EditAsync(Person person)
        {
            try
            {
                var status = await Api.EditPersonAsync(person);
                LoadData();
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<Uri> DeleteAsync(int id)
        {
            try
            {
                var status = await Api.DeletePersonAsync(id);
                LoadData();
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
