using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_CategoryCustomers
    {
        private DAL_CategoryCustomers customers;
        public BLL_CategoryCustomers()
        {
            this.customers = new DAL_CategoryCustomers();
        }
        public List<DTO_CategoryCustomers> GetAll()
        {
            List<DTO_CategoryCustomers> customerss = customers.GetAll();
            return customerss;
        }
        public bool Create(DTO_CategoryCustomers customerss)
        {
            customers.Creat(customerss);
            return true;
        }
        public bool Update(DTO_CategoryCustomers customerss)
        {
            customers.Update(customerss);
            return true;
        }
        public bool Delete(int customerss)
        {
            customers.Delete(customerss);
            return true;
        }
        public List<DTO_CategoryCustomers> Search(string keyword)
        {
            List<DTO_CategoryCustomers> DTO_CategoryCustomers = customers.Search(keyword);
            return DTO_CategoryCustomers;
        }
    }
}
