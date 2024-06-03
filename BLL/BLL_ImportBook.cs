using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ImportBook
    {
        DAL_ImportBook dalNhapKho = new DAL_ImportBook();
        public OperationResult Add(DTO_ImportBook nhapKho, List<DTO_ImportBookItem> chiTietNhapKhos)
        {
            return dalNhapKho.Add(nhapKho, chiTietNhapKhos);
        }
        public List<DTO_ImportBook> SearchImportBooks(string keyword)
        {
            return dalNhapKho.SearchImportBooks(keyword);
        }
        public List<DTO_ImportBook> GetAll()
        {
            return dalNhapKho.GetAll();
        }
        public void Delete(string manhapkho)
        {
            dalNhapKho.DeleteWarehouse(manhapkho);
        }
        public bool Update(int masp, int newPrice, int newQuanlity)
        {
            return dalNhapKho.UpdateProductPriceAndQuantity(masp, newPrice, newQuanlity);
        }
        public List<DTO_ImportBook> GetToDay()
        {
            return dalNhapKho.GetToday();
        }
        public List<DTO_ImportBook> GetThismotnh()
        {
            return dalNhapKho.GetMonth();
        }
        public List<DTO_ImportBook> GetLastmonth()
        {
            return dalNhapKho.GetLastMonth();
        }

    }
}
