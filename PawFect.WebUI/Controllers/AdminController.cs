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
                CategoryId = product.CategoryId

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
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            //if (file == null)
            //{
            //    ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
            //    ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            //    return View(model);
            //}
            //else
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName); // Benzersiz dosya adı
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", uniqueFileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await file.CopyToAsync(stream); // Dosya yükleniyor
            //    }
            //    model.Image = "/img/" + uniqueFileName;
            //}
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                if (model.CategoryId == -1 || model.CategoryId == null)
                {
                    ModelState.AddModelError("", "Lütfen bir kategori seçiniz.");
                    ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                    return View(model);
                }

                var entity = new Product()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock,
                    Image = model.Image,
                    CategoryId = model.CategoryId.Value

                };
                _productService.Create(entity);
                return RedirectToAction("ProductList");
            }
            ViewBag.Category = _categoryService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(model);
        }
        public IActionResult EditProduct(int id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetProductDetails(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Stock = entity.Stock,
                Image = entity.Image,
                CategoryId = entity.CategoryId.Value
            };

            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int categoryId)
        {
            var entity = _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Stock = model.Stock;
            entity.Image = model.Image;
            entity.CategoryId = categoryId;
            _productService.Update(entity, categoryId);
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var product = _productService.GetById(productId);
            if (product != null)
            {
                _productService.Delete(product);
            }
            return RedirectToAction("ProductList");
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel() { Categories = _categoryService.GetAll() });
        }
        public IActionResult CreateCategory()
        {
            return View(new CategoryModel());
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Id=model.Id,
                Name = model.Name,
                Icon=model.Icon
            };
            _categoryService.Create(entity);
            return RedirectToAction("CategoryList");
        }
        public IActionResult EditCategory(int? id)
        {
            var entity = _categoryService.GetByWithProducts(id.Value);
            return View(
                    new CategoryModel()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Icon = entity.Icon,
                        Products = entity.Products.ToList()
                    }
                );
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);

            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Icon = model.Icon;
            _categoryService.Update(entity);
            return RedirectToAction("CategoryList");
        }
        //[HttpPost]
        //public IActionResult DeleteCategory(int categoryId)
        //{
        //    var entity = _categoryService.GetById(categoryId);
        //    var productsWithCategory = _productService.GetProductsByCategoryId(categoryId);
        //    if (productsWithCategory.Any(p => p.CategoryId != null))
        //    {
        //        TempData["ErrorMessage"] = "Kategoriye ilişkin ürünler olduğu için kategori silinemez.";
        //        return RedirectToAction("CategoryList");
        //    }
        //    _categoryService.Delete(entity);
        //    return RedirectToAction("CategoryList");
        //}
    }
}
