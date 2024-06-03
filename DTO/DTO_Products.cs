using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace DTO
{
    public class DTO_Products
    {
        public int masanpham { get; set; }
        public string mabarcode { get; set; }
        public string tensanpham { get; set; }
        public int giavon { get; set; }
        public int giaban {  get; set; }
        public string donvi { get; set; }
        public int soluongtonkho { get; set; }
        public string mota {  get; set; }
        public string gannhansanpham { get; set; }
        public string kichthuoc { get; set; }
        public string danhmucsanpham { get; set; }
        public string tinhtrangsanpham { get; set; }
        public byte[] hinhanhsanpham { get; set; }
        public int madanhmuc { get; set; }
    }
}
