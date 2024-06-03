using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SaleInvoiceItem
    {
        public int MaChiTietHoaDonBan { get; set; } // Auto-incremented Invoice Item ID
        public string MaHoaDonBan { get; set; } // Invoice ID
        public int MaSP { get; set; } // Product ID
        public string TenSP { get; set; } // Product Name
        public int SoLuongMua { get; set; } // Quantity Purchased
        public int GiaBan { get; set; } // Sale Price
    }

}
