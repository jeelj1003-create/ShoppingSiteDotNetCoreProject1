using Microsoft.Data.SqlClient;
using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.Model;
using System.Data;

namespace ShoppingSiteDotNetCore.NTier
{
    public interface IProductTblServices
    {
        Task<string> AddProduct(ProductTblDTO Model);

        Task<string> UpdateProduct(int ProductId, ProductTblDTO Model);

        Task<string> DeleteProduct(int ProductId);

        Task<Dictionary<string, object>> GetByProductId(int ProductId);

        Task<Dictionary<string, object>> GetByProductList();
    }
    public class ProductTblServices : IProductTblServices
    {
        private readonly DbConnector db;
        public ProductTblServices(DbConnector db)
        {
            this.db = db;
        }
        public async Task<string> AddProduct(ProductTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is Null.";
                }
                var Data = await db.GetData("Select * from ProductTbl Where ProductName=@pr", new SqlParameter[]
                {
                    new SqlParameter("@pr", Model.Product)
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


                var InsertData = await db.InsertUpdateDeleteData("Insert into ProductTbl Values(@pn,@pr,@ct,@sc,@tc,@br,@ph,@ds,@st,GETDATE())", new SqlParameter[]
                {
                    new SqlParameter("@pn", Model.Product),
                    new SqlParameter("@pr", Model.Price),
                    new SqlParameter("@ct", Model.CategoryId),
                    new SqlParameter("@sc", Model.SubCategoryId),
                    new SqlParameter("@tc", Model.ThirdCategoryId),
                    new SqlParameter("@br", Model.BrandId),
                    new SqlParameter("@ph", Model.Icon),
                    new SqlParameter("@ds", Model.Description),
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

        public async Task<Dictionary<string, object>> GetByProductId(int ProductId)
        {
            try
            {
                var GetById = await db.GetData("Select * from ProductTbl Where ProductId = @pid", new SqlParameter[]
                {
                    new SqlParameter("@pid", ProductId)
                });
                if (GetById.ContainsKey("Data"))
                {
                    DataTable Editdt = (DataTable)GetById["Data"];
                    if (Editdt != null)
                    {
                        if (Editdt.Rows.Count > 0)
                        {
                            ProductTblDTO ProductTbl = new ProductTblDTO();
                            ProductTbl.Product = Editdt.Rows[0]["ProductName"].ToString();
                            ProductTbl.Price = Convert.ToInt32(Editdt.Rows[0]["Price"].ToString());
                            ProductTbl.CategoryId = Convert.ToInt32(Editdt.Rows[0]["CategoryId"].ToString());
                            ProductTbl.SubCategoryId = Convert.ToInt32(Editdt.Rows[0]["SubCategoryId"].ToString());
                            ProductTbl.ThirdCategoryId = Convert.ToInt32(Editdt.Rows[0]["ThirdCategoryId"].ToString());
                            ProductTbl.BrandId = Convert.ToInt32(Editdt.Rows[0]["BrandId"].ToString());
                            ProductTbl.IconPath = Editdt.Rows[0]["Photo"].ToString();
                            ProductTbl.Status = Editdt.Rows[0]["Description"].ToString();
                            ProductTbl.Status = Editdt.Rows[0]["Status"].ToString();
                            ProductTbl.ProductId = Convert.ToInt32(Editdt.Rows[0]["ProductId"].ToString());
                            return new Dictionary<string, object>()
                            {
                                {"Model", ProductTbl}
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

        public async Task<Dictionary<string, object>> GetByProductList()
        {
            try
            {
                var GetByList = await db.GetData("Select * from ProductTbl");
                if (GetByList.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)GetByList["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            List<ProductTblDTO> ProductList = new List<ProductTblDTO>();
                            foreach (System.Data.DataRow Row in dt.Rows)
                            {
                                ProductList.Add(new ProductTblDTO()
                                {
                                    ProductId = Convert.ToInt32(Row["ProductId"]),
                                    Product = Row["ProductName"].ToString(),
                                    Price = Convert.ToInt32(Row["Price"]),
                                    CategoryId = Convert.ToInt32(Row["CategoryId"]),
                                    SubCategoryId = Convert.ToInt32(Row["SubCategoryId"]),
                                    ThirdCategoryId = Convert.ToInt32(Row["ThirdCategoryId"]),
                                    BrandId = Convert.ToInt32(Row["BrandId"]),
                                    IconPath = Row["Photo"].ToString(),
                                    Description = Row["Description"].ToString(),
                                    Status = Row["status"].ToString(),
                                    EntryDate = Convert.ToDateTime(Row["EntryDate"])
                                });
                            }

                            return new Dictionary<string, object>()
                            {
                                {"RegList", ProductList }
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

        public async Task<string> UpdateProduct(int ProductId, ProductTblDTO Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null.";
                }
                if (ProductId == 0)
                {
                    return "ProductId is zero.";
                }
                var Data = await db.GetData("Select * from ProductTbl Where ProductId!=@pid and ProductName=@pr", new SqlParameter[]
                {
                    new SqlParameter("@pid", ProductId),
                    new SqlParameter("@pr", Model.Product)
                });
                if (Data.ContainsKey("Data"))
                {
                    DataTable dt = (DataTable)Data["Data"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "This Product is already exist.";
                        }
                    }
                }

                var UpdateData = await db.InsertUpdateDeleteData("Update ProductTbl set ProductName=@prn,Price=@pr,CategoryId=@ct,SubCategoryId=@sc,ThirdCategoryId=@tc,BrandId=@br,Photo=@ph,Description=@ds,Status=@st, EntryDate=GETDATE() Where ProductId=@pid", new SqlParameter[]
                {
                    new SqlParameter("@pn", Model.Product),
                    new SqlParameter("@pr", Model.Price),
                    new SqlParameter("@ct", Model.CategoryId),
                    new SqlParameter("@sc", Model.SubCategoryId),
                    new SqlParameter("@tc", Model.ThirdCategoryId),
                    new SqlParameter("@br", Model.BrandId),
                    new SqlParameter("@ph", Model.Icon),
                    new SqlParameter("@ds", Model.Description),
                    new SqlParameter("@st", Model.Status),
                    new SqlParameter("@cid", ProductId)
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

        public async Task<string> DeleteProduct(int ProductId)
        {
            try
            {
                if (ProductId == 0)
                {
                    return "ProductId is zero.";
                }
                var DeleteData = await db.InsertUpdateDeleteData("Delete from ProductTbl Where ProductId=@pid", new SqlParameter[]
                {
                    new SqlParameter("@pid",ProductId)
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
