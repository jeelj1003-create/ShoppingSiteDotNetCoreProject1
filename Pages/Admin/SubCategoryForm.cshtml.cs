using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using ShoppingSiteDotNetCore.NTier;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShoppingSiteDotNetCore.Pages.Admin
{
    public class SubCategoryFormModel : PageModel
    {
        [BindProperty]
        public SubCategoryTblDTO SubCategoryTblDTO { get; set; }
        public string Error { get; set; }

        // Category Dropdown
        public List<SelectListItem> CategoryList  { get; set; }

        //Constructor
        private readonly ISubCategoryTblServices db;
        private readonly DbConnector dbConnector;
        public SubCategoryFormModel(ISubCategoryTblServices db, DbConnector dbConnector)
        {
            this.db = db;
            this.dbConnector = dbConnector;
        }
        public async Task OnGet()
        {
            var FillCategoryDrop = await dbConnector.GetData("Select * from CategoryTbl");
            if(FillCategoryDrop.ContainsKey("Data"))
            {
                if(FillCategoryDrop != null)
                {
                    DataTable dt = (DataTable)FillCategoryDrop["Data"];
                    if (dt != null)
                    { 
                        CategoryList = new List<SelectListItem>();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    CategoryList.Add(new SelectListItem()
                        //    {
                        //        Text = dt.Rows[i]["Category"].ToString(),
                        //        Value = dt.Rows[i]["CategoryId"].ToString()
                        //    });

                        //}

                        //2nd Method
                        foreach (DataRow Row in dt.Rows)
                        {
                            CategoryList.Add(new SelectListItem()
                            {
                                Text = Row["Category"].ToString(),
                                Value = Row["CategoryId"].ToString()
                            });
                        }
                    }
                }
            }
        }

        //Insert Category
        public async Task<IActionResult> OnPostCreate()
        {
            var ResponseData = await db.AddCategory(SubCategoryTblDTO);
            if (!string.IsNullOrEmpty(ResponseData))
            {
                Response.WriteAsync("<script>alert('SubCategory is successfully inserted.')</script>");
            }
            else
            {
                Error = ResponseData;
            }
            return Page();
        }
    }
}
