using Security_jwt;
using backend.Model;
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

builder.Services.AddTransient<IRepository<Post>, RepoPost>(); // Create class every req
builder.Services.AddTransient<IRepository<Forum>, RepoForum>(); // Create class every req
builder.Services.AddTransient<IRepository<User>, RepoUser>(); // Create class every req

builder.Services.AddTransient<IUserService, UserService>(); // Create class every req
builder.Services.AddTransient<IPostService, PostService>(); // Create class every req
builder.Services.AddTransient<IForumService, ForumService>(); // Create class every req
builder.Services.AddTransient<IJwtService>(p =>
    new JwTService(new PasswordProvider("chupiquete"))
);


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