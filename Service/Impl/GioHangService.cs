using DACN_H_P.Dtos.Response;
using DACN_H_P.Mapper;
using DACN_H_P.Repository;

namespace DACN_H_P.Service.Impl
{
    public class GioHangService : IGioHangService
    {
        private readonly IGioHangRepository _repo;

        public GioHangService(IGioHangRepository repo)
        {
            _repo = repo;
        }
        public async Task<(bool, string,GioHangResponse)> GetGioHangByMaTaiKhoanAsync(string matk)
        {
            var gioHang = await _repo.GetGioHangByMaTaiKhoanAsync(matk);
            if (gioHang == null)
            {
                return (false, "Không có tài khoản này", null);
            }
            return (true, "Lấy thành công giỏ hàng", GioHangBuilder.ToResponse(gioHang));
        }

        public async Task<(bool, string, GioHangResponse)> AddOrUpdateGioHang(string matk, ChiTietGHResponse request)
        {
            var result = await _repo.AddOrUpdateGioHang(matk, request.MaSp, request.SoLuong);
            if (!result)
            {
                return (false, "Tài khoản không tồn tại", null);
            }
            await _repo.SavechangeAsync();
            var gioHang = await _repo.GetGioHangByMaTaiKhoanAsync(matk);
            var response = GioHangBuilder.ToResponse(gioHang);
            return (true, "Thêm hoặc cập nhật giỏ hàng thành công", response);
        }
        public async Task<(bool, string, GioHangResponse)> DecreaseItemGioHang(string matk, Dtos.Request.ChiTietGioHangRequest request)
        {
            var result = await _repo.DecreaseItemGioHang(matk, request.MaSP, request.SoLuong);
        }
    }
}
