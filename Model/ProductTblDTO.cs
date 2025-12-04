using System.ComponentModel.DataAnnotations;

namespace ShoppingSiteDotNetCore.Model
{
    public class ProductTblDTO
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Select SubCategory.")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Please Select ThirdCategory.")]
        public int ThirdCategoryId { get; set; }

        [Required(ErrorMessage = "Please Select Brand.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Please enter Product Name. only in alphabatic character in 1 to 50 size")]
        public string? Product { get; set; }

        [Required(ErrorMessage = "Please Enter Price.")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Please upload the icon.")]
        public IFormFile? Icon { get; set; }
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please Select Sany option.")]
        public string? Status { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
