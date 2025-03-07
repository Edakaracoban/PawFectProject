//System.Net: Ağ ile ilgili işlemler için gerekli sınıfları içerir. Bu, örneğin SMTP (Simple Mail Transfer Protocol) ile e-posta göndermek için kullanılır.
//System.Net.Mail: E - posta ile ilgili sınıfları içerir. MailMessage ve SmtpClient gibi sınıflar burada tanımlıdır.
using System.Net;
using System.Net.Mail;

namespace PawFect.WebUI.EmailService
{
    public static class MailHelper //e-posta göndermek için kullanılan bir yardımcı sınıf olan MailHelper'ı içeriyor. 
    {
        // Geçici olarak gönderilen e-posta adreslerini tutan set
        private static HashSet<string> SentEmailAddresses = new HashSet<string>();

        // E-posta gönderme fonksiyonu
        public static bool SendEmail(string body, string to, string subject, bool isHtml = true)
        {
            // Eğer e-posta adresine daha önce gönderilmişse, göndermemek için false döneriz
            if (SentEmailAddresses.Contains(to))
            {
                return false;
            }

            // E-posta adresini ekliyoruz
            SentEmailAddresses.Add(to);

            // E-posta gönderme işlemi
            return SendEmail(body, new List<string> { to }, subject, isHtml);
        }

        // Birden fazla e-posta adresine e-posta gönderen fonksiyon
        private static bool SendEmail(string body, List<string> to, string subject, bool isHtml)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("ucuncubinyilakademimailservice@gmail.com");

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("ucuncubinyilakademimailservice@gmail.com", "wdpy prpp pekv nfll");
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }

            return result;
        }
    }
}