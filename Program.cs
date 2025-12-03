
using DACN_H_P.Helper;
using DACN_H_P.Model;
using DACN_H_P.Repository;
using DACN_H_P.Repository.Impl;
using DACN_H_P.Service;
using DACN_H_P.Service.Impl;
using DACN_H_P.Utils;
using Microsoft.EntityFrameworkCore;

namespace DACN_H_P
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DacnHContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySetting"));
            builder.Services.AddSingleton<CloudinaryService>();

            builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
            builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();

            builder.Services.AddScoped<IDanhMucRepository, DanhMucRepository>();
            builder.Services.AddScoped<IDanhMucService, DanhMucService>();

            builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
            builder.Services.AddScoped<ISanPhamService, SanPhamService>();

            builder.Services.AddScoped<IAnhRepository, AnhRepository>();
            builder.Services.AddScoped<IAnhService, AnhService>();

            builder.Services.AddScoped<IGioHangRepository, GioHangRepository>();
            builder.Services.AddScoped<IGioHangService, GioHangService>();

            builder.Services.AddScoped<IKhuyenMaiRepository, KhuyenMaiRepository>();
            builder.Services.AddScoped<IKhuyenMaiService, KhuyenMaiService>();

            builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
            builder.Services.AddScoped<IVoucherService, VoucherService>();

            builder.Services.AddScoped<IDonHangRepositoy, DonHangRepository>();
            builder.Services.AddScoped<IDonHangService, DonHangService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
