using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class CountryFormModel : PageModel
    {
        [BindProperty]
        public CountryTblDTO CountryTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
