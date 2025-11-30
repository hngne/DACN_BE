using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Mapper
{
    public class KhuyenMaiBuilder
    {
        public static KhuyenMaiResponse ToResponse(KhuyenMai km)
        {
            return new KhuyenMaiResponse
            {
                MaKhuyenMai = km.MaKhuyenMai,
                TenKhuyenMai = km.TenKhuyenMai,
                NgayBatDau = km.NgayBatDau,
                NgayKetThuc = km.NgayKetThuc,
                MoTa = km.MoTa,
                TrangThai = km.NgayKetThuc < DateTime.Now ? "Hết hạn" : "Còn hiệu lực"
            };
        }
    }
}
