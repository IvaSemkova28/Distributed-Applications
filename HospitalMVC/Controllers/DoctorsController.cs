using Data.Entities;
using HospitalMVC.Models;
using HospitalWebAPI.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HospitalMVC.Controllers
{
    public class DoctorsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            HttpResponseMessage response;

            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();
            using (ApiClient client = new())
            {
                response = client.GetAsync("https://localhost:44378/api/Doctors").Result;
            }

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                doctorList = JsonConvert.DeserializeObject<List<DoctorViewModel>>(data);
            }

            return View(doctorList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Doctor model)
        {
            try
            {

                string url = "https://localhost:44378/api/Doctors";
                string result = JsonConvert.SerializeObject(model);
                StringContent content = new(result, Encoding.UTF8, "application/json");

                using (ApiClient client = new())
                {
                    var resp = client.PostAsync(url, content).Result;
                    Console.WriteLine(resp);
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            DoctorViewModel doctor = null;
            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44378/api/Doctors/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                }
            }
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor model)
        {
            try
            {
                string url = "https://localhost:44378/api/Doctors/" + model.Id;
                string result = JsonConvert.SerializeObject(model);
                StringContent content = new(result, Encoding.UTF8, "application/json");

                using (ApiClient client = new())
                {
                    var resp = client.PutAsync(url, content).Result;
                    Console.WriteLine(resp);
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {

            string url = "https://localhost:44378/api/Doctors/" + id;

            using (ApiClient client = new())
            {
                HttpResponseMessage resp = client.DeleteAsync(url).Result;
                Console.WriteLine(resp);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string url = "https://localhost:44378/api/Doctors/" + id;
            DoctorViewModel doctor = null;

            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    doctor = JsonConvert.DeserializeObject<DoctorViewModel>(json);
                }
                else
                {
                    return NotFound();
                }
            }

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }
    }
}
