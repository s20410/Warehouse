using WarehouseABPD.Models;

namespace WarehouseABPD.Services;

public abstract class IWarehouseDbServise
{
    public abstract Task<int> PostWarehouse(Warehouse warehouse);
}