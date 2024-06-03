using DTO;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.DetailForm
{
    public partial class ArrangeCus : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private Customers _customers;
        public ArrangeCus(Customers customers)
        {
            InitializeComponent();
            _customers = customers;
        }
        private void guna2Button2_apply_Click(object sender, EventArgs e)
        {
           if(guna2RadioButton1_AZ.Checked)
            {
                _customers.SortAndDisplayData("Từ A  -   Z");
            }    
           else if(guna2RadioButton2_ZA.Checked)
            {
                _customers.SortAndDisplayData("Từ Z  -   A");
            }
           else if(guna2RadioButton3_DATE.Checked)
            {
                _customers.SortAndDisplayData("Theo ngày tạo khách hàng");
            }
        }

        private void guna2Button1_again_Click(object sender, EventArgs e)
        {
            // cần khắc phục nè
            _customers.Display();

        }
    }
}
