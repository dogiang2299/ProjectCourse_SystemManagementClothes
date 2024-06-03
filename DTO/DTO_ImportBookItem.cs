using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ImportBookItem
    {
        public int MaChiTietNhapKho { get; set; }
        public string MaNhapKho { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongTonKho { get; set; }
        public int SoLuongNhapKho { get; set; }
        public int DonGiaNhap { get; set; }
    }
}
