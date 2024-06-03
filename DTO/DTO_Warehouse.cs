using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OperationResultWare
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class DTO_Warehouse
    {
       
        public string MaXuatKho { get; set; }
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public int TongTienXuat { get; set; }
        public DateTime NgayXuatKho { get; set; }
    }
}
