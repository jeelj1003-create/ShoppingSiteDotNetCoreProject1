using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class CityFormModel : PageModel
    {
        [BindProperty]
        public CityTblDTO CityTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
