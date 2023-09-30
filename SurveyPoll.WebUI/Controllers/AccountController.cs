using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyPoll.WebUI.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveyPoll.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        //private readonly RoleManager<Role> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateViewModel model)
            {
            Dictionary<string, List<string>> errors;
            if (ModelState.IsValid)
            {

                var userExist = await _userManager.FindByNameAsync(model.Email);
                if (userExist == null)
                {

                    AppUser user = new()
                    {
                        UserName = model.Email,
                        Name = model.FirstName,
                        Surname = model.LastName,
                        Email = model.Email,
                        Status = model.Status,
                        CreatedDate = model.CreatedDate
                    };

                    var identityResult = await _userManager.CreateAsync(user, model.Password);
                    if (identityResult.Succeeded)
                    {
                        //var roleExists = await _roleManager.RoleExistsAsync("User");

                        return Json(new { isValid = true,message="Giriş ekranına yönlendiriliyorsunuz."});

                    }
                 
                }
                else
                {
                    errors = new Dictionary<string, List<string>> {
    { "Email", new List<string> { "Bu e-posta adresinde bir kullanıcı zaten mevcut !" } }
};
                    return Json(new { isValid = false,errors });
                }
            }
            // Veri doğrulama başarısız ise ModelState hatalarını JSON olarak döndürün
            errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

            return Json(new { isValid = false, errors = errors });
        }


        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
         {
            Dictionary<string, List<string>> errors;
            if (ModelState.IsValid)
            {
              var signInResult=  await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

                if (signInResult.Succeeded)
                {
                    return Json(new { isValid = true, status = true, message = "Anasayfaya yönlendiriliyorsunuz..." });
                }
                else if (signInResult.IsLockedOut)
                {
                    //hesap birkaç defa üst üste yanlış bilgi girildiği için kilitliyse
                    return Json(new { isValid = true, status = false, errors = "Üst üste çok fazla hatalı giriş yaptınız ! Hesabınız geçici bir süre kısıtlanmıştır." });

                }
                else
                {
                    return Json(new { isValid = true,status=false,errors="E-posta veya parola yanlış !" });
                }
            }
            errors = ModelState.ToDictionary(
              kvp => kvp.Key,
              kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

            return Json(new { isValid = false, errors = errors });
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); // Oturum sonlandıktan sonra yönlendirme yapabilirsiniz
        }
    }
}
