using Microsoft.Extensions.FileProviders;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class CategoryTblDTO
    {
        public int CatId { get; set; }

        [Required(ErrorMessage = "Please Enter Category.")]
        //[RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage ="Please enter Category only in alphabatic character in 1 to 50 size")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please select any option")]
        public string? Status { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
