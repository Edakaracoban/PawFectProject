using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PawFect.Business.Abstract;
using PawFect.Entities;
using PawFect.WebUI.Session;
using System.Collections.Generic;
using System.Linq;

namespace PawFect.WebUI.Filters
{
    public class SessionCategoryFilter : IActionFilter
    {
        private readonly ICategoryService _categoryService;

        public SessionCategoryFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Kategorileri session'dan alıyoruz
            var categories = context.HttpContext.Session.GetObjectFromJson<List<Category>>("Categories");

            if (categories == null || !categories.Any())
            {
                // Eğer session'da kategori yoksa, kategorileri servis ile alıyoruz
                categories = _categoryService.GetAll();

                // Kategorileri session'a kaydediyoruz
                if (categories != null && categories.Any())
                {
                    context.HttpContext.Session.SetObjectAsJson("Categories", categories);
                }
            }
            //kategorileri session'da saklamak ve bu kategorileri her sayfa isteğinde kontrol ederek, session'dan alıp view'larda kullanılabilir hale getirmektir. Eğer kategoriler session'da bulunmazsa, kategori verileri veritabanı servisinden alınarak session'a kaydedilir ve ardından view'a aktarılır.

            // Controller'ı ViewData'ya erişmek için doğru bir şekilde casting yapıyoruz
            if (context.Controller is Controller controller)
            {
                controller.ViewData["Categories"] = categories; // Kategorileri ViewData'ya ekliyoruz
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // İşlem sonrası herhangi bir işleme gerek yok
        }
    }
}

//SESSION KULLANIM AMACI
/*Session Kullanımı:

Kategorilerin session'a kaydedilmesi sağlanarak, veritabanına her defasında istek göndermek yerine, bir defa veriler alındıktan sonra kullanıcıların sayfalarında hızlıca erişilebilen bir veri kaynağı oluşturulmuş olur.

Veritabanı Yükünü Azaltma:

Kategoriler ilk seferde veritabanından alınır, ancak sonrasındaki tüm isteklerde veriler session'dan alınır. Bu, her sayfa isteğinde veritabanına sorgu göndermeyi engeller ve performansı artırır.

Global Filtre:

Bu filtre, AddControllersWithViews() ile global olarak eklenebilir ve uygulamanın her sayfasında otomatik olarak çalışır. Böylece, tüm view'larda kategoriler session'dan alınır ve ViewData aracılığıyla view'a aktarılır.

/ViewData Kullanımı:

Kategoriler, view'lar arasında ViewData üzerinden paylaşılır. Bu, ViewData["Categories"] ile Razor view'larında erişilebilen bir veri sağlar.*/
