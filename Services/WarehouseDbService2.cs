using WarehouseABPD.Models;

namespace WarehouseABPD.Services;

public class WarehouseDbService2 : IWarehouseDbService2
{
    private readonly string _sqlConn = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

    public override async Task<int> PostWarehouse(Warehouse warehouse)
    {
        var procedure = "AddProductToWarehouse";
        using (var conn = new SqlConnection(_sqlConn))
        {
            await conn.OpenAsync();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                using (var comm = new SqlCommand())
 
                {
                    comm.Connection = conn;
                    comm.Transation = transaction;
                    comm.CommandText = procedure;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        comm.Parameters.Add("@IdProduct", System.Data.SqlDbType.Int).Value = warehouse.IdProduct;
                        comm.Parameters.Add("@IdWarehouse", System.Data.SqlDbType.Int).Value = warehouse.IdWarehouse;
                        comm.Parameters.Add("@Amount", System.Data.SqlDbType.Int).Value = warehouse.Amount;
                        comm.Parameters.Add("@CreatedAt", System.Data.SqlDbType.DateTime).Value =
                            DateTime.Parse(warehouse.CreatedAt);
                        await comm.ExecuteNonQueryAsync();
                    }
                    catch (Exception e)
                    {
                        await transaction.RollbackAsync();
                    }
                }

                await transaction.CommitAsync();
                await conn.CloseAsync();
            }

        }

        {

        }
        return 1;
    }
}