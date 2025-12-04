using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class SubCategoryFormModel : PageModel
    {
        [BindProperty]
        public SubCategoryTblDTO SubCategoryTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
