using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using PawFect.WebUI.Models;
using PawFect.WebUI.EmailService;

namespace PawFect.Controllers
{
    public class MailController : Controller
    {
        [HttpPost]
        public IActionResult SendMail(CreateMailModel createMailModel)
        {
            try
            {
                // E-posta içeriği oluşturuluyor
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h1 style='color: #4CAF50;'>Tebrikler {createMailModel.Name}!</h1>
                    <p>Bizden <strong>%25 indirim kodu</strong> kazandınız!</p>
                    <p>Siparişinizde bu kodu kullanarak avantajlardan faydalanabilirsiniz:</p>
                    <div style='padding: 10px; background-color: #f9f9f9; border: 1px solid #ddd; display: inline-block;'>
                        <strong style='font-size: 20px;'>INDIRIM25</strong>
                    </div>
                    <p>Keyifli alışverişler dileriz,<br><strong>PawFect Ekibi</strong></p>
                </div>";

                // HTML body'den bir string oluşturuyoruz
                string body = bodyBuilder.HtmlBody;

                // E-posta gönderme işlemi
                bool mailSent = MailHelper.SendEmail(body, createMailModel.ReceiverMail, "Tebrikler! %25 İndirim Kodunuzu Kazandınız");

                if (mailSent)
                {
                    // Mail başarıyla gönderildiyse, anasayfaya yönlendirme
                    TempData["message"] = "Mail başarıyla gönderildi!";
                }
                else
                {
                    // E-posta daha önce gönderildiyse, TempData ile hata mesajı
                    TempData["message"] = $"E-posta adresine ({createMailModel.ReceiverMail}) daha önce e-posta gönderildi.";
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Hata oluşursa, hata mesajı
                TempData["message"] = "Bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }

}
