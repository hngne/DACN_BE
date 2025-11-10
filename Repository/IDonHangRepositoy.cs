using DACN_H_P.Model;

namespace DACN_H_P.Repository
{
    public interface IDonHangRepositoy
    {
        Task<DonHang> CreateDonHang(DonHang donHang);
        Task<List<ChiTietDh>> CreateChiTietDonHang(List<ChiTietDh> cts);
        Task<DonHang?> GetDonHangByMaDH(string maDH);
        Task<List<DonHang>> GetDonHangByMaTK(string matk);
        Task<List<ChiTietDh>> GetChiTietDonHangByMaDH(string maDH);
        Task<bool> CheckAcc(string matk);
        Task<bool> CheckPTTT(string mapttt);
        Task<bool> CheckPTVC(string maptvc);
        Task<bool> CheckVoucher(string mavoucher);

    }
}
