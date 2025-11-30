using DACN_H_P.Dtos.Request;
using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;
using DACN_H_P.Repository;

namespace DACN_H_P.Service.Impl
{
    public class DonHangService : IDonHangService
    {
        private readonly IDonHangRepositoy _repo;
        private readonly IGioHangRepository _gioRepo;

        public DonHangService(IDonHangRepositoy repo, IGioHangRepository gioRepo)
        {
            _repo = repo;
            _gioRepo = gioRepo;
        }
        public async Task<(bool, string, DonHangResponse)> CreateDonHang(DatHangRequest request)
        {
            var gioHang = await _gioRepo.GetGioHangByMaTaiKhoanAsync(request.MaTaiKhoan);
            if(gioHang == null || gioHang.ChiTietGhs.Count == 0)
            {
                return (false, "Giỏ hàng trống", null);
            }
            decimal tongTienHang = 0;
            var chiTietDonHangs = new List<ChiTietDh>();
            foreach(var item in gioHang.ChiTietGhs)
            {
                var sp = item.MaSpNavigation;
                var km = sp.ChiTietKms.FirstOrDefault(ct => ct.MaKhuyenMaiNavigation.NgayBatDau <= DateTime.Now && ct.MaKhuyenMaiNavigation.NgayKetThuc >= DateTime.Now);

                var donGiaLucDat = km != null ? sp.Gia * (1 - km.PhanTramGiam / 100) : sp.Gia;

                tongTienHang += donGiaLucDat * item.SoLuong;
                chiTietDonHangs.Add(new ChiTietDh
                {
                    MaDonHang = Guid.NewGuid().ToString(),
                    MaSp = item.MaSp,
                    SoLuong = item.SoLuong,
                    DonGiaLucDat = donGiaLucDat
                });
            }
            return (false, "f", null);
        }
        public async Task<DonHangResponse> GetDonHangByMaDH(string maDH)
        {
            return null;
        }
        public async Task<IEnumerable<DonHangResponse>> GetDonHangByMaTK(string maTK)
        {
            return null;
        }
    }

}
