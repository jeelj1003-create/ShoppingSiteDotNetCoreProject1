using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface IThirdCategoryTblServices
    {
        /// <summary>
        /// Add new category and store in category
        /// </summary>
        /// <param name="Model">Pass CategoryTblDTO as Model</param>
        /// <returns>Return a Message With Store a Data or Exception</returns>
        Task<string> AddThirdCategory(ThirdCategoryTblDTO Model);

        Task<string> UpdateThirdCategory(int ThirdCatId, ThirdCategoryTblDTO Model);

        Task<string> DeleteThirdCategory(int ThirdCatId);

        Task<Dictionary<string, object>> GetByThirdCatId(int ThirdCatId);

        Task<Dictionary<string, object>> GetByThirdCategoryList();
    }
    public class ThirdCategoryTblServices : IThirdCategoryTblServices
    {
        private readonly DbConnector db;
        public ThirdCategoryTblServices(DbConnector db)
        {
            this.db = db;
        }

        public async Task<string> AddThirdCategory(ThirdCategoryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }

                var Data = await db.GetData("Select * from ThirdCategoryTbl Where ThirdCategory=@tct", new SqlParameter[]
                {
                    new SqlParameter("@tct", Model.ThirdCategory)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "Data is already exixst.";
                        }
                    }
                }
                else if (Data.ContainsKey("Error"))
                {
                    return Data["Error"].ToString();
                }

                var InsertData = await db.InsertUpdateDeleteData("Insert into ThirdCategoryTbl Values(@ct,@sc,@tc,@ic,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.CategoryId),
                    new SqlParameter("@sc", Model.SubCategoryId),
                    new SqlParameter("@tc", Model.ThirdCategory),
                    new SqlParameter("@ic", Model.Icon),
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

        public async Task<Dictionary<string, object>> GetByThirdCategoryList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from ThirdCategoryTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<ThirdCategoryTblDTO> ThirdCatList = new List<ThirdCategoryTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                ThirdCatList.Add(new ThirdCategoryTblDTO()
                                {
                                    ThirdCatId = Convert.ToInt32(Row["ThirdCategoryId"]),
                                    CategoryId = Convert.ToInt32(Row["ThirdCategoryId"]),
                                    SubCategoryId = Convert.ToInt32(Row["ThirdCategoryId"]),
                                    ThirdCategory = Row["Category"].ToString(),
                                    IconPath = Row["Icon"].ToString(),
                                    Status = Row["status"].ToString(),
                                    EntryDate = Convert.ToDateTime(Row["EntryDate"])
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", ThirdCatList }
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

        public async Task<Dictionary<string, object>> GetByThirdCatId(int ThirdCatId)
        {
            try
            {
                var GetById = await db.GetData("Select * from ThirdCategoryTbl Where ThirdCategoryId = @tid", new SqlParameter[]
                {
                    new SqlParameter("@tid", ThirdCatId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            ThirdCategoryTblDTO ThirdCatTbl = new ThirdCategoryTblDTO();
                            ThirdCatTbl.CategoryId = Convert.ToInt32(Editdt.Rows[0]["CategoryId"]);
                            ThirdCatTbl.SubCategoryId = Convert.ToInt32(Editdt.Rows[0]["SubCategoryId"]);
                            ThirdCatTbl.ThirdCategory = Editdt.Rows[0]["ThirdCategory"].ToString();
                            ThirdCatTbl.IconPath = Editdt.Rows[0]["Icon"].ToString();
                            ThirdCatTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            ThirdCatTbl.ThirdCatId = Convert.ToInt32(Editdt.Rows[0]["ThirdCategoryId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", ThirdCatTbl}
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

        public async Task<string> UpdateThirdCategory(int ThirdCatId, ThirdCategoryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (ThirdCatId == 0)
                {
                    return "ThirdCatId is zero.";
                }
                var Data = await db.GetData("Select * from ThirdCategoryTbl Where ThirdCategoryId!=@tid and ThirdCategory=@tc", new SqlParameter[]
                {
                    new SqlParameter("@tid", ThirdCatId),
                    new SqlParameter("@tc", Model.ThirdCategory)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This ThirdCategory is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update ThirdCategory set CategoryId=@ct,SubCategoryId=@sc,ThirdCategory=@tc,Icon=@ic, Status=@st, EntryDate=GETDATE() Where ThirdCategoryId=@tid", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.CategoryId),
                    new SqlParameter("@sc", Model.SubCategoryId),
                    new SqlParameter("@tc", Model.ThirdCategory),
                    new SqlParameter("@ic", Model.Icon),
                    new SqlParameter("status", Model.Status),
                    new SqlParameter("@tid", ThirdCatId)
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

         public async Task<string> DeleteThirdCategory(int ThirdCatId)
         {
            try
            {
                if (ThirdCatId == 0)
                {
                    return "CatId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from ThirdCategoryTbl Where ThirdCategoryId=@tid", new SqlParameter[]
                {
                    new SqlParameter("@tid",ThirdCatId)
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
    }
}
