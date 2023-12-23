using Oracle.ManagedDataAccess.Client;
using SHELF_LABEL_API_NET7.Models;

namespace SHELF_LABEL_API_.NET7.Services
{
    public class ProductDtlService
    {
        public string connectionString { get; set; }
        public ProductDtlService(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public string GetConnectioinString()
        {
            return connectionString;
        }
        public ProductDtlModel GetProduct(string barcode, string loc)
        {
            ProductDtlModel Product = new ProductDtlModel();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT 
                                                    SU_DESCRIPTION,
                                                    SU_DESCRIPTION_AR,
                                                    SELLING_PRICE
                                                FROM
                                                    GRAND_PRD_MASTER_FULL_NEW A,
                                                    POS_CONTROL B
                                                WHERE
                                                    A.PRODUCT_CODE = B.GOLD_CODE
                                                    AND A.SU = B.SU
                                                    AND A.BARCODE = '" + barcode + @"'
                                                    AND STORE_ID = '" + loc + @"'";
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Product.Su_desc = reader["SU_DESCRIPTION"].ToString();
                                Product.Su_desc_ar = reader["SU_DESCRIPTION_AR"].ToString();
                                Product.Price = reader["SELLING_PRICE"].ToString();
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Product;
        }
    }
}
