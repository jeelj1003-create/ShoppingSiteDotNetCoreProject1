using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface ICityTblServices
    {
        Task<string> AddCity(CityTblDTO Model);

        Task<string> UpdateCity(int CityId, CityTblDTO Model);

        Task<string> DeleteCity(int CityId);

        Task<Dictionary<string, object>> GetByCityId(int CityId);

        Task<Dictionary<string, object>> GetByCityList();
    }
    public class CityTblServices : ICityTblServices
    {
        private readonly DbConnector db;
        public CityTblServices(DbConnector db)
        {
            this.db = db;
        }
        public async Task<string> AddCity(CityTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from CityTbl Where CityName=@ct", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.City)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into CityTbl Values(@cid,@sid,ct)", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CountryId),
                    new SqlParameter("@sid", Model.StateId),
                    new SqlParameter("@ct", Model.City)
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

        public async Task<Dictionary<string, object>> GetByCityId(int CityId)
        {
            try
            {
                var GetById = await db.GetData("Select * from CityTbl Where CityId = @ctid", new SqlParameter[]
                {
                    new SqlParameter("@ctid", CityId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            CityTblDTO CityTbl = new CityTblDTO();
                            CityTbl.CountryId = Convert.ToInt32(Editdt.Rows[0]["CountryId"].ToString());
                            CityTbl.StateId = Convert.ToInt32(Editdt.Rows[0]["StateId"].ToString());
                            CityTbl.City = Editdt.Rows[0]["CityName"].ToString();
                            CityTbl.CityId = Convert.ToInt32(Editdt.Rows[0]["CityId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", CityTbl}
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

        public async Task<Dictionary<string, object>> GetByCityList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from CityTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<CityTblDTO> CityList = new List<CityTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                CityList.Add(new CityTblDTO()
                                {
                                    CityId = Convert.ToInt32(Row["CityId"]),
                                    CountryId = Convert.ToInt32(Row["CountryId"]),
                                    StateId = Convert.ToInt32(Row["StateId"]),
                                    City = Row["CityName"].ToString(),
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", CityList }
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

        public async Task<string> UpdateCity(int CityId, CityTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (CityId == 0)
                {
                    return "CityId is zero.";
                }
                var Data = await db.GetData("Select * from CityTbl Where CityId!=@ctid and CityName=@ct", new SqlParameter[]
                {
                    new SqlParameter("@ctid", CityId),
                    new SqlParameter("@ct", Model.City)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This City is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update CityTbl set CountryId=@cid, StateId=@sid, CityName=@cn Where CityId=@ctid", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CountryId),
                    new SqlParameter("@sid", Model.StateId),
                    new SqlParameter("@cn", Model.City),
                    new SqlParameter("@ctid", Model.CityId),
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

        public async Task<string> DeleteCity(int CityId)
        {
            try
            {
                if (CityId == 0)
                {
                    return "CityId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from CityTbl Where CityId=@ctid", new SqlParameter[]
                {
                    new SqlParameter("@ctid",CityId)
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
