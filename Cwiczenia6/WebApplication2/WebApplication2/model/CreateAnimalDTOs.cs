using System.ComponentModel.DataAnnotations;

namespace WebApplication2;

public record CreateAnimalDTOs(int id, string Name, string Description, string Category, string Area)
{
    public CreateAnimalDTOs(int id, CreateAnimalRequest request) : this(id, request.Name, request.Description,
        request.Category, request.Area)
    {
    }
};

public record CreateAnimalRequest(
    [Required] [MaxLength(200)] string Name,
    [MaxLength(200)] string Description,
    [Required] string Category,
    [Required] string Area
);