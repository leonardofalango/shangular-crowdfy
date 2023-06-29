using security_jwt;
using backend.Model;
using backend.Controllers;
using backend.Model.Interfaces;
using backend.Model.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddScoped<CrowdfyContext>(); // Shared Context

builder.Services.AddTransient<IService<Post>, PostService>(); // Create class every req
builder.Services.AddTransient<IService<Forum>, ForumService>(); // Create class every req
builder.Services.AddTransient<IService<User>, UserService>(); // Create class every req

builder.Services.AddTransient<IPasswordProvider>(
    p => new PasswordProvider("chupiquete")
);

builder.Services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();


app.UseCors();
app.UseSwagger(); // Swagger for debug
app.UseSwaggerUI(); // Swagger for debug

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();



public static class ExtensionMethods
{
    public static IEnumerable<T> AddMany<T>(this List<T> l, IEnumerable<T> elements)
    {
        foreach (var item in elements)
        {
            l.Add(item);
        }
        return l;
    }

    
    public static bool isIn(this int? n, int[] arr)
    {
        foreach (var number in arr)
            if (n == number)
                return true;
        return false;
    }
}