using HIS.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC (Controllers + Views) thay cho Razor Pages mặc định
builder.Services.AddControllersWithViews();

// Session: lưu lựa chọn của người dùng giữa các bước đặt lịch (khoa, ngày, ca, người khám)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Nguồn dữ liệu đặt lịch. Hiện là DỮ LIỆU MẪU.
// Khi nối database: tạo class mới implement IBookingData (gọi HIS.BLL) rồi thay dòng này.
builder.Services.AddSingleton<IBookingData, MockBookingData>();

// Giữ nguyên cấu hình HttpClient gọi HIS.WebApi (phần của thành viên khác có thể dùng)
builder.Services.AddHttpClient("HisWebApi", client =>
{
    var baseUrl = builder.Configuration["WebApi:BaseUrl"] ?? "https://localhost:7198/";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Booking/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Trang mặc định: trang bắt đầu đặt lịch
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Booking}/{action=Index}/{id?}");

app.Run();