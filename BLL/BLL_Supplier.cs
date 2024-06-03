using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Supplier
    {
        DAL_Supllier dalSupplier = new DAL_Supllier();
        public List<DTO_Supplier> SearchSuppliers(string keyword)
        {
            return dalSupplier.SearchSuppliers(keyword);
        }
        public bool AddSupplier(DTO_Supplier supplier)
        {
            return dalSupplier.InsertSupplier(supplier);
        }

        public bool EditSupplier(DTO_Supplier supplier)
        {
            return dalSupplier.UpdateSupplier(supplier);
        }

        public bool RemoveSupplier(string manhacc)
        {
            return dalSupplier.DeleteSupplier(manhacc);
        }

        public List<DTO_Supplier> GetSuppliers()
        {
            return dalSupplier.GetSuppliers();
        }
    }
}
