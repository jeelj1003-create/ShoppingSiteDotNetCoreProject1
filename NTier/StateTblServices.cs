using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface IStateTblServices
    {
        /// <summary>
        /// Add new category and store in category
        /// </summary>
        /// <param name="Model">Pass CategoryTblDTO as Model</param>
        /// <returns>Return a Message With Store a Data or Exception</returns>
        Task<string> AddState(StateTblDTO Model);

        Task<string> UpdateState(int StateId, StateTblDTO Model);

        Task<string> DeleteState(int StateId);

        Task<Dictionary<string, object>> GetByStateId(int StateId);

        Task<Dictionary<string, object>> GetByStateList();
    }
    public class StateTblServices:IStateTblServices
    {
        private readonly DbConnector db;

        public StateTblServices(DbConnector db)
        {
            this.db = db;
        }

        public async Task<string> AddState(StateTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from StateTbl Where StateName=@st", new SqlParameter[]
                {
                    new SqlParameter("@st", Model.State)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into StateTbl Values(@cid,@st)", new SqlParameter[]
                {
                    new SqlParameter("@ct", Model.CountryID),
                    new SqlParameter("@st", Model.State)
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

        public async Task<Dictionary<string, object>> GetByStateId(int StateId)
        {
            try
            {
                var GetById = await db.GetData("Select * from StateTbl Where StateId = @sid", new SqlParameter[]
                {
                    new SqlParameter("@sid", StateId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            StateTblDTO StateTbl = new StateTblDTO();
                            StateTbl.CountryID = Convert.ToInt32(Editdt.Rows[0]["CountryId"].ToString());
                            StateTbl.State = Editdt.Rows[0]["StateName"].ToString();
                            StateTbl.StateId = Convert.ToInt32(Editdt.Rows[0]["StateId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", StateTbl}
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

        public async Task<Dictionary<string, object>> GetByStateList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from StateTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<StateTblDTO> StateList = new List<StateTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                StateList.Add(new StateTblDTO()
                                {
                                    StateId = Convert.ToInt32(Row["StateId"]),
                                    CountryID = Convert.ToInt32(Row["CountryId"]),
                                    State = Row["StateName"].ToString(),
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", StateList }
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

        public async Task<string> UpdateState(int StateId, StateTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (StateId == 0)
                {
                    return "StateId is zero.";
                }
                var Data = await db.GetData("Select * from StateTbl Where StateId!=@sid and StateName=@st", new SqlParameter[]
                {
                    new SqlParameter("@sid", StateId),
                    new SqlParameter("@st", Model.State)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This State is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update StateTbl set CountryId=@cid, StateName=@st Where StateId=@sid", new SqlParameter[]
                {
                    new SqlParameter("@cid", Model.CountryID),
                    new SqlParameter("@st", Model.State),
                    new SqlParameter("@sid", StateId)
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

        public async Task<string> DeleteState(int StateId)
        {
            try
            {
                if (StateId == 0)
                {
                    return "StateId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from StateTbl Where StateId=@sid", new SqlParameter[]
                {
                    new SqlParameter("@sid",StateId)
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
