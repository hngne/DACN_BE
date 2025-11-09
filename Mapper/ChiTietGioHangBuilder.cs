using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Mapper
{
    public class ChiTietGioHangBuilder
    {
        public static ChiTietGHResponse ToResponse(ChiTietGh ct, SanPham sp)
        {
            return new ChiTietGHResponse
            {
                MaSp = sp.MaSp,
                TenSp = sp.TenSp,
                Anh = sp.AnhSps.FirstOrDefault()?.DuongDan,
                SoLuong = ct.SoLuong,
                DonGia = sp.Gia
            };
        }
    }
}
