using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.ColorVM
{
	public class ColorCreateVM
	{
		[Required]
		public string Name { get; set; }
	}
}
