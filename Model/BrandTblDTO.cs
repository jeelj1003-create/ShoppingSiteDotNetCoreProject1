using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class BrandTblDTO
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please Enter Brand.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Brand only in alphabatic character in 1 to 50 size")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
