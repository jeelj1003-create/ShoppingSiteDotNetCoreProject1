using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;
namespace ShoppingSiteDotNetCore.NTier
{
    public interface ICategoryTblServices
    {
        /// <summary>
        /// Add new category and store in category
        /// </summary>
        /// <param name="Model">Pass CategoryTblDTO as Model</param>
        /// <returns>Return a Message With Store a Data or Exception</returns>
        Task<string> AddCategory(CategoryTblDTO Model);

        Task<string> UpdateCategory(int CatId,CategoryTblDTO Model);

        Task<string> DeleteCategory(int CatId);

        Task<Dictionary<string, object>> GetByCatId(int CatId);

        Task<Dictionary<string, object>> GetByCategoryList();
    }

    public class CategoryTblServices : ICategoryTblServices
    {
        //File Upload
        private readonly IWebHostEnvironment webHostEnvironment;

        //Database Connection
        private readonly DbConnector db;
        public CategoryTblServices(DbConnector db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        //Start Insert Data
        public async Task<string> AddCategory(CategoryTblDTO Model)
        {
            try
            {
                if(Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from CategoryTbl Where Category=@ct", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.Category)
                });
                if(Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if(dt != null)
                    {
                        if(dt.Rows.Count > 0)
                        {
                            return "Data is already exixst.";
                        }
                    }
                }
                else if(Data.ContainsKey("Error"))
                {
                    return Data["Error"].ToString();
                }

                //File Upload
                if(Model.Icon != null)
                {
                    if(Model.Icon.Length > 0)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into CategoryTbl Values(@ct,@ic,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.Category),
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
        public async Task<Dictionary<string, object>> GetByCatId(int CatId)
        {
            try
            {
                var GetById = await db.GetData("Select * from CategoryTbl Where CategoryId = @cid", new SqlParameter[]
                {
                    new SqlParameter("@cid", CatId)
                });
                if(GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if(Editdt != null)
                    {
                        if(Editdt.Rows.Count > 0)
                        {
                            CategoryTblDTO CatTbl = new CategoryTblDTO();
                            CatTbl.Category = Editdt.Rows[0]["Category"].ToString();
                            CatTbl.IconPath = Editdt.Rows[0]["Icon"].ToString();
                            CatTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            CatTbl.CatId = Convert.ToInt32(Editdt.Rows[0]["CategoryId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", CatTbl}
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
        public async Task<Dictionary<string, object>> GetByCategoryList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from CategoryTbl");
                if(GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if(dt.Rows.Count > 0)
                        {
                            List<CategoryTblDTO> CatList = new List<CategoryTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                CatList.Add(new CategoryTblDTO()
                                {
                                    CatId = Convert.ToInt32(Row["CategoryId"]),
                                    Category = Row["Category"].ToString(),
                                    IconPath = Row["Icon"].ToString(),
                                    Status = Row["status"].ToString(),
                                    EntryDate = Convert.ToDateTime(Row["EntryDate"])
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", CatList }
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
        public async Task<string> UpdateCategory(int CatId, CategoryTblDTO Model)
        {
            try
            {
                if(Model == null)
                {
                    return "Model is null.";
                }
                if(CatId == 0)
                {
                    return "CatId is zero.";
                }
                var Data = await db.GetData("Select * from CategoryTbl Where CategoryId!=@cid and Category=@ct", new SqlParameter[]
                {
                    new SqlParameter("@cid", CatId),
                    new SqlParameter("@ct", Model.Category)
                });
                if(Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if(dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This Category is already exist.";
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

                var UpdateData = await db.InsertUpdateDeleteData("Update CategoryTbl set Category=@ct, Icon=@ic, Status=@st, EntryDate=GETDATE() Where CategoryId=@cid", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.Category),
                    new SqlParameter("@ic", Model.Icon),
                    new SqlParameter("@st", Model.Status),
                    new SqlParameter("@cid", CatId)
                });
                if(UpdateData.ContainsKey("Status"))
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
        public async Task<string> DeleteCategory(int CatId)
        {
            try
            {
                if(CatId == 0)
                {
                    return "CatId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from CategoryTbl Where CategoryId=@cid", new SqlParameter[]
                {
                    new SqlParameter("@cid",CatId)
                });
                if(DeleteData.ContainsKey("Status"))
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
