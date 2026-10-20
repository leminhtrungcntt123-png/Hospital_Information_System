using HIS.BLL;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Lấy Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Đăng ký AppDbContext (snake_case khớp schema CSDL thật;
// kiểu timestamp đã được AppDbContext cấu hình sẵn)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention());

// DbContext scaffold từ CSDL thật, dùng cho IAppointmentService
// (tên bảng/cột đã được cấu hình tường minh trong OnModelCreating)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Đăng ký các BLL Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();