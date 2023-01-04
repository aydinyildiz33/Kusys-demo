using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        //readonly UserManager<Users> _userManager;
        //readonly SignInManager<Users> _signInManager;
        //public UserController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        public UserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult login()
        {
            Users users = new Users();
            return View(users);
        }


        [HttpPost]
        public async Task<ActionResult> login(Users model)
        {
            Users user = _context.Users.FirstOrDefault(i => i.Username == model.Username && i.Password == model.Password);

            bool kuladi = user.Username.Equals(model.Username, StringComparison.Ordinal);
            bool sfr = user.Password.Equals(model.Password, StringComparison.Ordinal);


            if (user != null && kuladi == true && sfr == true)
            {
                //var claims = new List<Claim>()
                //{
                //        new Claim(ClaimTypes.Name,model.Username)
                //};

                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.id)),
                    //new Claim(ClaimTypes.Name,user.Username),
                    new Claim(ClaimTypes.Role,user.Role)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Students");

            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }

            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
