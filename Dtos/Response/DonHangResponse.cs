namespace DACN_H_P.Dtos.Response
{
    public class DonHangResponse
    {
        public string MaDonHang { get; set; } = null!;
        public DateTime NgayDatHang { get; set; }
        public string TrangThaiDonHang { get; set; } = null!;
        public decimal TongTienHang { get; set; }
        public string DiaChiGiaoHang { get; set; } = null!;
        public string PhuongThucThanhToan { get; set; } = null!;
        public string PhuongThucVanChuyen { get; set; } = null!;
        public List<ChiTietDonHangResponse> ChiTietDonHang { get; set; } = new();
    }
}
