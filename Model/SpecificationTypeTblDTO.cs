using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class SpecificationTypeTblDTO
    {
        public int SpecificationId { get; set; }

        [Required(ErrorMessage = "Please Enter Specification Name.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Specification Name. only in alphabatic character in 1 to 50 size")]
        public string? SpecificationName { get; set; }

        [Required(ErrorMessage = "Please Choose the bellow option.")]
        public string? SpecificationType { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
    }
}
