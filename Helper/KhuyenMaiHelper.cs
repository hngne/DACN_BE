using DACN_H_P.Model;

namespace DACN_H_P.Helper
{
    public class KhuyenMaiHelper
    {
        public static decimal TinhGiaKhuyenMai(SanPham sp)
        {
            decimal giakm;
            var km = sp.ChiTietKms.FirstOrDefault(ct => ct.MaKhuyenMaiNavigation.NgayBatDau <= DateTime.Now && ct.MaKhuyenMaiNavigation.NgayKetThuc >= DateTime.Now);
            if (km == null)
            {
                giakm = sp.Gia;
            }
            else
            {
                giakm = sp.Gia - (sp.Gia * km.PhanTramGiam / 100);
            }
            return giakm;
        } 
    }
}
