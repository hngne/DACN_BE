namespace DACN_H_P.Dtos.Response
{
    public class GioHangResponse
    {
        public string? MaTaiKhoan { get; set; }
        public List<GioHangItemResponse> SanPhams { get; set; } = new();
        public double TongTien { get; set; }
    }
        public class GioHangItemResponse
    {
        public string MaSp { get; set; } = null!;
        public string TenSp { get; set; } = null!;
        public string? Anh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
