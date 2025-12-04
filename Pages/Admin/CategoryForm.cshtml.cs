using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;
using ShoppingSiteDotNetCore.NTier;
using System.Threading.Tasks;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class CategoryFormModel : PageModel
    {
        private readonly ICategoryTblServices db;
        public CategoryFormModel(ICategoryTblServices db)
        {
            this.db = db;
        }
        [BindProperty]
        public CategoryTblDTO CategoryTblDTO { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        //public async Task EditData()
        //{
        //    var ResponseData = await db.GetByCatId(CategoryTblDTO.CatId);
        //    if (ResponseData != null)
        //    {
        //        if (ResponseData.ContainsKey("Data"))
        //        {
        //            CategoryTblDTO = (CategoryTblDTO)ResponseData["Data"];
        //        }
        //    }
        //}

        public async Task<IActionResult> OnGet(int EditId)
        {

            if (EditId > 0)
            {
                var ResponseData = await db.GetByCatId(EditId);
                if (ResponseData != null)
                {
                    if (ResponseData.ContainsKey("Model"))
                    {

                        CategoryTblDTO = (CategoryTblDTO)ResponseData["Model"];
                    }
                }
            }
            return Page();
        }

        //Insert Category
        public async Task<IActionResult> OnPostCreate()
        {
            var ResponseData = await db.AddCategory(CategoryTblDTO);
            if (!string.IsNullOrEmpty(ResponseData))
            {
                Message = ResponseData;
            }
            else
            {
                Error = ResponseData;
            }
            return Page();
        }
    }
}
