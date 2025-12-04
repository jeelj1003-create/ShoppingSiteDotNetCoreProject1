using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class StateFormModel : PageModel
    {
        [BindProperty]
        public StateTblDTO StateTblDTO { get; set; }
        public void OnGet()
        {
        }
    }
}
