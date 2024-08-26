using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pendientes.Models;

namespace Pendientes.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var loginRequest = new
            {
                Username = userLogin.Username,
                Password = userLogin.Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7113/AuthPendientes/token", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<dynamic>(responseString).Token;

                // Guardar el token en una cookie o almacenamiento local según sea necesario
                //HttpContext.Session.SetString("JWT", token);
                return RedirectToAction("Index", "Pendientes");
            }

            ModelState.AddModelError("", "Login failed");
            return View();
        }
    }
}