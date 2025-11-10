using DACN_H_P.Model;
using Microsoft.EntityFrameworkCore;

namespace DACN_H_P.Repository.Impl
{
    public class DonHangRepositoy : IDonHangRepositoy
    {
        private readonly DacnHContext _context;
        public DonHangRepositoy(DacnHContext context)
        {
            _context = context;
        }

        public async Task<DonHang> CreateDonHang(DonHang donHang)
        {
            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();
            return donHang;
        }
        public async Task<List<ChiTietDh>> CreateChiTietDonHang(List<ChiTietDh> cts)
        {
            await _context.ChiTietDhs.AddRangeAsync(cts);
            await _context.SaveChangesAsync();
            return cts;
        }
        public async Task<DonHang?> GetDonHangByMaDH(string maDH)
        {
            return await _context.DonHangs.Include(c => c.ChiTietDhs).ThenInclude(m => m.MaSpNavigation).FirstOrDefaultAsync(d => d.MaDonHang == maDH);
        }
        public async Task<List<DonHang>> GetDonHangByMaTK(string matk)
        {
            return await _context.DonHangs.Include(c => c.ChiTietDhs).ThenInclude(m => m.MaSpNavigation).Where(tk => tk.MaTaiKhoan == matk).ToListAsync();
        }
        public async Task<List<ChiTietDh>> GetChiTietDonHangByMaDH(string maDH)
        {
            return await _context.ChiTietDhs.Where(ct => ct.MaDonHang == maDH).ToListAsync();
        }
        public async Task SavechangeAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckAcc(string matk)
        {
            var result = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaTaiKhoan == matk);
            if(result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckPTTT(string mapttt)
        {
            var result = await _context.PhuongThucTts.FirstOrDefaultAsync(pt => pt.MaPttt == mapttt);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckPTVC(string maptvc)
        {
            var result = await _context.PhuongThucVcs.FirstOrDefaultAsync(pt => pt.MaPtvc == maptvc);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckVoucher(string mavoucher)
        {
            var result = await _context.Vouchers.FirstOrDefaultAsync(v => v.MaVoucher == mavoucher);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
