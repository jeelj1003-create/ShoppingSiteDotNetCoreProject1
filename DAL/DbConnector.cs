using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace ShoppingSiteDotNetCore.DAL
{
    public class DbConnector
    {
        private readonly IConfiguration configuration;
        private readonly string ConnectionString;

        public DbConnector(IConfiguration configuration)
        {
            //Class Variable = //Class Parameter
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DbCon");
        }

        private async Task<SqlConnection> GetSqlConnection()
        {
            SqlConnection Con = new SqlConnection(ConnectionString);
            await Con.OpenAsync();
            return Con;
            //return new SqlConnection(ConnectionString);
        }

        //Get Data
        public async Task<Dictionary<string, object>> GetData(string SqlQuery, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlConnection Con = await GetSqlConnection())
                using (SqlCommand Cmd = new SqlCommand(SqlQuery, Con))
                using(SqlDataAdapter Da = new SqlDataAdapter(Cmd))
                {
                    if(parameters != null)
                    {
                        Cmd.Parameters.AddRange(parameters);
                    }

                    using (DataTable Dt = new DataTable())
                    {
                        Da.Fill(Dt);
                        return new Dictionary<string, object>()
                        {
                            {"Data", Dt }
                        };

                    }
                }
            }
            catch (Exception Ex)
            {
                return new Dictionary<string, object>()
                {
                    {"Error", Ex.ToString() }
                };
            }
        }

        // Insert Update and Delete Data
        public async Task<Dictionary<string, object>> InsertUpdateDeleteData(string SqlQuery, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlConnection Con = await GetSqlConnection())
                using (SqlCommand Cmd = new SqlCommand(SqlQuery, Con))
                using (SqlDataAdapter Da = new SqlDataAdapter(Cmd))
                {
                    if (parameters != null)
                    {
                        Cmd.Parameters.AddRange(parameters);
                    }
                    //Con.Open();
                    int Row = Cmd.ExecuteNonQuery();
                    if(Row > 0)
                    {
                        return new Dictionary<string, object>()
                        {
                            {"Status", "Success"}
                        };
                    }
                    else
                    {
                        return new Dictionary<string, object>()
                        {
                            {"Status" , "NoData " },
                        };
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                return new Dictionary<string, object>()
                {
                    {"Error", Ex.ToString() }
                };
            }
        }
    }
}
