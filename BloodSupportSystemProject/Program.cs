using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.Utilities;
using DAL.Data;
using DAL.Repositories.Implement;
using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
   ServiceLifetime.Scoped
);
builder.Services.AddAutoMapper(typeof(BloodMappingProfile).Assembly);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBloodRegistrationService, BloodRegistrationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBloodRepository, BloodRepository>();
builder.Services.AddScoped<IBloodService, BloodService>();
builder.Services.AddScoped<IChronicDiseaseService, ChronicDiseaseService>();
builder.Services.AddScoped<IUserMedicalService, UserMedicalService>();
builder.Services.AddScoped<IUserMedicalRepository, UserMedicalRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserUtility>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Allow react app
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
