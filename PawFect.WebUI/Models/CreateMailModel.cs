using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class CreateMailModel
    {
        public int Id { get; set; }
        // Kullanıcı ID'si
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ReceiverMail { get; set; }
        [Required]
        public string Subject { get; set; } = "PawFect'e Hoşgeldiniz!";
        [Required]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsSent { get; set; } = false;
        public DateTime? SentAt { get; set; }
        public string StatusMessage { get; set; }
        public void MarkAsSent()
        {
            IsSent = true;
            SentAt = DateTime.Now;
            StatusMessage = "Mail başarıyla gönderildi.";
        }
    }
}
