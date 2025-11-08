using System;
using System.Collections.Generic;

namespace DACN_H_P.Model;

public partial class Voucher
{
    public string MaVoucher { get; set; } = null!;

    public string? TenVoucher { get; set; }

    public int? PhanTramGiam { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public string? DieuKienApDung { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
