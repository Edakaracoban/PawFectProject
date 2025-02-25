using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class CreateMailModel
    {
        public string ReceiverMail { get; set; }
        public string Subject { get; set; } = "PawFect'e Hoşgeldiniz!";
        public string Name { get; set; }
        public string Body { get; set; }
    }

}
