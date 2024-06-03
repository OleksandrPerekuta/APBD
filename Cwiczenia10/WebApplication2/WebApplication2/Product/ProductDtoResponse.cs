namespace WebApplication2.Controller;

public class ProductDtoResponse
{
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }
    public int ProductId { get; set; }
    public List<object> Categories { get; set; }
}