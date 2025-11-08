using DACN_H_P.Dtos.Response;
using DACN_H_P.Model;
using Microsoft.EntityFrameworkCore;

namespace DACN_H_P.Repository.Impl
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly DacnHContext _context;

        public GioHangRepository(DacnHContext context)
        {
            _context = context;
        }
        public async Task<GioHang?> GetGioHangByMaTaiKhoanAsync(string matk)
        {
            return await _context.GioHangs
                .Include(GH => GH.ChiTietGhs)
                .ThenInclude(GH => GH.MaSpNavigation)
                .ThenInclude(sp => sp.AnhSps)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
        }

        public async Task AddOrUpdateGioHang(string matk, string masp, int soluong)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if (tk == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            var gioHang = await _context.GioHangs.Include(GH => GH.ChiTietGhs)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaGioHang = Guid.NewGuid().ToString(),
                    MaTaiKhoan = matk,
                    ChiTietGhs = new List<ChiTietGh>()
                };
                _context.GioHangs.Add(gioHang);
            }
            var item = gioHang.ChiTietGhs.FirstOrDefault(i => i.MaSp == masp);
            if (item == null)
            {
                item = new ChiTietGh
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaSp = masp,
                    SoLuong = soluong
                };
                gioHang.ChiTietGhs.Add(item);
            }
            else
            {
                item.SoLuong += soluong;
            }
        }
        public async Task DecreaseItemGioHang(string matk, string masp, int soluong)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if (tk == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            var gioHang = await _context.GioHangs
                .Include(GH => GH.ChiTietGhs)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
            if (gioHang == null)
            {
                return;
            }
            var item = gioHang.ChiTietGhs.FirstOrDefault(i => i.MaSp == masp);
            if (item != null)
            {
                item.SoLuong -= soluong;
            }
            else
            {
                return;
            }
        }
        public async Task UpdateItemGioHang(string matk, string masp, int soluong)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if (tk == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            var gioHang = await _context.GioHangs
                .Include(GH => GH.ChiTietGhs)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
            if (gioHang == null)
            {
                return;
            }
            var item = gioHang.ChiTietGhs.FirstOrDefault(i => i.MaSp == masp);
            if (item != null)
            {
                item.SoLuong = soluong;
            }
            else
            {
                return;
            }
        }
        public async Task RemoveItemAsync(string matk, string masp)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if (tk == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            var gioHang = await _context.GioHangs
                .Include(GH => GH.ChiTietGhs)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
            if (gioHang == null)
            {
                return;
            }
            var item = gioHang.ChiTietGhs.FirstOrDefault(i => i.MaSp == masp);
            if (item != null)
            {
                gioHang.ChiTietGhs.Remove(item);
            }
        }

        public async Task RemoveAllGioHang(string matk)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if (tk == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            var gioHang = await _context.GioHangs
                .Include(GH => GH.ChiTietGhs)
                .FirstOrDefaultAsync(GH => GH.MaTaiKhoan == matk);
            if (gioHang != null)
            {
                _context.GioHangs.Remove(gioHang);
            }
        }
        public async Task SavechangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
