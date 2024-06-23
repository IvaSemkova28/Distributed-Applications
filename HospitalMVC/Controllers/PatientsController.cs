using Azure;
using HospitalWebAPI.Authentication;
using Data.Entities;
using HospitalMVC.Models;
using HospitalWebAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HospitalMVC.Controllers
{
    public class PatientsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            string requestUrl = "https://localhost:44378/api/Patients";

            if (!string.IsNullOrEmpty(searchString))
            {
                requestUrl = $"https://localhost:44378/api/Patients/{searchString}";
            }

            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    if (data.Trim().StartsWith("["))
                    {
                        patientList = JsonConvert.DeserializeObject<List<PatientViewModel>>(data);
                    }
                    else
                    {
                        var singlePatient = JsonConvert.DeserializeObject<PatientViewModel>(data);
                        if (singlePatient != null)
                        {
                            patientList.Add(singlePatient);
                        }
                    }
                }
                else
                {
                    TempData["Error"] = $"Error retrieving patient list. Status code: {response.StatusCode}";
                }

                ViewData["SearchString"] = searchString;
                return View(patientList);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Patient model)
        {
            try
            {

                string url = "https://localhost:44378/api/Patients";
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
            PatientViewModel patient = null;
            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44378/api/Patients/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    patient = JsonConvert.DeserializeObject<PatientViewModel>(data);
                }
            }
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient model)
        {
            try
            {
                string url = "https://localhost:44378/api/Patients/" + model.Id;
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

            string url = "https://localhost:44378/api/Patients/" + id;

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
            string url = "https://localhost:44378/api/Patients/" + id;
            PatientViewModel patient = null;

            using (ApiClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    patient = JsonConvert.DeserializeObject<PatientViewModel>(json);
                }
                else
                {
                    return NotFound();
                }
            }

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }


    }

}

