using Data.Entities;
using HospitalMVC.Models;
using HospitalWebAPI.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HospitalMVC.Controllers
{
    public class AppointmentsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            HttpResponseMessage response;

            List<AppointmentViewModel> appointmentList = new List<AppointmentViewModel>();
            using (ApiClient client = new())
            {
                response = client.GetAsync("https://localhost:44378/api/Appointments").Result;
            }

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                appointmentList = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(data);
            }

            return View(appointmentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Appointment model)
        {
            try
            {

                string url = "https://localhost:44378/api/Appointments";
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
            AppointmentViewModel appointment = null;
            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44378/api/Appointments/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(data);
                }
            }
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment model)
        {
            try
            {
                string url = "https://localhost:44378/api/Appointments/" + model.Id;
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

            string url = "https://localhost:44378/api/Appointments/" + id;

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
            string url = "https://localhost:44378/api/Appointments/" + id;
            AppointmentViewModel appointment = null;

            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(json);
                }
                else
                {
                    return NotFound();
                }
            }

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
    }
}
