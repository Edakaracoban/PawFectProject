using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
    }
}
