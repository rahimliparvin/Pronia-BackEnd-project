using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.TagVM
{
    public class TagEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
