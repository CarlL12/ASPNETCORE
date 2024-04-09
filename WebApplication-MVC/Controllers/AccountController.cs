
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApplication_MVC.Models.Views;

namespace WebApplication_MVC.Controllers
{


    [Authorize]
    public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressService addressService, IConfiguration configuration, HttpClient httpClient) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly AddressService _addressService = addressService;
        private readonly IConfiguration _configuration = configuration;
        private readonly HttpClient _httpClient = httpClient;

        [HttpGet]
        [Route("/account/details")]
        public async Task<IActionResult> Details()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var viewModel = new DetailsViewModel();

            if (viewModel.BasicInfo == null)
                viewModel.BasicInfo = await PopulateBasicInfoAsync();

            if (viewModel.AddressInfo == null)
                viewModel.AddressInfo = await PopulateAddressInfoAsync();

            viewModel.ProfileInfo = await PopulateProfileInfoasync();

            return View(viewModel);
        }


        [HttpPost]
        [Route("/account/details")]
        public async Task<IActionResult> Details(DetailsViewModel viewModel)
        {

            if (viewModel.BasicInfo != null)
            {
                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.FirstName = viewModel.BasicInfo.FirstName;
                        user.LastName = viewModel.BasicInfo.LastName;
                        user.PhoneNumber = viewModel.BasicInfo.PhoneNumber;
                        user.Email = viewModel.BasicInfo.Email;
                        user.Biography = viewModel.BasicInfo.Biography;
                        user.IsExternalAccount = viewModel.BasicInfo.IsExternalAccount;

                        var result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                            ViewData["ErrorMessage"] = "Something went wrong!";

                        }
                    }
                }
            }

            if (viewModel.AddressInfo != null)
            {
                if (viewModel.AddressInfo.Address1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {


                    var addressResult = await addressService.GetOrCreateAddress(viewModel.AddressInfo);

                    if (addressResult != null)
                    {
                        var user = await _userManager.GetUserAsync(User);
                        if (user != null)
                        {
                            user.AddressId = addressResult.Id;

                            var result = await _userManager.UpdateAsync(user);

                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                                ViewData["ErrorMessage"] = "Something went wrong!";

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                            ViewData["ErrorMessage"] = "Something went wrong!";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                        ViewData["ErrorMessage"] = "Something went wrong!";
                    }

                }
            }


            if (viewModel.BasicInfo == null)
                viewModel.BasicInfo = await PopulateBasicInfoAsync();

            if (viewModel.AddressInfo == null)
                viewModel.AddressInfo = await PopulateAddressInfoAsync();

            viewModel.ProfileInfo = await PopulateProfileInfoasync();

            return View(viewModel);
        }

        [HttpGet]
        [Route("/account/security")]
        public async Task<ActionResult> Security()
        {
            var viewModel = new SecurityViewModel();

            if (viewModel.BasicInfo == null)
                viewModel.BasicInfo = await PopulateBasicInfoAsync();

            viewModel.Profile = await PopulateProfileInfoasync();

            return View(viewModel);
        }

        [HttpPost]
        [Route("/account/security")]
        public async Task<IActionResult> Security(SecurityViewModel model)
        {

            if (model.SecurityInfo != null)
            {

                if (model.SecurityInfo.Password != null && model.SecurityInfo.NewPassword != null && model.SecurityInfo.ConfirmPassword != null)
                {
                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, model.SecurityInfo.Password, model.SecurityInfo.NewPassword);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                            ViewData["ErrorMessage"] = "Something went wrong!";
                        }
                    }
                }

            }
            else
            {

                ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                ViewData["ErrorMessage"] = "Something went wrong!";

            }

            if (model.Delete != null)
            {
                if (model.Delete.Delete == true)
                {
                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var result = await _userManager.DeleteAsync(user);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong!");
                            ViewData["ErrorMessage"] = "Something went wrong!";
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            return View(model);

        }

        public async Task<IActionResult> SavedCourses()
        {
            ViewData["Title"] = "Saved courses";

            var viewModel = new SavedCoursesViewModel();

            if (viewModel.BasicInfo == null)
                viewModel.BasicInfo = await PopulateBasicInfoAsync();

            viewModel.Profile = await PopulateProfileInfoasync();

            return View(viewModel);
        }

        public async Task<IActionResult> Courses(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
        {
            ViewData["Title"] = "Courses";

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                CoursesViewModel viewModel = new CoursesViewModel();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseCourses = await _httpClient.GetAsync($"https://localhost:7294/api/Courses?key={_configuration["ApiKey"]}&category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");

                var jsonCourses = await responseCourses.Content.ReadAsStringAsync();
                var dataCourses = JsonConvert.DeserializeObject<CourseResult>(jsonCourses);
                if(dataCourses != null)
                {
                    viewModel.Courses = dataCourses!.Course;
                    viewModel.Pageination = new PaginationModel
                    {
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = dataCourses.TotaltPages,
                        TotalItems = dataCourses.TotalItems
                    };
                }
                

                var responseCategory = await _httpClient.GetAsync($"https://localhost:7294/api/Category?key={_configuration["ApiKey"]}");
                var jsonCategory = await responseCategory.Content.ReadAsStringAsync();
                var dataCategory = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(jsonCategory);
                viewModel.Categories = dataCategory;


                return View(viewModel);
            }

            return View();


        }



        public async Task<IActionResult> SingleCourse(int id)
        {

            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                SingleCourseViewModel viewModel = new SingleCourseViewModel();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseCourse = await _httpClient.GetAsync($"https://localhost:7294/api/Courses/{id}?key={_configuration["ApiKey"]}");

                var jsonCourse = await responseCourse.Content.ReadAsStringAsync();
                var dataCourse = JsonConvert.DeserializeObject<CourseModel>(jsonCourse);

                viewModel.Course = dataCourse!;

                return View(viewModel);
            }

                return View();
        }


        private async Task<DetailsViewModel> PopulateDetailsViewModel()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                DetailsViewModel viewModel = new DetailsViewModel
                {
                    BasicInfo = new BasicInfoModel
                    {
                        AddressId = user.AddressId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email!,
                        PhoneNumber = user.PhoneNumber,
                        Biography = user.Biography,
                        IsExternalAccount = user.IsExternalAccount
                    },

                    ProfileInfo = new ProfileInfoModel
                    {
                        ProfileImage = user.ProfileImage,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email!
                    },

                    AddressInfo = new AddressInfoModel()
                };

                if (viewModel.BasicInfo.AddressId == null)
                {
                    return viewModel;
                }
                else
                {
                    var result = await addressService.GetAddressAsync(viewModel.BasicInfo.AddressId);

                    if (result != null)
                    {
                        viewModel.AddressInfo.Address1 = result.Address1;
                        viewModel.AddressInfo.Address2 = result.Address2;
                        viewModel.AddressInfo.City = result.City;
                        viewModel.AddressInfo.PostalCode = result.PostalCode;

                    }

                    return viewModel;
                }

            }

            return null!;
        }

        private async Task<BasicInfoModel> PopulateBasicInfoAsync()
        {
            var viewModel = await PopulateDetailsViewModel();

            if (viewModel.BasicInfo != null)
            {
                var infoModel = new BasicInfoModel
                {
                    FirstName = viewModel.BasicInfo.FirstName,
                    LastName = viewModel.BasicInfo.LastName,
                    Email = viewModel.BasicInfo.Email,
                    PhoneNumber = viewModel.BasicInfo.PhoneNumber,
                    Biography = viewModel.BasicInfo.Biography,
                    AddressId = viewModel.BasicInfo.AddressId,
                    IsExternalAccount = viewModel.BasicInfo.IsExternalAccount
                };

                return infoModel;
            }
            else
            {
                var model = new BasicInfoModel();

                return model;
            }

        }

        private async Task<ProfileInfoModel> PopulateProfileInfoasync()
        {
            var viewModel = await PopulateDetailsViewModel();

            if (viewModel.ProfileInfo != null)
            {
                var infoModel = new ProfileInfoModel
                {
                    FirstName = viewModel.ProfileInfo.FirstName,
                    LastName = viewModel.ProfileInfo.LastName,
                    Email = viewModel.ProfileInfo.Email,
                    ProfileImage = viewModel.ProfileInfo.ProfileImage,
                };

                return infoModel;
            }
            else
            {
                var model = new ProfileInfoModel();

                return model;
            }
        }

        private async Task<AddressInfoModel> PopulateAddressInfoAsync()
        {
            var viewModel = await PopulateDetailsViewModel();

            if (viewModel.AddressInfo != null)
            {
                var infoModel = new AddressInfoModel
                {
                    Address1 = viewModel.AddressInfo.Address1,
                    Address2 = viewModel.AddressInfo.Address2,
                    PostalCode = viewModel.AddressInfo.PostalCode,
                    City = viewModel.AddressInfo.City

                };

                return infoModel;
            }
            else
            {
                var model = new AddressInfoModel();

                return model;
            }

        }
    }
}
