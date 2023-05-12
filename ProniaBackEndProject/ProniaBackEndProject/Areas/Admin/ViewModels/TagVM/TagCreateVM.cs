using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.TagVM
{
	public class TagCreateVM
	{
		[Required]
		public string Name { get; set; }
	}
}
