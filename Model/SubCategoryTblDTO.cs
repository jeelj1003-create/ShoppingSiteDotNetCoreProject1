using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class SubCategoryTblDTO
    {
        public int SubCatId { get; set; }

        [Required(ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter SubCategory.")]
        public string? SubCategory { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
    }
}
