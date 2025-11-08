using DACN_H_P.Model;
using Microsoft.EntityFrameworkCore;

namespace DACN_H_P.Repository.Impl
{
    public class TaiKhoanRepository:ITaiKhoanRepository
    {
        private readonly DacnHContext _context;

        public TaiKhoanRepository(DacnHContext context)
        {
            _context = context;
        }

        public async Task<TaiKhoan?> GetByTenDangNhapAsync(string username)
        {
            return await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.TenDangNhap == username);
        }
        public async Task AddAsync(TaiKhoan tk)
        {
            await _context.TaiKhoans.AddAsync(tk);
        }
        public async Task SavechangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
