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
        Task<Dictionary<string, object>> GetSubCatId(int SubCatId);
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
                if (Data.ContainsKey("Data"))
                {
                    return "This subcategory is already exist.";
                }
                else if (Data.ContainsKey("Error"))
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
                if (InsertData.ContainsKey("Status"))
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
        public async Task<Dictionary<string, object>> GetSubCatList()
        {
            try
            {
                var GetByList = await Db.GetData("Select * from SubCategoryTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<SubCategoryTblDTO> SubCatList = new List<SubCategoryTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                SubCatList.Add(new SubCategoryTblDTO()
                                {
                                    SubCatId = Convert.ToInt32(Row["SubCategoryId"]),
                                    CategoryId = Convert.ToInt32(Row["CategoryId"]),
                                    SubCategory = Row["SubCategory"].ToString(),
                                    IconPath = Row["Icon"].ToString(),
                                    Status = Row["status"].ToString(),
                                    EntryDate = Convert.ToDateTime(Row["EntryDate"])
                                });
                            }
                            return new Dictionary<string, object>()
                            {
                                {"RegList", SubCatList }
                            };
                        }
                    }
                    else
                    {
                        return new Dictionary<string, object>()
                        {
                            {"Error", GetByList["Error"].ToString()}
                        };
                    }
                }
                return GetByList;
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>()
                {
                    {"Error", ex.ToString()}
                };
            }
        }
        //End GetByList Data

        //Start Update Data
        public async Task<string> UpdateSubCategory(int SubCatId, SubCategoryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (SubCatId == 0)
                {
                    return "CatId is zero.";
                }
                var Data = await Db.GetData("Select * from SubCategoryTbl Where SubCategoryId!=@sid and SubCategory=@st", new SqlParameter[]
                {
                    new SqlParameter("@sid", SubCatId),
                    new SqlParameter("@st", Model.SubCategory)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This SubCategory already exists.";
                        }
                    }
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

                var UpdateData = await Db.InsertUpdateDeleteData("Update SubCategoryTbl set CategoryId=@cid, SubCategory=@sc, Icon=@ic, Status=@st, EntryDate=GETDATE() Where SubCategoryId=@sid", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CategoryId),
                    new SqlParameter("@sc", Model.SubCategory),
                    new SqlParameter("@ic", Model.Icon),
                    new SqlParameter("@st", Model.Status),
                    new SqlParameter("@sid", SubCatId)
                });
                if (UpdateData.ContainsKey("Status"))
                {
                    return "Your Data is successfully updated.";
                }
                else
                {
                    return UpdateData["Error"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //End Update Data

        //Start Delete Data
        public async Task<string> DeleteSubCategory(int SubCatId)
        {
            try
            {
                if (SubCatId == 0)
                {
                    return "CatId is zero.";
                }
                var DeleteData = await Db.InsertUpdateDeleteData("Delete from SubCategoryTbl Where SubCategoryId=@sid", new SqlParameter[]
                {
                    new SqlParameter("@sid", SubCatId)
                });
                if (DeleteData.ContainsKey("Status"))
                {
                    return "Your data is successfully deleted.";
                }
                else
                {
                    return DeleteData["Error"].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //End Delete Data
    }
}
