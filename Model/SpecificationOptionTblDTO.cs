using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class SpecificationOptionTblDTO
    {
        public int SpecificationId { get; set; }

        [Required(ErrorMessage = "Please Choose the bellow option.")]
        public string? Specification { get; set; }

        [Required(ErrorMessage = "Please Enter the Value.")]
        public string? Value { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
    }
}
