using Microsoft.AspNetCore.Mvc;
using WarehouseABPD.Models;
using WarehouseABPD.Services;



namespace WarehouseABPD
{
    [ApiController]
    [Route("api/warehouses")]

    public class WarehouseController : ControllerBase
    {
        private IWarehouseDbServise _warehouseDbServise;

        public WarehouseController(IWarehouseDbServise warehouseDbServise)
        {
            _warehouseDbServise = warehouseDbServise;
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehouse(Warehouse warehouse)
        {
            var result = _warehouseDbServise.PostWarehouse(warehouse);

            return Ok(result);
        }
    }
}