using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface ISubCategoryTblServices
    {
        Task<string> AddCategory(SubCategoryTblDTO Model);
        Task<Dictionary<string, object>> GetSubCatId(int  SubCatId);
        Task<Dictionary<string, object>> GetSubCatList();
        Task<string> UpdateSubCategory(int SubCatId, SubCategoryTblDTO Model);
        Task<string> DeleteSubCategory(int SubCatId);

    }
    public class SubCategoryTblServices : ISubCategoryTblServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DbConnector Db;
        public SubCategoryTblServices(DbConnector Db, IWebHostEnvironment webHostEnvironment)
        {
            this.Db = Db;
            this.webHostEnvironment = webHostEnvironment;
        }
        //Start Insert Data
        public async Task<string> AddCategory(SubCategoryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }

                var Data = await Db.GetData("Select * from SubCategoryTbl Where SubCategory=@sc", new SqlParameter[]
                {
                    new SqlParameter("@sc", Model.SubCategory)
                });
                if(Data.ContainsKey("Data"))
                {
                    return "This subcategory is already exist.";
                }
                else if(Data.ContainsKey("Error"))
                {
                    return Data["Data"].ToString();
                }

                //File Upload
                if (Model.Icon != null)
                {
                    if (Model.Icon.Length > 0)
                    {
                        string ImageName = System.DateTime.Now.ToString("ddMMyyyymmssffff");
                        string ImageExtantion = Path.GetExtension(Model.Icon.FileName);
                        string NewName = ImageName + ImageExtantion;

                        FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Img", NewName), FileMode.Create);
                        Model.Icon.CopyTo(fs);
                        fs.Close();
                        fs.Dispose();
                        Model.IconPath = "/Img/" + NewName;
                    }
                }

                var InsertData = await Db.InsertUpdateDeleteData("Insert into SubCategoryTbl Values(@cid,@sc,@ic,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@cid",Model.CategoryId),
                    new SqlParameter("@sc",Model.SubCategory),
                    new SqlParameter("@ic", Model.IconPath),
                    new SqlParameter("@st", Model.Status)
                });
                if(InsertData.ContainsKey("Status"))
                {
                    return "Your data is successfully inserted.";
                }
                else
                {
                    return InsertData["Error"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //End Insert Data

        //Start GetById Data
        public async Task<Dictionary<string, object>> GetSubCatId(int SubCatId)
        {
            try
            {
                var GetById = await Db.GetData("Select * from SubCategoryTbl Where SubCategoryId = @sid", new SqlParameter[]
                {
                    new SqlParameter("@sid", SubCatId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            SubCategoryTblDTO SubCatTbl = new SubCategoryTblDTO();
                            SubCatTbl.CategoryId = Convert.ToInt32(Editdt.Rows[0]["CategoryId"].ToString());
                            SubCatTbl.SubCategory = Editdt.Rows[0]["SubCategory"].ToString();
                            SubCatTbl.IconPath = Editdt.Rows[0]["Icon"].ToString();
                            SubCatTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            SubCatTbl.SubCatId = Convert.ToInt32(Editdt.Rows[0]["SubCategoryId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", SubCatTbl}
                            };
                        }
                    }
                }
                else
                {
                    return new Dictionary<string, object>()
                    {
                        {"Error", GetById["Error"] }
                    };
                }
                return GetById;
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>()
                {
                    {"Error", ex.ToString()}
                };
            }
        }
        //End GetById Data

        //Start GetByList Data
        public Task<Dictionary<string, object>> GetSubCatList()
        {
            throw new NotImplementedException();
        }
        //End GetByList Data

        //Start Update Data
        public Task<string> UpdateSubCategory(int SubCatId, SubCategoryTblDTO Model)
        {
            throw new NotImplementedException();
        }
        //End Update Data

        //Start Delete Data
        public Task<string> DeleteSubCategory(int SubCatId)
        {
            throw new NotImplementedException();
        }
        //End Delete Data
    }
}
