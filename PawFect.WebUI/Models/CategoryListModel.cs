using PawFect.Entities;
using System.ComponentModel.DataAnnotations;

namespace PawFect.WebUI.Models
{
    public class CategoryListModel
    {
        [Required]
        public List<Category> Categories { get; set; }
    }
}
