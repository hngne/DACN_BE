using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Mapper
{
    public class SanPhamBuilder
    {
        public static SanPhamResponse ToResponse(SanPham sp)
        {
            return new SanPhamResponse
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                TenDanhMuc = sp.MaDanhMucNavigation?.TenDanhMuc ?? "N/a",
                Gia = sp.Gia,
                MoTa = sp.MoTa,
                SoLuongTon = sp.SoLuongTon,
                TheTich = sp.TheTich,
                DonVi = sp.DonVi,
                DuongDanAnhSPs = sp.AnhSps.Select(a => a.DuongDan).ToList()
            };
        }
    }
}
