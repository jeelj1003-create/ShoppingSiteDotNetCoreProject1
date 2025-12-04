using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class StateTblDTO
    {
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please Select Country.")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter State Name.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter State Name. only in alphabatic character in 1 to 50 size")]
        public string? State { get; set; }
    }
}
