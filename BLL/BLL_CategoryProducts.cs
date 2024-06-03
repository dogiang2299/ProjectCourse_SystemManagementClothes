using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_CategoryProducts
    {
        private DAL_CategoryProducts customers;
        public BLL_CategoryProducts()
        {
            this.customers = new DAL_CategoryProducts();
        }
        public List<DTO_CategoryProducts> GetAll()
        {
            List<DTO_CategoryProducts> customerss = customers.GetAll();
            return customerss;
        }
        public bool Create(DTO_CategoryProducts customerss)
        {
            customers.Creat(customerss);
            return true;
        }
        public bool Update(DTO_CategoryProducts customerss)
        {
            customers.Update(customerss);
            return true;
        }
        public bool Delete(int customerss)
        {
            customers.Delete(customerss);
            return true;
        }
        public List<DTO_CategoryProducts> Search(string keyword)
        {
            List<DTO_CategoryProducts> DTO_CategoryProducts = customers.Search(keyword);
            return DTO_CategoryProducts;
        }
    }
}
