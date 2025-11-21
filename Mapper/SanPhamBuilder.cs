using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Mapper
{
    public class SanPhamBuilder
    {
        public static SanPhamResponse ToResponse(SanPham sp)
        {
            var km = sp.ChiTietKms.FirstOrDefault(ct => ct.MaKhuyenMaiNavigation.NgayBatDau <= DateTime.Now && ct.MaKhuyenMaiNavigation.NgayKetThuc >= DateTime.Now);
            var giaKhuyenMai = km != null ? sp.Gia - (sp.Gia * km.PhanTramGiam / 100) : sp.Gia;
            return new SanPhamResponse
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                TenDanhMuc = sp.MaDanhMucNavigation?.TenDanhMuc ?? "N/a",
                Gia = sp.Gia,
                GiaKhuyenMai = giaKhuyenMai,
                MoTa = sp.MoTa,
                SoLuongTon = sp.SoLuongTon,
                TheTich = sp.TheTich,
                DonVi = sp.DonVi,
                AnhDaiDien = sp.AnhSps.FirstOrDefault()?.DuongDan,
                AnhPhu1 = sp.AnhSps.Skip(1).FirstOrDefault()?.DuongDan,
                AnhPhu2 = sp.AnhSps.Skip(2).FirstOrDefault()?.DuongDan,
                DuongDanAnhSPs = sp.AnhSps.Select(a => a.DuongDan).ToList()
            };
        }
    }
}
