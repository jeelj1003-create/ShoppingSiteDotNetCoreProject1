using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class CityTblDTO
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "Please Select Country.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Please Select State.")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please Enter City Name.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter City Name. only in alphabatic character in 1 to 50 size")]
        public string? City { get; set; }
    }
}
