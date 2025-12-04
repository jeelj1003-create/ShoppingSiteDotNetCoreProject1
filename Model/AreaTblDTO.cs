using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class AreaTblDTO
    {
        public int AreaId { get; set; }

        [Required(ErrorMessage = "Please Select Country.")]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "Please Select State.")]
        public int? StateId { get; set; }

        [Required(ErrorMessage = "Please Select City.")]
        public int? CityId { get; set; }

        [Required(ErrorMessage = "Please Enter Area.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Area. only in alphabatic character in 1 to 50 size")]
        public string? Area { get; set; }

        [Required(ErrorMessage = "Please Select any option.")]
        public string? Status { get; set; }
    }
}
