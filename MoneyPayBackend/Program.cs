using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoneyPayBackend.Service;
using MoneyPayBackend.IRepo;
using MoneyPayBackend.IService;
using MoneyPayBackend.Repo;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoneyPay API", Version = "v1" });

    // 加入 JWT Bearer 設定
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "請輸入 'Bearer ' 後面加上 JWT token。範例：Bearer abc123"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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
builder.Services.AddScoped<ITypeRepo, TypeRepo>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IHomeRepo, HomeRepo>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IUserRecordRepo, UserRecordRepo>();
builder.Services.AddScoped<IUserRecordService, UserRecordService>();

// JWT 驗證
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MoneyPay",
            ValidAudience = "MoneyPay",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("OOTimbroTimbroTimbroTimbroTimbroTimbroOO"))
        };
    });
var app = builder.Build();

// 啟用 CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Keep local development requests on HTTP so the Vite app can call the API
// without being redirected before the controller is reached.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();  // 🔑先驗證身份
app.UseAuthorization();   // 🔑再檢查權限

app.MapControllers(); // ⚠️ 沒有就沒辦法呼叫 Controller

// 啟動後自動跳 Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
