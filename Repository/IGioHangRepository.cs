using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;

namespace DACN_H_P.Repository
{
    public interface IGioHangRepository
    {
        Task<GioHang?> GetGioHangByMaTaiKhoanAsync(string matk);
        Task AddOrUpdateGioHang(string matk, string masp, int soluong);
        Task DecreaseItemGioHang(string matk, string masp, int soluong);
        Task UpdateItemGioHang(string matk, string masp, int soluong);
        Task RemoveItemAsync(string matk, string masp);
        Task RemoveAllGioHang(string matk);
        Task SavechangeAsync();
    }
}
