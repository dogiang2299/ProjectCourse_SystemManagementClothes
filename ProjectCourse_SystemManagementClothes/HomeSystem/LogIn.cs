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

namespace ProjectCourse_SystemManagementClothes
{
    public partial class LogIn : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";

        public LogIn()
        {
            InitializeComponent();
        }

        private void DangNhap_Click(object sender, EventArgs e)
        {
            string username = tenDangNhap.Text;
            string pas = matKhau.Text;
            string emaill = nhapEmail.Text;
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT * FROM tbl_QuanLyNguoiDung WHERE tendangnhap = @tendangnhap and pas = @pas and email = @email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tendangnhap", username);
                    cmd.Parameters.AddWithValue("@pas", pas);
                    cmd.Parameters.AddWithValue("@email", emaill);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (tenDangNhap.Text == "" || matKhau.Text == "" || nhapEmail.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập thông tin bắt buộc", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (pas != matKhau.Text)
                        {
                            MessageBox.Show("Mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            matKhau.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thành công!");
                            Dashboard loading = new Dashboard();
                            loading.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại!");
                    }

                }
            }
        }
    }
}
