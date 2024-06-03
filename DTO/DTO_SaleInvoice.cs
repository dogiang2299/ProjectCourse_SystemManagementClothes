using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    // HoaDonBanDTO.cs
    public class DTO_SaleInvoice
    {
        public string MaHoaDonBan { get; set; } // Invoice ID (6 characters)
        public int MaKhach { get; set; } // Customer ID
        public string TenKhach { get; set; } // Customer Name
        public int MaNV { get; set; } // Employee ID
        public string TenNV { get; set; } // Employee Name
        public int KhuyenMai { get; set; } // Discount
        public int GiamGia { get; set; } // Discount amount
        public int VanChuyen { get; set; } // Shipping cost
        public int TongPhu { get; set; } // Subtotal
        public int TongChinh { get; set; } // Total
        public DateTime NgayBan { get; set; } // Sale Date
    }


}
