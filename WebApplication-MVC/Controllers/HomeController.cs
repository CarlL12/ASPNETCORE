using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;
using WebApplication_MVC.Models.Views;

namespace WebApplication_MVC.Controllers
{
    public class HomeController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            var model = new SubscribeModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SubscribeModel model)
        {
            ViewData["Title"] = "Home";

            if (ModelState.IsValid)
            {
                using var http = new HttpClient();

                var url = $"https://localhost:7294/api/Subscriber?email={model.Email}&key={_configuration["ApiKey"]}";
                var request = new HttpRequestMessage(HttpMethod.Post, url);


                var response = await http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                }
                else
                {
                    ViewData["Status"] = "Connectionfailed";
                }
            }
            else
            {
                ViewData["Status"] = "Invalid";
            }
            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact us";
            return View();
        }

    }
}
