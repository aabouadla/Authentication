using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "ABED"),
                new Claim(ClaimTypes.Email, "aabouadla@gmail.com"),
                new Claim("Grandma.Says", "Very nice boi."),
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Abdulrahman Abou Adla"),
                new Claim(ClaimTypes.Email, "aabouadla@gmail.com"),
                new Claim("DrivingLicense", "A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var LicenseIdentity = new ClaimsIdentity(licenseClaims, "Govermet");

            var userPrinciple = new ClaimsPrincipal(new[] { grandmaIdentity, LicenseIdentity});

            HttpContext.SignInAsync(userPrinciple);
            
            return RedirectToAction("Index");
        }
    }
}
