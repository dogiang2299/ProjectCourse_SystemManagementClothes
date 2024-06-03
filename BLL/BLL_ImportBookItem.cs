using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ImportBookItem
    {
        DAL_ImportBookItem dalNhapKhoChiTiet = new DAL_ImportBookItem();
        public List<DTO_ImportBookItem> GetAllChiTietNhapKho(string manhapkho)
        {
            return dalNhapKhoChiTiet.GetAllChiTietNhapKho(manhapkho);
        }

        public bool DeleteChiTietNhapKho(int maChiTietNhapKho)
        {
            return dalNhapKhoChiTiet.DeleteChiTietNhapKho(maChiTietNhapKho);
        }
    }
}
