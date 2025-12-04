using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class AreaFormModel : PageModel
    {
        [BindProperty]
        public AreaTblDTO AreaTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
