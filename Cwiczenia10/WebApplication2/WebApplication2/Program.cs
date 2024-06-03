using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Context;
using WebApplication2.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddDbContext<Context>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
        );
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProductService, ProductService>();

    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
        }

        app.UseMiddleware<ExceptionHandler>();

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}