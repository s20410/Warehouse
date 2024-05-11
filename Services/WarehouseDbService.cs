using System.Transactions;
using WarehouseABPD.Models;

namespace WarehouseABPD.Services;

public class WarehouseDbService : IWarehouseDbServise, Startup.IWarehouseDbService
{
    private readonly string _sqlConn = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";
    public override async Task<int> PostWarehouse(Warehouse warehouse)
    {
        if (warehouse.IdProduct == 0 || warehouse.IdWarehouse == 0 || warehouse.Amount <= 0 ||
            string.IsNullOrEmpty(warehouse.CreatedAt))
        {
            return 1;
        }

        var varehouseId = 0;
        var order = 0;
        var price = 0;
        using (var conn = new SqlConnection(_sqlConn))
        {
            conn.Open();
            using (SqlTransation transation = conn.BeginTransation())
            {
                using (var comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.Transation = transation;
                    comm.CommandText = "SELECT * FROM WAREHOUSE WHERE IDWAREHOUSE = \" + warehouse.IdProduct;";
                    using (SqlDataReader dataReader = await comm.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            int.Parse((ReadOnlySpan<byte>)dataReader["IdOrder"].ToString());
                        }
                    }
                }

                using (var comm = new SqlCommand())
                {
                    if (order != 0)
                    {
                        comm.CommandText = "SELECT * FROM PRODUCT_WAREHOUSE WHERE IDORDER = " + order;
                        int rows = comm.ExecuteNonQuery();
                        if (rows != 0)
                        {
                            
                        }
                    }
                }

                using (var comm = new SqlCommand())
                {
                    comm.CommandText = "Select price from product where idproduct = " + warehouse.IdProduct;
                    await comm.ExecuteNonQueryAsync();
                    SqlDataReader dataReader = await comm.ExecuteReaderAsync();
                    if (dataReader.HasRows)
                    {
                        price = int.Parse((string)dataReader["price"].ToString());
                    }
                    comm.CommandText = $"INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES( {warehouse.IdWarehouse}, {warehouse.IdProduct}, {order}, {warehouse.Amount}, {warehouse.Amount * price}, {warehouse.CreatedAt} );";
                    await comm.ExecuteNonQueryAsync()
                }
            }

            await Transaction.CommitAsync();
            await conn.CloseAsync();

        }

        return 1;
    }
    
}