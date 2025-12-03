using DACN_H_P.Dtos.Request;
using DACN_H_P.Dtos.Response;
using DACN_H_P.Mapper;
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
        public async Task<IEnumerable<TaiKhoanResponse>> GetAllTaiKhoan()
        {
            var data = await _repo.GetAllTaiKhoan();
            return data.Select(TaiKhoanBuilder.ToResponse);
        }

        public async Task<TaiKhoanResponse?> GetTaiKhoanById(string id)
        {
            var tk = await _repo.GetByMaTaiKhoan(id);
            return tk == null ? null : TaiKhoanBuilder.ToResponse(tk);
        }

        public async Task<(bool, string, TaiKhoanResponse?)> UpdateProfile(string id, UpdateProfileRequest request)
        {
            var tk = await _repo.GetByMaTaiKhoan(id);
            if (tk == null) return (false, "Tài khoản không tồn tại", null);

            tk.HoTen = request.HoTen ?? tk.HoTen;
            tk.SoDienThoai = request.SoDienThoai ?? tk.SoDienThoai;
            tk.DiaChi = request.DiaChi ?? tk.DiaChi;
            tk.Email = request.Email ?? tk.Email;

            await _repo.UpdateAsync(tk);
            return (true, "Cập nhật thông tin thành công", TaiKhoanBuilder.ToResponse(tk));
        }

        public async Task<(bool, string, TaiKhoanResponse?)> ChangePassword(string id, ChangePasswordRequest request)
        {
            var tk = await _repo.GetByMaTaiKhoan(id);
            if (tk == null) return (false, "Tài khoản không tồn tại", null);

            var verify = _hasher.VerifyHashedPassword(tk, tk.MatKhau, request.MatKhauCu);
            if (verify == PasswordVerificationResult.Failed)
            {
                return (false, "Mật khẩu cũ không chính xác", null);
            }

            tk.MatKhau = _hasher.HashPassword(tk, request.MatKhauMoi);

            await _repo.UpdateAsync(tk);
            return (true, "Đổi mật khẩu thành công", TaiKhoanBuilder.ToResponse(tk));
        }

        public async Task<(bool, string, TaiKhoanResponse?)> UpdateRole(string id, string newRole)
        {
            var tk = await _repo.GetByMaTaiKhoan(id);
            if (tk == null) return (false, "Tài khoản không tồn tại", null);

            if (newRole.ToLower() != "admin" && newRole.ToLower() != "user")
                return (false, "Role không hợp lệ (chỉ chấp nhận 'admin' hoặc 'user')", null);

            tk.VaiTro = newRole.ToLower();
            await _repo.UpdateAsync(tk);
            return (true, $"Đã cập nhật quyền thành {newRole}", TaiKhoanBuilder.ToResponse(tk));
        }

        public async Task<(bool, string)> DeleteTaiKhoan(string id)
        {
            var result = await _repo.DeleteOrLockTaiKhoan(id);
            if (!result)
            {
                return (false, "Không thể xóa (Tài khoản không tồn tại hoặc ĐÃ CÓ ĐƠN HÀNG)");
            }
            return (true, "Xóa tài khoản thành công");
        }
    }
}
