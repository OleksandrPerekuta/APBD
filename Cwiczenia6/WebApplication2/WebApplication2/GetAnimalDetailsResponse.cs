using System.ComponentModel.DataAnnotations;

namespace WebApplication2;

public record GetAnimalDetailsResponse(int id, string Name,string Description, string Category, string Area);