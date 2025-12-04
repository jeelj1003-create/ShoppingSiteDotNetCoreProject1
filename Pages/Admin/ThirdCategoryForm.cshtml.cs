using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class ThirdCategoryFormModel : PageModel
    {
        [BindProperty]
        public ThirdCategoryTblDTO ThirdCategoryTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
