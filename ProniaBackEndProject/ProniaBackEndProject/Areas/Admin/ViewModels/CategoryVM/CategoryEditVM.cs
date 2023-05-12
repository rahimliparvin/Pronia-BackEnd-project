using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.CategoryVM
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
