using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Employer
    {
       
        private DAL_Employer DAL_Employer = new DAL_Employer();

        public List<DTO_Employer> GetAll()
        {
            return DAL_Employer.GetAllNhanVien();
        }

        public void Add(DTO_Employer nv)
        {
            DAL_Employer.AddNhanVien(nv);
        }

        public void Update(DTO_Employer nv)
        {
             DAL_Employer.UpdateNhanVien(nv);
        }

        public void Delete(int maNhanVien)
        {
             DAL_Employer.DeleteNhanVien(maNhanVien);
        }

        public List<DTO_Employer> Search(string keyword)
        {
            return DAL_Employer.SearchNhanVien(keyword);
        }
    

    }
}