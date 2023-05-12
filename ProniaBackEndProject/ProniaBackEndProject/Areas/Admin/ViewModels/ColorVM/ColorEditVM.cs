using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.ColorVM
{
    public class ColorEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
