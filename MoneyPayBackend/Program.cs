using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Repo;
using MoneyPayBackend.Service;

var builder = WebApplication.CreateBuilder(args);

// 讀取連接字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 註冊 DbContext
builder.Services.AddDbContext<MoneyPayDBContext>(options =>
    options.UseSqlServer(connectionString));

// 加入 Controllers
builder.Services.AddControllers();

// 加入 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 加入 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 注入
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// 啟用 CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // ⚠️ 沒有就沒辦法呼叫 Controller

// 啟動後自動跳 Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
