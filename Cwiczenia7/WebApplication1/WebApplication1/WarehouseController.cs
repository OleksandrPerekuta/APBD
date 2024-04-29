using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseService _warehouseService;

    public WarehouseController(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public IActionResult AddProductToWarehouse([FromBody] ProductToWarehouseRequest request)
    {
        try
        {
            int newId = _warehouseService.AddProductToWarehouse(request.IdProduct, request.IdWarehouse, request.Amount, request.CreatedAt);
            return Ok(new { NewId = newId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}

public class ProductToWarehouseRequest
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}