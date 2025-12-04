using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingSiteDotNetCore.Model;
using ShoppingSiteDotNetCore.NTier;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class CategoryListModel : PageModel
    {
        private readonly ICategoryTblServices db;
        public CategoryListModel(ICategoryTblServices db)
        {
            this.db = db;
        }
        [BindProperty]
        public CategoryTblDTO CategoryTblDTO { get; set; }
        [BindProperty]
        public List<CategoryTblDTO> GetCategory { get; set; }

        public async Task FillGrid()
        {
            var ResponseData = await db.GetByCategoryList();
            if (ResponseData != null)
            {
                if (ResponseData.ContainsKey("RegList"))
                {
                    GetCategory = (List<CategoryTblDTO>)ResponseData["RegList"];
                }
            }
        }

        public async Task OnGet()
        {
            await FillGrid();
        }

        public async Task<IActionResult> OnPostDelete(int DeleteId)
        {
            var ResponseData = await db.DeleteCategory(DeleteId);
            if(!string.IsNullOrEmpty(ResponseData))
            {
                Response.WriteAsync("<script>alert('Yor Data is Successfully Deleted.');</script>");
            }
            await FillGrid();
            return RedirectToPage("CategoryList");
        }
    }
}