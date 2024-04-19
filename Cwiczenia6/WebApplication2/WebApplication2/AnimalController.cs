using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAllAnimals()
    {
        var response = new List<GetAnimalsDetailsResponse>();
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand("SELECT * FROM Animals", sqlConnection);
            sqlCommand.Connection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                response.Add(new GetAnimalsDetailsResponse(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    )
                );
            }
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        var sqlCommand = new SqlCommand("SELECT * FROM Animals WHERE ID = @1", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@1", id);
        sqlCommand.Connection.Open();

        var reader = sqlCommand.ExecuteReader();
        if (!reader.Read()) return NotFound();

        return Ok(new GetAnimalDetailsResponse(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4)
            )
        );
    }

    [HttpPost]
    public IActionResult CreateAnimal(CreateAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(
                "INSERT INTO Animals (Name, Description, Category, Area) values (@1, @2, @3, @4); SELECT CAST(SCOPE_IDENTITY() as int)",
                sqlConnection
            );
            sqlCommand.Parameters.AddWithValue("@1", request.Name);
            sqlCommand.Parameters.AddWithValue("@2", request.Description);
            sqlCommand.Parameters.AddWithValue("@3", request.Category);
            sqlCommand.Parameters.AddWithValue("@4", request.Area);
            sqlCommand.Connection.Open();

            var id = sqlCommand.ExecuteScalar();

            return Created($"animals/{id}", new CreateAnimalDTOs((int)id, request));
        }
    }


    [HttpDelete("{id}")]
    public IActionResult RemoveAnimal(int id)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = new SqlCommand("DELETE FROM Animals WHERE ID = @1", sqlConnection);
            command.Parameters.AddWithValue("@1", id);
            command.Connection.Open();

            var affectedRows = command.ExecuteNonQuery();

            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
}