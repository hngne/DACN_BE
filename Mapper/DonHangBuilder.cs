using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Mapper
{
    public class DonHangBuilder
    {
        public static DonHangResponse ToResponse(DonHang dh)
        {
            return new DonHangResponse
            {
                MaDonHang = dh.MaDonHang,
                NgayDatHang = dh.NgayDatHang ?? DateTime.MinValue,
                TrangThaiDonHang = dh.TrangThaiDonHang ?? string.Empty,
                TongTienHang = dh.TongTienHang,
                DiaChiGiaoHang = dh.DiaChiGh,
                PhuongThucThanhToan = dh.MaPtttNavigation?.TenPttt ?? string.Empty,
                PhuongThucVanChuyen = dh.MaPtvcNavigation?.TenPtvc ?? string.Empty,
                ChiTietDonHang = dh.ChiTietDhs.Select(ct => ChiTietDonHangBuilder.ToResponse(ct)).ToList()
            };
        }
    }
}
