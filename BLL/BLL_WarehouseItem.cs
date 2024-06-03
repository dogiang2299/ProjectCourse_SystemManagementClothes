using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_WarehouseItem
    {
        DAL_WarehouseItem dalXuatKhoChiTiet = new DAL_WarehouseItem();
        public List<DTO_WarehouseItem> GetAllChiTietXuatKho(string maXuatkho)
        {
            return dalXuatKhoChiTiet.GetAllChiTietXuatKho(maXuatkho);
        }

        public bool DeleteChiTietXuatKho(int maChiTietXuatKho)
        {
            return dalXuatKhoChiTiet.DeleteChiTietXuatKho(maChiTietXuatKho);
        }
    }
}
