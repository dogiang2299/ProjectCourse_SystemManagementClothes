using BLL;
using DocumentFormat.OpenXml.ExtendedProperties;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Mainfolder;
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

namespace ProjectCourse_SystemManagementClothes.Models
{
    public partial class AddCategoryCustomers : Form
    {
        private Customers customers;
        private BLL_CategoryCustomers categoryCustomers = new BLL_CategoryCustomers();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public AddCategoryCustomers(Customers _cusca)
        {
            InitializeComponent();
            customers = _cusca;
        }
        public void SetData(string[] texbox)
        {
            madanhmuc.Enabled = false;
            madanhmuc.Text = texbox[0];
            tendanhmuc.Text = texbox[1];
            
        }
        private void AddCategoryCustomers_Load(object sender, EventArgs e)
        {
            customers.DisplayCate();
        }
        private void guna2Button4_lammoi_Click(object sender, EventArgs e)
        {
            madanhmuc.Enabled = true;
            madanhmuc.Text = "";
            tendanhmuc.Text = "";
        }
        int kiemtramatrung(string ma)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tblQuanLyDanhMucKhachHang where maphanloaikhachhang = @maphanloaikhachhang";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@maphanloaikhachhang", ma);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }
        }
        private void guna2Button1_save_Click(object sender, EventArgs e)
        {
            if (kiemtramatrung(madanhmuc.Text) == 1)
            {
                guna2MessageDialog1.Show("Mã danh mục khách hàng đã tồn tại! Vui lòng nhập mã danh mục khách hàng khác");
            }
            else if (madanhmuc.Text == "" || tendanhmuc.Text == "")
            {
                guna2MessageDialog2.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }
            
            else
            {
                DTO_CategoryCustomers dTO_catecus = new DTO_CategoryCustomers()
                {
                    madanhmucKH = Convert.ToInt32(madanhmuc.Text),
                    tendanhmucKH = tendanhmuc.Text,

                };
                categoryCustomers.Create(dTO_catecus);
                guna2MessageDialog5.Show("Dữ liệu được tạo thành công!");
                customers.DisplayCate();
            }

        }

        private void guna2Button2_update_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn sửa thông tin ở nhân viên này không?");
            if (result == DialogResult.Yes)
            {
                madanhmuc.Enabled = false;
                int makhach = Convert.ToInt32(madanhmuc.Text);
                string tenkhach = tendanhmuc.Text;
                
                DTO_CategoryCustomers dTO_Customers = new DTO_CategoryCustomers()
                {
                    madanhmucKH = makhach,
                    tendanhmucKH = tenkhach,
                   
                };
                categoryCustomers.Update(dTO_Customers);
                guna2MessageDialog5.Show("Dữ liệu được thay đổi thành công!");
                customers.DisplayCate();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }

        private void guna2Button3_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn xoá thông tin ở nhân viên này không?");
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(madanhmuc.Text);
                categoryCustomers.Delete(id);
                guna2MessageDialog5.Show("Dữ liệu được xoá thành công!");
                customers.DisplayCate();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }

        private void madanhmuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tendanhmuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
