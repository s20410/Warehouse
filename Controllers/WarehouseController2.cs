using Microsoft.AspNetCore.Mvc;
using WarehouseABPD.Models;
using WarehouseABPD.Services;

namespace WarehouseABPD
{

    [ApiController]
    [Route("api/warehouses2")]
    public class WarehouseController2<IWarehouseDbServise2> : ControllerBase
    {
        private readonly IWarehouseDbService2 _warehouseDbServise2;

        public WarehouseController2(IWarehouseDbService2 warehouseDbService2)
        {
            _warehouseDbServise2 = warehouseDbService2;
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehouse(Warehouse warehouse)
        {
            _warehouseDbServise2.PostWarehouse(warehouse);
            return Ok("Warehouse state changed");
        }
    }
}