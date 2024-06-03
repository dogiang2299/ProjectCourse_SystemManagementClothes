using BLL;
using DTO;
using Guna.UI2.WinForms;
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
    public partial class Supplier : Form
    {
        private DTO_Supplier supplier = new DTO_Supplier();
        private List<DTO_Supplier> dTO_Suppliers = new List<DTO_Supplier>();
        private BLL_Supplier _Supplier = new BLL_Supplier();
        public Supplier()
        {
            InitializeComponent();
        }
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private void guna2Button1_taothem_Click(object sender, EventArgs e)
        {
            txtManhacc.Enabled = true;
            txtManhacc.Text = "";
            txtTennhacc.Text = "";
            txtDiachincc.Text = "";
            txtEmail.Text = "";
            txtSodienthoai.Text = "";
        }

        private void Display()
        {
            List<DTO_Supplier> suppliers = _Supplier.GetSuppliers();

            // Kiểm tra xem có dữ liệu hay không
            if (suppliers == null || suppliers.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nhà cung cấp");
                return;
            }

            guna2DataGridView1.DataSource = suppliers;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[1].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[2].HeaderText = "Địa chỉ";
            guna2DataGridView1.Columns[3].HeaderText = "Email";
            guna2DataGridView1.Columns[4].HeaderText = "Số điện thoại";
        }
        int kiemtramatrung(string ma)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tbl_QuanLyNhaCungCap where manhacc = @manhacc";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@manhacc", ma);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }
        }
      
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(kiemtramatrung(txtManhacc.Text) == 1)
            {
                MessageBox.Show("Mã trùng, vui lòng nhập mã khác");
            }   
            else if(txtManhacc.Text == "" || txtTennhacc.Text == "" || txtEmail.Text == "" || txtSodienthoai.Text == "")
            {
                MessageBox.Show("Thông tin không được trống! Vui lòng nhập đầy đủ");
            }
            else
            {
                DTO_Supplier supplier = new DTO_Supplier
                {
                    manhacc = txtManhacc.Text,
                    tennhacc = txtTennhacc.Text,
                    diachincc = txtDiachincc.Text,
                    email = txtEmail.Text,
                    sodienthoai = txtSodienthoai.Text
                };

                bool result = _Supplier.AddSupplier(supplier);
                if (result)
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                    Display();
                }
                else
                {
                    MessageBox.Show("Thêm nhà cung cấp thất bại");
                }
            }
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DTO_Supplier supplier = new DTO_Supplier
            {
                manhacc = txtManhacc.Text,
                tennhacc = txtTennhacc.Text,
                diachincc = txtDiachincc.Text,
                email = txtEmail.Text,
                sodienthoai = txtSodienthoai.Text
            };

            bool result = _Supplier.EditSupplier(supplier);
            if (result)
            {
                MessageBox.Show("Cập nhật nhà cung cấp thành công");
                Display();
            }
            else
            {
                MessageBox.Show("Cập nhật nhà cung cấp thất bại");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string manhacc = txtManhacc.Text;

            bool result = _Supplier.RemoveSupplier(manhacc);
            if (result)
            {
                MessageBox.Show("Xóa nhà cung cấp thành công");
                Display();
            }
            else
            {
                MessageBox.Show("Xóa nhà cung cấp thất bại");
            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtManhacc.Enabled = false;
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                txtManhacc.Text = row.Cells["manhacc"].Value.ToString();
                txtTennhacc.Text = row.Cells["tennhacc"].Value.ToString();
                txtDiachincc.Text = row.Cells["diachincc"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtSodienthoai.Text = row.Cells["sodienthoai"].Value.ToString();
            }
        }

        private void guna2TextBox1_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            List<DTO_Supplier> suppliers = _Supplier.SearchSuppliers(keyword);

            if (suppliers == null || suppliers.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhà cung cấp nào");
                return;
            }

            guna2DataGridView1.DataSource = suppliers;
            guna2DataGridView1.Columns[0].HeaderText = "Mã nhà cung cấp";
            guna2DataGridView1.Columns[1].HeaderText = "Tên nhà cung cấp";
            guna2DataGridView1.Columns[2].HeaderText = "Địa chỉ";
            guna2DataGridView1.Columns[3].HeaderText = "Email";
            guna2DataGridView1.Columns[4].HeaderText = "Số điện thoại";
        }
    }
}
