using DACN_H_P.Dtos.Request;
using DACN_H_P.Dtos.Response;

namespace DACN_H_P.Service
{
    public interface IDonHangService
    {
        Task<(bool success, string message, DonHangResponse response)> CreateDonHang(DatHangRequest request);
        Task<DonHangResponse> GetDonHangByMaDH(string maDH);
        Task<IEnumerable<DonHangResponse>> GetDonHangByMaTK(string maTK);

    }
}
