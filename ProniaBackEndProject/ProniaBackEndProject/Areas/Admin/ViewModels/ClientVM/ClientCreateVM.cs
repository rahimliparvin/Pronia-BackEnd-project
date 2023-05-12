using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels
{
	public class ClientCreateVM
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public IFormFile Photo { get; set; }

	}
}
