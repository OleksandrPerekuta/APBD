using System.Data.SqlClient;

namespace WebApplication2;

public static class AnimalEndpoints
{
    public static void AnimalEnpoints(this WebApplication app)
    {
        var animals = app.MapGroup("minimal-animals");

        animals.MapGet("/", GetAnimals);
        animals.MapGet("{id:int}", GetAnimal);
        animals.MapPost("/", CreateAnimal);
        animals.MapDelete("{id:int}", RemoveAnimal);
        //animals.MapPut("{id:int}", ReplaceAnimal);
    }

    private static IResult RemoveAnimal(IConfiguration configuration, int id)
    {
        using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
        {
            var command = new SqlCommand("DELETE FROM Animals WHERE ID = @1", sqlConnection);
            command.Parameters.AddWithValue("@1", id);
            command.Connection.Open();
   
            var affectedRows = command.ExecuteNonQuery();
   
            return affectedRows == 0 ? Results.NotFound() : Results.NoContent();
        }
    }

    private static IResult CreateAnimal(IConfiguration configuration, CreateAnimalRequest request)
    {
        using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
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
               
            return Results.Created($"students/{id}", new CreateAnimalDTOs((int)id, request));
        }
    }
    private static IResult GetAnimals(IConfiguration configuration)
    {
        var response = new List<GetAnimalsDetailsResponse>();
        using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
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
        return Results.Ok(response);
    }
    private static IResult GetAnimal(IConfiguration configuration, int id)
    {
        using var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        var sqlCommand = new SqlCommand("SELECT * FROM Animals WHERE ID = @1", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@1", id);
        sqlCommand.Connection.Open();
   
        var reader = sqlCommand.ExecuteReader();
        if (!reader.Read()) return Results.NotFound();
   
        return Results.Ok(new GetAnimalDetailsResponse(
                reader.GetInt32(0), 
                reader.GetString(1),
                reader.GetString(2), 
                reader.GetString(3) ,
                reader.GetString(4)
            )
        );
    }

    
    
}