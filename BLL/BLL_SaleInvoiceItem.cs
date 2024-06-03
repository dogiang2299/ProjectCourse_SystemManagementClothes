using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_SaleInvoiceItem
    {
        private DAL_SaleInvoiceItem item = new DAL_SaleInvoiceItem();
        public List<DTO_SaleInvoiceItem> GetAll(string maHoaDon)
        {
            return item.GetChiTietHoaDonBan(maHoaDon);
        }
    }
}
