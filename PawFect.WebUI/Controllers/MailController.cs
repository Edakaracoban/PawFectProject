
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using PawFect.WebUI.Models;

namespace FoodMartProject.Controllers
{
	public class MailController : Controller
	{
		[HttpPost]
		public IActionResult SendMail(CreateMailModel createMailModel)
		{
			MimeMessage mimeMessage = new MimeMessage();

			MailboxAddress mailboxAddressFrom = new MailboxAddress("PawFect", "adminuser@pawfect.com");
			mimeMessage.From.Add(mailboxAddressFrom);

			MailboxAddress mailboxAddressTo = new MailboxAddress(createMailModel.Name, createMailModel.ReceiverMail);
			mimeMessage.To.Add(mailboxAddressTo);

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                    <h1 style='color: #4CAF50;'>Tebrikler {createMailModel.Name}!</h1>
                    <p>Bizden <strong>%25 indirim kodu</strong> kazandınız!</p>
                    <p>Siparişinizde bu kodu kullanarak avantajlardan faydalanabilirsiniz:</p>
                    <div style='padding: 10px; background-color: #f9f9f9; border: 1px solid #ddd; display: inline-block;'>
                        <strong style='font-size: 20px;'>INDIRIM25</strong>
                    </div>
                    <p>Şimdi sipariş vermek için <a href='https://localhost:7197' style='color: #007BFF;'>web sitemizi</a> ziyaret edin.</p>
                    <p>Keyifli alışverişler dileriz,<br><strong>PawFect Ekibi</strong></p>
                </div>";

			// Mesajın Body'si
			mimeMessage.Body = bodyBuilder.ToMessageBody();

			mimeMessage.Subject = "Tebrikler! %25 İndirim Kodunuzu Kazandınız";

			SmtpClient client = new SmtpClient();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("ucuncubinyilakademimailservice@gmail.com", "wdpy prpp pekv nfll");

			client.Send(mimeMessage);
			client.Disconnect(true);

			TempData["Message"] = "Tebrikler! Mail başarıyla gönderildi.";
			return RedirectToAction("Index", "Default");
		}
	}
}
