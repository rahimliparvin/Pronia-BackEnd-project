using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.SizeVM
{
    public class SizeEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
