using BLL;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProjectCourse_SystemManagementClothes.Models
{
    public partial class AddCustomers : Form
    {
        private DTO_Customers dto_cus = new DTO_Customers();

        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private Customers _customers;
        BLL_Customers customers = new BLL_Customers();
        public AddCustomers(Customers customers)
        {
            InitializeComponent();
            _customers = customers;
            //guna2ShadowForm1.SetShadowForm(this);
        }
        private void DisplayCombobox()
        {
            List<DTO_CategoryCustomers> products = new List<DTO_CategoryCustomers>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                con.Open();
                string query = "SELECT * FROM tblQuanLyDanhMucKhachHang";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_CategoryCustomers DTO_Customers = new DTO_CategoryCustomers()
                    {
                        madanhmucKH = reader.GetInt32(reader.GetOrdinal("maphanloaikhachhang")),
                        tendanhmucKH = reader["tenphanloaikhachhang"].ToString(),

                    };
                    products.Add(DTO_Customers);
                }
            }
            madanhmu.DataSource = products;
            madanhmu.DisplayMember = "madanhmucKH";
            madanhmu.ValueMember = "madanhmucKH";
        }
        int kiemtramatrung(string ma)
        {
            int i;
            using (SqlConnection connection = new SqlConnection(connectSQL))
            {
                connection.Open(); // Mở kết nối
                string sql = $"select count(*) from tbl_QuanLyKhachHang where makhachhang = @makhachhang";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@makhachhang", ma);
                i = (int)cmd.ExecuteScalar(); // Thực hiện truy vấn ExecuteScalar
                return i;
            }
        }
        private bool kiemtratrungsdt(string sdt)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tbl_QuanLyKhachHang WHERE sdthoai = @sdthoai";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sdthoai", sdt);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private bool kiemtratrungemail(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectSQL))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tbl_QuanLyKhachHang WHERE email = @email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void guna2Button4_lammoi_Click(object sender, EventArgs e)
        {
            makhachhang.Enabled = true;
            makhachhang.Text = "";
            tenkhachhangs.Text = "";
            ngaysinhkhach.Text = "";
            gioitinh.Text = "";
            diachikhach.Text = "";
            sodthoais.Text = "";
            emails.Text = "";
        }

        private void guna2Button1_hoantat_Click(object sender, EventArgs e)
        {
            DisplayCombobox();


            if (tenkhachhangs.Text == "" || ngaysinhkhach.Text == "" || gioitinh.Text == "" || diachikhach.Text == "" || sodthoais.Text == "" || emails.Text == "" || tendanhmuc.Text == "")
            {
                guna2MessageDialog3.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }
            else if (kiemtratrungemail(emails.Text) || (kiemtratrungsdt(sdt.Text)))
            {
                guna2MessageDialog3.Show("Email hoặc số điện thoại này đã tồn tại! Vui lòng kiểm tra lại.");
            }
            else if (kiemtramatrung(makhachhang.Text) == 1)
            {
                guna2MessageDialog3.Show("Mã đã tồn tại! Vui lòng nhập lại mã khác");

            }
            else if (!CheckBirthDate())
            {
                return;
            }
            else
            {
                dto_cus = new DTO_Customers()
                {
                    makhachhang = Convert.ToInt32(makhachhang.Text),
                    tenkhachhang = tenkhachhangs.Text,
                    ngaysinh = ngaysinhkhach.Text,
                    gioitinh = gioitinh.Text,
                    diachi = diachikhach.Text,
                    sdthoai = sodthoais.Text,
                    email = emails.Text,
                    madanhmuc = Convert.ToInt32(madanhmu.Text),
                    danhmuckhachhang = tendanhmuc.Text,
                };

                customers.Create(dto_cus);
                guna2MessageDialog5.Show("Dữ liệu được tạo thành công!");
                _customers.Display();
            }

        }

        public void SetData(string[] textbox, string combobox)
        {
            makhachhang.Enabled = false;
            makhachhang.Text = textbox[0];
            tenkhachhangs.Text = textbox[1];

            // Chuyển đổi định dạng ngày tháng
            DateTime birthDate;
            if (DateTime.TryParse(textbox[2], out birthDate))
            {
                ngaysinhkhach.Text = birthDate.ToString("dd-MM-yyyy");
            }
            else
            {
                ngaysinhkhach.Text = textbox[2];
            }

            gioitinh.Text = textbox[3];
            diachikhach.Text = textbox[4];
            sodthoais.Text = textbox[5];
            emails.Text = textbox[6];
            tendanhmuc.Text = textbox[7];
            madanhmu.SelectedItem = combobox;

        }

        private bool CheckBirthDate()
        {
            DateTime birthDate;
            if (DateTime.TryParseExact(ngaysinhkhach.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
            {
                if (birthDate.Year <= 1900)
                {
                    guna2MessageDialog2.Show("Năm sinh không hợp lệ (phải lớn hơn 1900).");
                    ngaysinhkhach.Focus();
                    return false;
                }
                if (birthDate > DateTime.Now)
                {
                    guna2MessageDialog3.Show("Ngày sinh không thể lớn hơn ngày hiện tại.");
                    ngaysinhkhach.Focus();
                    return false;
                }
                return true;
            }
            else
            {
                guna2MessageDialog2.Show("Định dạng ngày sinh không hợp lệ (phải là dd-MM-yyyy).");
                ngaysinhkhach.Focus();
                return false;
            }
        }

        private void guna2Button2_update_Click(object sender, EventArgs e)
        {
            DisplayCombobox();
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn sửa thông tin ở khách hàng này không?");
            if (result == DialogResult.Yes)
            {

                dto_cus = new DTO_Customers()
                {
                    makhachhang = Convert.ToInt32(makhachhang.Text),
                    tenkhachhang = tenkhachhangs.Text,
                    ngaysinh = ngaysinhkhach.Text,
                    gioitinh = gioitinh.Text,
                    diachi = diachikhach.Text,
                    sdthoai = sodthoais.Text,
                    email = emails.Text,
                    madanhmuc = Convert.ToInt32(madanhmu.Text),
                    danhmuckhachhang = tendanhmuc.Text,
                };
                customers.Update(dto_cus);
                guna2MessageDialog5.Show("Dữ liệu được thay đổi thành công!");
                _customers.Display();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }
        private void guna2Button3_delete_Click(object sender, EventArgs e)
        {
            DisplayCombobox();
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn xoá thông tin ở khách hàng này không?");
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(makhachhang.Text);
                customers.Delete(id);
                guna2MessageDialog5.Show("Dữ liệu được xoá thành công!");
                _customers.Display();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }

        }

        private void makhachhangs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gioitinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }


        private void ngaysinhkhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string ngaysinh = ngaysinhkhach.Text;

        }

        private void tenkhachhangs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        private bool CheckLimit()
        {
            string sdt = sodthoais.Text;
            if (sdt.Length < 10)
            {
                guna2MessageDialog1.Show("Số điện thoại phải đủ 10 số");
                sodthoais.Focus();
                return false;
            }
            else if (sdt.Length > 10)
            {
                guna2MessageDialog1.Show("Số điện thoại đã vượt quá 10 số");
                sodthoais.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void sodthoais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddCustomers_Load(object sender, EventArgs e)
        {
            DisplayCombobox();
        }

        private void sodthoais_Leave(object sender, EventArgs e)
        {
            CheckLimit();
        }

        private void ngaysinhkhach_Leave(object sender, EventArgs e)
        {
            // CheckBirthDate();
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
        }

        private bool CheckCharacterEmail()
        {
            string email = emails.Text;
            if (!IsValidEmail(email))
            {
                guna2MessageDialog1.Show("Email phải hợp lệ và thuộc miền @gmail.com");
                emails.Focus();
                return false;
            }
            return true;
        }

        private void emails_Leave(object sender, EventArgs e)
        {
            CheckCharacterEmail();
        }

        private void madanhmu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (madanhmu.SelectedItem is DTO_CategoryCustomers customers)
            {
                tendanhmuc.Text = customers.tendanhmucKH;
            }
        }
    }
}
