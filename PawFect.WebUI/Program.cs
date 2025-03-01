using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawFect.Business.Abstract;
using PawFect.Business.Concrete;
using PawFect.DataAccess.Abstract;
using PawFect.DataAccess.Concrete.EfCore;
using PawFect.WebUI.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();
//SeedIdentity
var userManager = builder.Services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
var roleManager = builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();

builder.Services.Configure<IdentityOptions>(options =>
{ 
    // password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;

    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    var gmtPlus3Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.RequireUniqueEmail = true;

    // SignIn settings.
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

//Cookie Options
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie = new CookieBuilder
    {
        Name = "PawFect.Security.Cookie",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
    };
});


builder.Services.AddScoped<IProductDal, EfCoreProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IBlogDal, EfCoreBlogDal>();
builder.Services.AddScoped<IBlogService, BlogManager>();
builder.Services.AddScoped<ICartDal, EfCoreCartDal>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICommentDal, EfCoreCommentDal>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<IOrderDal, EfCoreOrderDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedDatabase.Seed(); // Database Seed , Product,Category,Blog
app.UseHttpsRedirection();
app.UseStaticFiles(); //wwwroot içindeki js css dosyalarý
app.UseAuthentication(); //kimlik doðrulama iþlemleri
app.UseAuthorization(); //yetkilendirme iþlemleri
app.UseRouting();

// endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "products",
        pattern: "products/{category}",
        defaults: new { controller = "Shop", action = "List" }
    );
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" }
    );
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/products/{id}",
        defaults: new { controller = "Admin", action = "EditProduct" }
    );
    endpoints.MapControllerRoute(
         name: "adminProducts",
         pattern: "admin/category",
         defaults: new { controller = "Admin", action = "CategoryList" }
    );
    endpoints.MapControllerRoute(
        name: "adminProducts",
        pattern: "admin/categories/{id}",
        defaults: new { controller = "Admin", action = "EditCategory" }
    );
    endpoints.MapControllerRoute(
        name: "cart",
        pattern: "cart",
        defaults: new { controller = "Cart", action = "Index" }
    );
    endpoints.MapControllerRoute(
        name: "checkout",
        pattern: "checkout",
        defaults: new { controller = "Cart", action = "Checkout" }
    );
    endpoints.MapControllerRoute(
       name: "orders",
       pattern: "orders",
       defaults: new { controller = "Cart", action = "GetOrders" }
   );

}
);


SeedIdentity.Seed(userManager, roleManager, app.Configuration).Wait();

app.Run();
