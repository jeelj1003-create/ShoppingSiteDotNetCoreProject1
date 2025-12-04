using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface IAreaTblServices
    {
        Task<string> AddArea(AreaTblDTO Model);

        Task<string> UpdateArea(int AreaId, AreaTblDTO Model);

        Task<string> DeleteArea(int AreaId);

        Task<Dictionary<string, object>> GetByAreaId(int AreaId);

        Task<Dictionary<string, object>> GetByAreaList();
    }
    public class AreaTblServices : IAreaTblServices
    {
        private readonly DbConnector db;
        public AreaTblServices(DbConnector db)
        {
            this.db = db;
        }

        public async Task<string> AddArea(AreaTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from AreaTbl Where Area=@ar", new SqlParameter[]
                {
                    new SqlParameter("@ar", Model.Area)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into AreaTbl Values(@cid,@sid,@ctid,@ar,@st)", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CountryId),
                    new SqlParameter("@sid", Model.StateId),
                    new SqlParameter("@ctid", Model.CityId),
                    new SqlParameter("@ar", Model.Area),
                    new SqlParameter("@st", Model.Status),

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

        public async Task<Dictionary<string, object>> GetByAreaId(int AreaId)
        {
            try
            {
                var GetById = await db.GetData("Select * from AreaTbl Where AreaId = @aid", new SqlParameter[]
                {
                    new SqlParameter("@aid", AreaId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            AreaTblDTO AreaTbl = new AreaTblDTO();
                            AreaTbl.CountryId = Convert.ToInt32(Editdt.Rows[0]["CountryId"].ToString());
                            AreaTbl.StateId = Convert.ToInt32(Editdt.Rows[0]["StateId"].ToString());
                            AreaTbl.CityId = Convert.ToInt32(Editdt.Rows[0]["CityId"].ToString());
                            AreaTbl.Area = Editdt.Rows[0]["Area"].ToString();
                            AreaTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            AreaTbl.AreaId = Convert.ToInt32(Editdt.Rows[0]["AreaId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", AreaTbl}
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

        public async Task<Dictionary<string, object>> GetByAreaList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from AreaTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<AreaTblDTO> AreaList = new List<AreaTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                AreaList.Add(new AreaTblDTO()
                                {
                                    AreaId = Convert.ToInt32(Row["AreaId"]),
                                    CountryId = Convert.ToInt32(Row["CountryId"]),
                                    StateId = Convert.ToInt32(Row["StateId"]),
                                    CityId = Convert.ToInt32(Row["CityId"]),
                                    Area = Row["Area"].ToString(),
                                    Status = Row["status"].ToString()
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", AreaList }
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

        public async Task<string> UpdateArea(int AreaId, AreaTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (AreaId == 0)
                {
                    return "AreaId is zero.";
                }
                var Data = await db.GetData("Select * from AreaTbl Where AreaId!=@aid and Area=@ar", new SqlParameter[]
                {
                    new SqlParameter("@aid", AreaId),
                    new SqlParameter("@ar", Model.Area)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This Area is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update AreaTbl set CountryId=@cid,StateId=@sid,CityId=@ctid,Area=@ar,Status=@st, Where AreaId=@aid", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CountryId),
                    new SqlParameter("@sid", Model.StateId),
                    new SqlParameter("@ctid", Model.CityId),
                    new SqlParameter("@ar", Model.Area),
                    new SqlParameter("@st", AreaId)
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

        public async Task<string> DeleteArea(int AreaId)
        {
            try
            {
                if (AreaId == 0)
                {
                    return "AreaId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from AreaTbl Where AreaId=@aid", new SqlParameter[]
                {
                    new SqlParameter("@aid",AreaId)
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
