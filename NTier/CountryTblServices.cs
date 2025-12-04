using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface ICountryTblServices
    {
        Task<string> AddCountry(CountryTblDTO Model);

        Task<string> UpdateCountry(int CountryId, CountryTblDTO Model);

        Task<string> DeleteCountry(int CountryId);

        Task<Dictionary<string, object>> GetByCountryId(int CountryId);

        Task<Dictionary<string, object>> GetByCountryList();
    }
    public class CountryTblServices : ICountryTblServices
    {
        private readonly DbConnector db;
        public CountryTblServices(DbConnector db)
        {
            this.db = db;
        }
        public async Task<string> AddCountry(CountryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from CountryTbl Where CountryId=@ct", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.Country)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into CountryTbl Values(@ct,@cd)", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.Country),
                    new SqlParameter("@cd", Model.Code),
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

        public async Task<Dictionary<string, object>> GetByCountryId(int CountryId)
        {
            try
            {
                var GetById = await db.GetData("Select * from CountryTbl Where CountryId = @cid", new SqlParameter[]
                {
                    new SqlParameter("@cid", CountryId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            CountryTblDTO CountryTbl = new CountryTblDTO();
                            CountryTbl.Country = Editdt.Rows[0]["CountryName"].ToString();
                            CountryTbl.Code = Editdt.Rows[0]["Code"].ToString();
                            CountryTbl.CountryIId = Convert.ToInt32(Editdt.Rows[0]["CountryId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", CountryTbl}
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

        public async Task<Dictionary<string, object>> GetByCountryList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from CountryTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<CountryTblDTO> CountryList = new List<CountryTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                CountryList.Add(new CountryTblDTO()
                                {
                                    CountryIId = Convert.ToInt32(Row["CountryId"]),
                                    Country = Row["CountryName"].ToString(),
                                    Code = Row["Code"].ToString(),
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", CountryList }
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

        public async Task<string> UpdateCountry(int CountryId, CountryTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (CountryId == 0)
                {
                    return "CountryId is zero.";
                }
                var Data = await db.GetData("Select * from CountryTbl Where CountryId!=@cid and CountryName=@cn", new SqlParameter[]
                {
                    new SqlParameter("@cid", CountryId),
                    new SqlParameter("@cn", Model.Country)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This Country is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update CountryTbl set CountryName=@cn, Code=@cd Where CountryId=@cid", new SqlParameter[]
                {
                    new SqlParameter("@cn", Model.Country),
                    new SqlParameter("@cd", Model.Code),
                    new SqlParameter("@cid", CountryId)
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
        public async Task<string> DeleteCountry(int CountryId)
        {
            try
            {
                if (CountryId == 0)
                {
                    return "CountryId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from CountryTbl Where CountryId=@cid", new SqlParameter[]
                {
                    new SqlParameter("@cid",CountryId)
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
