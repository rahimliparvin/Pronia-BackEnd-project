using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.CategoryVM
{
	public class CategoryCreateVM
	{
		[Required]
		public string Name { get; set; }
	}
}
