using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class SpecificationFormModel : PageModel
    {
        [BindProperty]
        public SpecificationTypeTblDTO SpecificationTypeTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
