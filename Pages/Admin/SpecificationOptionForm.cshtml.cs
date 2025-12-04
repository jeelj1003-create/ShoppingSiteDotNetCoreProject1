using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class SpecificationOptionFormModel : PageModel
    {
        [BindProperty]
        public SpecificationOptionTblDTO SpecificationOptionTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
