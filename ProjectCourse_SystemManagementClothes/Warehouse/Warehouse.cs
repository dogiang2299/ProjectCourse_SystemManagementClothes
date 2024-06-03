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
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.Mainfolder
{
    public partial class Warehouse : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private DTO_Products products1 = new DTO_Products();
        public Warehouse()
        {
            InitializeComponent();
        }

        private void guna2Button3_importBook_Click(object sender, EventArgs e)
        {
            ImportBook importBook = new ImportBook();
            importBook.Show();
        }

        private void guna2Button4_goodDelivery_Click(object sender, EventArgs e)
        {
            WarehouseBook warehouseBook = new WarehouseBook();
            warehouseBook.Show();
        }
        private void DisplaySurvivalValue()
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT SUM(soluongtonkho) FROM tblQuanLySanPham_VOGUE";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    guna2HtmlLabel3.Text = result != null ? result.ToString() : "0";
                }
            }
        }

        private void Warehouse_Load(object sender, EventArgs e)
        {
            DisplaySurvivalValue();
        }
    }
}
