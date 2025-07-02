using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.Utilities;
using Common.Settings;
using DAL.Data;
using DAL.Repositories.Implement;
using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VNPAY.NET;

var builder = WebApplication.CreateBuilder(args);

// Thêm CORS vào services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL React chạy trên cổng 3000
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// Thêm Session vào DI container
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Timeout 30 phút
    options.Cookie.HttpOnly = true;  // Bảo mật chống XSS
    options.Cookie.IsEssential = true;
});

// Cấu hình Authentication với JWT
var secretKey = Encoding.UTF8.GetBytes(JwtSettingModel.SecretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = true,
            ValidIssuer = JwtSettingModel.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtSettingModel.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
   ServiceLifetime.Scoped
);
builder.Services.AddAutoMapper(typeof(BloodMappingProfile).Assembly);
//SERVICE
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBloodRegistrationService, BloodRegistrationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserMedicalService, UserMedicalService>();
builder.Services.AddScoped<IBloodService, BloodService>();
builder.Services.AddScoped<IChronicDiseaseService, ChronicDiseaseService>();
builder.Services.AddScoped<ISeparatedBloodComponentService, SeparatedBloodComponentService>();
builder.Services.AddScoped<IBloodRequestService, BloodRequestService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IBloodRegistrationService, BloodRegistrationService>();




//REPO
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserMedicalRepository, UserMedicalRepository>();
builder.Services.AddScoped<IBloodRepository, BloodRepository>();

//VNPay
builder.Services.AddScoped<IVnPayService, VnPayService>();

builder.Services.AddSingleton<IVnpay>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var vnpay = new Vnpay();
    vnpay.Initialize(
        config["VNPAY:TmnCode"],
        config["VNPAY:HashSecret"],
        config["VNPAY:Url"], // 🔥 Sửa "BaseUrl" -> "Url"
        config["VNPAY:ReturnUrl"] // 🔥 Đảm bảo đúng key
    );
    return vnpay;
});


//Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddScoped<IEmailService, EmailService>();



// Thêm Authorization dựa trên Role
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
//});

// Add Swagger với JWT Authentication
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet API", Version = "v1" });

// Thêm nút "Authorize" trong Swagger UI để nhập JWT Token
var securityScheme = new OpenApiSecurityScheme
{
    Name = "Authorization",
    Description = "Nhập token theo định dạng: Bearer {your JWT token}",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    Reference = new OpenApiReference
        {
        Type = ReferenceType.SecurityScheme,
        Id = "Bearer"
    }
};
  
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

// Thêm Global Authorization (Tất cả API yêu cầu đăng nhập, trừ khi được đánh dấu [AllowAnonymous])
//builder.Services.AddControllers(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

// 🟢 Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UserUtility>();


var app = builder.Build();
app.UseCors("AllowReactApp"); // ⬅️ NÊN để trước Auth

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blood API V1");
        c.RoutePrefix = "swagger"; // Truy cập trực tiếp tại https://localhost:5210/swagger
    });
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
