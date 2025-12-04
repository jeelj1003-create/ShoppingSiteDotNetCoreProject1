using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class CountryTblDTO
    {
        public int CountryIId { get; set; }

        [Required(ErrorMessage = "Please Enter Country Name.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Country Name. only in alphabatic character in 1 to 50 size")]

        public string? Country { get; set; }
        public string? Code { get; set; }
    }
}
