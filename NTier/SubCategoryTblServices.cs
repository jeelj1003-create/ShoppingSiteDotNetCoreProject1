using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;

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
        private readonly DbConnector Db;
        public SubCategoryTblServices(DbConnector Db)
        {
            this.Db = Db;
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

                var InsertData = await Db.InsertUpdateDeleteData("Insert into SubCategoryTbl Values(@cid,@sc,@ic,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@cid",Model.CategoryId),
                    new SqlParameter("@sc",Model.SubCategory),
                    new SqlParameter("@ic", Model.Icon),
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
        public Task<Dictionary<string, object>> GetSubCatId(int SubCatId)
        {
            throw new NotImplementedException();
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
