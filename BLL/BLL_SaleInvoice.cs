using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_SaleInvoice
    {
        private DAL_SaleInvoice invoice = new DAL_SaleInvoice();
        public List<DTO_SaleInvoice> GetAll()
        {
            return invoice.GetAllHoaDonBan();
        }
        public string CreateHoaDonBan(DTO_SaleInvoice hoaDon, List<DTO_SaleInvoiceItem> chiTietHoaDon)
        {
            string maHoaDon = GenerateRandomMaHoaDon();
            hoaDon.MaHoaDonBan = maHoaDon;

            invoice.CreateHoaDonBan(hoaDon, chiTietHoaDon);

            return maHoaDon;
        }

        private string GenerateRandomMaHoaDon()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public List<DTO_SaleInvoice> GetToDay()
        {
            return invoice.GetToday();
        }
        public List<DTO_SaleInvoice> GetThismotnh()
        {
            return invoice.GetMonth();
        }
        public List<DTO_SaleInvoice> GetLastmonth()
        {
            return invoice.GetLastMonth();
        }

    }
}
