using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Products
    {
        private DAL_Products customers;
        public BLL_Products()
        {
            this.customers = new DAL_Products();
        }
        public List<DTO_Products> GetAll()
        {
            List<DTO_Products> products = customers.GetAll();
            return products;
        }
        public bool Create(DTO_Products products)
        {
            return customers.Create(products);
        }
        public bool Update(DTO_Products products)
        {
            customers.Update(products);
            return true;
        }
        public bool Delete(int products)
        {
            return customers.Delete(products);
        }
        public List<DTO_Products> Search(string keyword)
        {
            List<DTO_Products> DTO_Products = customers.SearchDoctor(keyword);
            return DTO_Products;
        }
        public void UpdateInventoryQuantity(int slgmoi, int masp)
        {
           customers.UpdateInventoryQuantity(slgmoi, masp);
        }
        public static DataTable ThongKeTonKho()
        {
            return DAL_Products.ThongKeTonKho();
        }
    }
}
