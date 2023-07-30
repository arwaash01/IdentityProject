using IdentityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager <IdentityUser> _userManager;
        private readonly RoleManager <IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRoles()
        {
            //check
            var roles = new[] { "Admin", "Recep","Manger" };
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole(role));

            }
            return View("index", "roles added successfully");
        }
        public async Task<IActionResult> AddRoleToUsers()
        {
            var arwa = await _userManager.FindByNameAsync("arwa@gmail.com");
            await _userManager.AddToRoleAsync(arwa, "Admin");

            var ahmad = await _userManager.FindByNameAsync("ahmad@gmail.com");
            await _userManager.AddToRoleAsync(ahmad, "Recep");

            var khaled = await _userManager.FindByNameAsync("khaled@gmail.com");
            await _userManager.AddToRoleAsync(khaled,"C");
            return View("index","users added successfully");
        }
            public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //by defult : every one can enter
        public IActionResult FirstPage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")] // it should be log in to open this page
        public IActionResult SecondPage()
        {
            return View();
        }

        [Authorize (Roles = "Recep,Admin,Manger")] // it should has a role to enter
        public IActionResult ThirdPage()
        {
             
            return View();
        }





    }
}