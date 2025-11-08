using DACN_H_P.Dtos.Request;
using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;
using DACN_H_P.Repository;
using Microsoft.AspNetCore.Identity;

namespace DACN_H_P.Service.Impl
{
    public class TaiKhoanService:ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _repo;

        private readonly PasswordHasher<TaiKhoan> _hasher = new();

        public TaiKhoanService(ITaiKhoanRepository repo) {
            _repo = repo; 
        }

        public async Task<(bool, string)> DangKyAsync(DangKyRequest request)
        {
            var exist = await _repo.GetByTenDangNhapAsync(request.TenDangNhap);
            if (exist != null)
            {
                return (false, "Tên đăng nhập đã tồn tại");
            }
            var tk = new TaiKhoan
            {
                MaTaiKhoan = Guid.NewGuid().ToString(),
                TenDangNhap = request.TenDangNhap,
                Email = request.Email,
                HoTen = request.HoTen,
                SoDienThoai = request.SoDienThoai,
                DiaChi = request.DiaChi,
                VaiTro = "user",
            };

            tk.MatKhau = _hasher.HashPassword(tk, request.MatKhau);

            await _repo.AddAsync(tk);
            await _repo.SavechangeAsync();
            return (true, "Đăng ký thành công");
            
        }
        public async Task<(bool, string, DangNhapResponse)> DangNhapAsync(DangNhapRequest request)
        {
            var exist = await _repo.GetByTenDangNhapAsync(request.TenDangNhap);
            if(exist == null)
            {
                return (false, "Tên đăng nhập hoặc mật khẩu không chính xác",  null);
            }
            var result = _hasher.VerifyHashedPassword(exist, exist.MatKhau, request.MatKhau);
            if(result == PasswordVerificationResult.Failed)
            {
                return (false, "Tên đăng nhập hoặc mật khẩu không chính xác", null);
            }
            return (true,"Dăng nhập thành công",new DangNhapResponse
            {
                MaTaiKhoan = exist.MaTaiKhoan,
                TenDangNhap = exist.TenDangNhap,
                Email = exist.Email,
                VaiTro = exist.VaiTro,
            });
        }
    }
}
