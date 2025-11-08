using DACN_H_P.Dtos.Request;
using DACN_H_P.Dtos.Response;

namespace DACN_H_P.Service
{
    public interface ITaiKhoanService
    {
        Task<(bool success, string message)> DangKyAsync(DangKyRequest request);
        Task<(bool success, string message, DangNhapResponse response)> DangNhapAsync(DangNhapRequest request);
    }
}
