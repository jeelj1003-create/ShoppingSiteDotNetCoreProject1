using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class ThirdCategoryTblDTO
    {
        public int ThirdCatId { get; set; }

        [Required(ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Select SubCategory.")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter ThirdCategory.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter ThirdCategory only in alphabatic character in 1 to 50 size")]
        public string? ThirdCategory { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
