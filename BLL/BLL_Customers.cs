using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Customers
    {
        private DAL_Customers customers;
        public BLL_Customers()
        {
            this.customers = new DAL_Customers();
        }
        public List<DTO_Customers> GetAll()
        {
            List<DTO_Customers> customerss = customers.GetAll();
            return customerss;
        }
        public bool Create(DTO_Customers customerss)
        {
            customers.Creat(customerss);
            return true;
        }
        public bool Update(DTO_Customers customerss)
        {
            customers.Update(customerss);
            return true;
        }
        public bool Delete(int customerss)
        {
            customers.Delete(customerss);
            return true;
        }
        public List<DTO_Customers> Search(string keyword)
        {
            List<DTO_Customers> dTO_Customers = customers.SearchDoctor(keyword);
            return dTO_Customers;
        }
    }
}
