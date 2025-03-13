using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PawFect.Business.Abstract;
using PawFect.WebUI.EmailService;
using PawFect.WebUI.Identity;
using PawFect.WebUI.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace PawFect.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private IProductService _productService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AdminController(IProductService productService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult ProductList(int page = 1)
        {
            int pageSize = 10; //Her sayfada 10 ürün olsun

            var productList = _productService.GetAll(); 
          
            var productModelList = productList.Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Category = product.Category 
            }).ToList();

            IPagedList<ProductModel> pagedProducts = productModelList.ToPagedList(page, pageSize);  // Dönen product modelleri listeliyoruz.

            return View(pagedProducts);
        }
        public async Task<IActionResult> AdminManage()
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
        public async Task<IActionResult> AdminManage(AccountModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
       
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
                string siteUrl = "https://localhost:7197";
                string resetUrl = $"{siteUrl}{callbackUrl}";
                //send email
                string body = $"Şifrenizi yenilemek için linke <a href='{resetUrl}'> tıklayınız.</a>";

                MailHelper.SendEmail(body, model.Email, "PAWFECT Şifre Sıfırlama");

                return View(model);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);//kullanıcı bilgilerini güncelledikten sonra oturumu günceller.

                return RedirectToAction("ProductList", "Admin");
            }
            return View(model);
        }
    }


}