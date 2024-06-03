using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Warehouse
    {
        DAL_Warehouse dalNhapKho = new DAL_Warehouse();
        public OperationResultWare Add(DTO_Warehouse nhapKho, List<DTO_WarehouseItem> chiTietNhapKhos)
        {
            return dalNhapKho.Add(nhapKho, chiTietNhapKhos);
        }
        public List<DTO_Warehouse> SearchImportBooks(string keyword)
        {
            return dalNhapKho.SearchWareBooks(keyword);
        }
        public List<DTO_Warehouse> GetAll()
        {
            return dalNhapKho.GetAllWarehouses();
        }
        public void Delete(string manhapkho)
        {
             dalNhapKho.DeleteWarehouse(manhapkho);
        }
       
        public List<DTO_Warehouse> GetToDay()
        {
            return dalNhapKho.GetToday();
        }
        public List<DTO_Warehouse> GetThismotnh()
        {
            return dalNhapKho.GetMonth();
        }
        public List<DTO_Warehouse> GetLastmonth()
        {
            return dalNhapKho.GetLastMonth();
        }

    }
}
