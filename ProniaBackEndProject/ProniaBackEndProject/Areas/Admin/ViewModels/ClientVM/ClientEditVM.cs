using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels
{
	public class ClientEditVM
	{
		[Required]
		public string Name { get; set; }
        [Required]
        public string Description { get; set; }
		public string Image { get; set; }
		public IFormFile Photo { get; set; }
	}
}
