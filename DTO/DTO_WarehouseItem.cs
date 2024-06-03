using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_WarehouseItem
    {
        public int MaChiTietXuatKho { get; set; }
        public string MaXuatKho { get; set; }
        public int MaSanPham { get; set; }
        public string TenSanPham { get;  set; }
        public int SoLuongTonKho { get; set; }
        public int SoLuongXuatKho { get; set; }
        public int GiaXuat { get; set; }

    }
}
