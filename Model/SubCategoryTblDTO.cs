using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class SubCategoryTblDTO
    {
        public int SubCatId { get; set; }

        [Required(ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter SubCategory.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Subategory only in alphabatic character in 1 to 50 size")]
        public string? SubCategory { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please Select Sany option.")]
        public string? Status { get; set; }
    }
}
