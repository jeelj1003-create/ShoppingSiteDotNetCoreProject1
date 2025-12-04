using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class BrandFormModel : PageModel
    {
        [BindProperty]
        public BrandTblDTO BrandTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
