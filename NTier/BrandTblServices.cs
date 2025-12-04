using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface IBrandTblServices
    {
        Task<string> AddBrand(BrandTblDTO Model);

        Task<string> UpdateBrand(int BrandId, BrandTblDTO Model);

        Task<string> DeleteBrand(int BrandId);

        Task<Dictionary<string, object>> GetByBrandId(int BrandId);

        Task<Dictionary<string, object>> GetByBrandList();
    }
    public class BrandTblServices : IBrandTblServices
    {
        private readonly DbConnector db;
        public BrandTblServices(DbConnector db)
        {
            this.db = db;
        }
        public async Task<string> AddBrand(BrandTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from BrandTbl Where Brand=@br", new SqlParameter[]
                {
                    new SqlParameter("@br", Model.Brand)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into BrandTbl Values(@br,@ic,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@br", Model.Brand),
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

        public async Task<Dictionary<string, object>> GetByBrandId(int BrandId)
        {
            try
            {
                var GetById = await db.GetData("Select * from BrandTbl Where BrandId = @bid", new SqlParameter[]
                {
                    new SqlParameter("@bid", BrandId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            BrandTblDTO BrandTbl = new BrandTblDTO();
                            BrandTbl.Brand = Editdt.Rows[0]["Brand"].ToString();
                            BrandTbl.IconPath = Editdt.Rows[0]["Icon"].ToString();
                            BrandTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            BrandTbl.BrandId = Convert.ToInt32(Editdt.Rows[0]["BrandId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", BrandTbl}
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

        public async Task<Dictionary<string, object>> GetByBrandList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from BrandTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<BrandTblDTO> BrandList = new List<BrandTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                BrandList.Add(new BrandTblDTO()
                                {
                                    BrandId = Convert.ToInt32(Row["BrandId"]),
                                    Brand = Row["Brand"].ToString(),
                                    IconPath = Row["Icon"].ToString(),
                                    Status = Row["status"].ToString(),
                                    EntryDate = Convert.ToDateTime(Row["EntryDate"])
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", BrandList }
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

        public async Task<string> UpdateBrand(int BrandId, BrandTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (BrandId == 0)
                {
                    return "BrandId is zero.";
                }
                var Data = await db.GetData("Select * from BrandTbl Where BrandId!=@bid and Brand=@br", new SqlParameter[]
                {
                    new SqlParameter("@bid", BrandId),
                    new SqlParameter("@br", Model.Brand)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This Brand is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update BrandTbl set Brand=@br, Icon=@ic, Status=@st, EntryDate=GETDATE() Where BrandId=@bid", new SqlParameter[]
                {
                    new SqlParameter("@br", Model.Brand),
                    new SqlParameter("@ic", Model.Icon),
                    new SqlParameter("status", Model.Status),
                    new SqlParameter("@bid", BrandId)
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
        public async Task<string> DeleteBrand(int BrandId)
        {
            try
            {
                if (BrandId == 0)
                {
                    return "CatId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from BrandTbl Where BrandId=@bid", new SqlParameter[]
                {
                    new SqlParameter("@bid",BrandId)
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
