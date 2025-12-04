using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class ProductFormModel : PageModel
    {
        [BindProperty]
        public ProductTblDTO ProductTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
