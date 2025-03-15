using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawFect.Business.Abstract;
using PawFect.Entities;
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
        private ICategoryService _categoryService;
        private IOrderService _orderService;
        public AdminController(IProductService productService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICategoryService categoryService, IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
            _categoryService = categoryService;
            _orderService = orderService;
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


        public IActionResult CreateProduct()
        {
            var category = _categoryService.GetAll();
            ViewBag.Category = category.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            return View(new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model, IFormFile file)
        {
            // Model validasyon kontrolü
            if (ModelState.IsValid)
            {
                // Kategori seçilmemişse hata ekle
                if (model.CategoryId == null)
                {
                    ModelState.AddModelError("", "Lütfen bir kategori seçiniz.");
                    ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                    return View(model);
                }

                // Ürün oluşturuluyor
                var entity = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock,
                    CategoryId = model.CategoryId,
                };

                // Eğer dosya yüklenmemişse, hata ver
                if (file == null)
                {
                    ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
                    ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                    return View(model);
                }

                // Dosya yükleme dizini
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                // Eğer dizin yoksa oluştur
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var fileName = file.FileName;
                var filePath = Path.Combine(uploadDirectory, fileName);

                // Dosyayı kaydetme işlemi
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Yüklenen resmin yolunu modeldeki Image alanına atıyoruz
                model.Image = "/img/" + fileName;

                // Ürünü veritabanına kaydediyoruz
                entity.Image = model.Image;

                // Kategoriyi güncelle
                var category = _categoryService.GetById(model.CategoryId.Value);
                if (category != null)
                {
                    category.Products.Add(entity); // Ürünü kategoriye ekliyoruz
                    _categoryService.Update(category); // Kategoriyi güncelliyoruz
                }

                _productService.Create(entity);

                return RedirectToAction("ProductList");
            }

            // Model geçerli değilse kategori listesi tekrar doldur
            ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(model);
        }





    }



}


