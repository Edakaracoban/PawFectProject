using PawFect.Business.Abstract;
using PawFect.WebUI.EmailService;
using PawFect.WebUI.Identity;
using PawFect.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;

namespace PawFect.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);//password hashlenmiş olarak tutulur.

            if (result.Succeeded)
            {
                //generate email confirmation token //emaile onay maili gönderme
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                string siteUrl = "https://localhost:7197";
                string activeUrl = $"{siteUrl}{callbackUrl}";
                //send email
                string body = $"Hesabınızı onaylayınız. <br> <br> Lütfen email hesabını onaylamak için linke <a href='{activeUrl}'> tıklayınız.</a>"; //tıklayınıza hperlink oluşturur.

                //Email Service
                MailHelper.SendEmail(body, model.Email, "PAWFECT Hesap Aktifleştirme Onayı");//mailhelper sınıfı static olduğu için direkt adı ile erişim sağlayabiliriz.
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token) //callback url dönecek.
        {
            if (userId == null || token == null)
            {
                return Redirect("~");//anasayfaya yönlendir.
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token); // email confirmed ı 1 yapar.
                if (result.Succeeded)
                {
                    _cartService.InitialCart(userId);
                    return RedirectToAction("Login", "Account");//login sayfasına yönlendir.
                }
            }
            return Redirect("~");//anasayfaya yönlendir.
        }

        public IActionResult Login(string returnUrl = null) //default değeri null
        {
            return View(
                new LoginModel()
                {
                    ReturnUrl = returnUrl
                }
             );
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            ModelState.Remove("ReturnUrl");

            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Bu email adresi ile kayıtlı kullanıcı bulunamadı");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (result.Succeeded)
            {
                // Admin kontrolü
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Admin", "ProductList"); // Admin sayfasına yönlendirme
                }
                else
                {
                    // Eğer admin değilse, normal sayfaya yönlendir
                    return Redirect(model.ReturnUrl ?? "~/");
                }
            }
            else if (result.IsLockedOut)
            {
                return View(model);
            }

            ModelState.AddModelError("", "Email veya şifre hatalı");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");//login sayfasına yönlendir
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
            
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                string siteUrl = "https://localhost:7076";
                string resetUrl = $"{siteUrl}{callbackUrl}";
                //send email
                string body = $"Şifrenizi yenilemek için linke <a href='{resetUrl}'> tıklayınız.</a>"; //tıklayınıza hperlink oluşturur.
                //Email Service
                MailHelper.SendEmail(body, email, "ETRADE Şifre Sıfırlama");//mailhelper sınıfı static olduğu için direkt adı ile erişim sağlayabiliriz.
              
                return RedirectToAction("Login");
            }
         
            return View();
        }
        public IActionResult ResetPassword(string token)
        {
            if (token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPasswordModel { Token = token };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
               
                return RedirectToAction("Home", "Index");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
               
                return View(model);
            }
        }
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
              
                return View();
            }

            var model = new AccountModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(AccountModel model)
        {

            if (!ModelState.IsValid)
            {

               
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
              
                return RedirectToAction("Login", "Account");
            }




            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;

            if (model.Email != user.Email)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                string siteUrl = "https://localhost:7076";
                string resetUrl = $"{siteUrl}{callbackUrl}";
                //send email
                string body = $"Şifrenizi yenilemek için linke <a href='{resetUrl}'> tıklayınız.</a>";

                MailHelper.SendEmail(body, model.Email, "ETRADE Şifre Sıfırlama");
              
                return RedirectToAction("Login");
            }

            var result = await _userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);//kullanıcı bilgilerini güncelledikten sonra oturumu günceller.
              
                return RedirectToAction("Index", "Home");
            }

         
            return View(model);
        }

    }


}

