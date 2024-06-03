using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class DTO_ImportBook
    {
        public string MaNhapKho { get; set; }
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public int TongTienNhap { get; set; }
        public DateTime NgayNhapKho { get; set; }

    }
}
