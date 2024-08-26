using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Pendientes.Models;

namespace Pendientes.Controllers
{
    public class PendientesController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:7113/api/Pendientes";

        public PendientesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync(ApiUrl);
            var tasks = JsonConvert.DeserializeObject<List<PendientesModel>>(response);
            return View(tasks);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}/{id}");
            var task = JsonConvert.DeserializeObject<PendientesModel>(response);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PendientesModel pendientesModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pendientesModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiUrl}/{pendientesModel.ID}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Unable to update the task.");
            return View(pendientesModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PendientesModel pendientesModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pendientesModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Unable to create the task.");
            return View(pendientesModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}/{id}");
            var Pendientes = JsonConvert.DeserializeObject<PendientesModel>(response);
            return View(Pendientes);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Unable to delete the task.");
            return RedirectToAction("Index");
        }
    }
}
