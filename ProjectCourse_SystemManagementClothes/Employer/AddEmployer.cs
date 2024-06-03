using BLL;
using DTO;
using Guna.UI2.WinForms;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes.Models
{
    public partial class AddEmployer : Form
    {
        private DTO_Employer dTO_employ = new DTO_Employer();
        private Employer employer;
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public AddEmployer(Employer _em)
        {
            InitializeComponent();
            this.employer = _em;
            
        }
        BLL_Employer _Employer = new BLL_Employer();
        private void guna2Button4_lammoi_Click(object sender, EventArgs e)
        {
           
            tennhanvien.Text = "";
            sdt.Text = "";
            diachi.Text = "";
            email.Text = "";
        }
       
        private void guna2Button1_save_Click(object sender, EventArgs e)
        {
           
             if (tennhanvien.Text == "" || diachi.Text == "" || sdt.Text == "" || email.Text == "" || chucvu_combo.Text == "" )
            {
                guna2MessageDialog2.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
            }
           
            else
            {
                dTO_employ = new DTO_Employer()
                {
                    TenNhanVien = tennhanvien.Text,
                    SoDienThoai = sdt.Text,
                    NgaySinh = DateTime.ParseExact(ngaysinh.Text, "dd-MM-yyyy", null),
                    GioiTinh = gioitinh.Text,
                    DiaChi = diachi.Text,
                    Email = email.Text,
                    TenChucVu = chucvu_combo.Text,
                };
                _Employer.Add(dTO_employ);
                guna2MessageDialog5.Show("Dữ liệu được tạo thành công!");
                employer.Display();
            }

        }

        public void SetData(string[] textbox, string[] combobox)
        {
            manhanvien.Text = textbox[0];
            tennhanvien.Text = textbox[1];
            sdt.Text = textbox[2];
            ngaysinh.Text = textbox[3];
            gioitinh.Text = textbox[4];
            diachi.Text = textbox[5];
            email.Text = textbox[6];
            chucvu_combo.SelectedItem = combobox[0];
        }

        private void guna2Button2_update_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn sửa thông tin ở nhân viên này không?");
            if (result == DialogResult.Yes)
            {
                manhanvien.Enabled = false;
                int makhach = Convert.ToInt32(manhanvien.Text);
                string tenkhach = tennhanvien.Text;
                string sodienthoai = sdt.Text;
                string diachis = diachi.Text;
                string emailL = email.Text;
                string chucvus = chucvu_combo.Text;

                DateTime ngaySinh;
                bool isDateValid = DateTime.TryParseExact(ngaysinh.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh);

                if (!isDateValid)
                {
                    guna2MessageDialog5.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng dd-MM-yyyy.");
                    return;
                }

                DTO_Employer dTO_Customers = new DTO_Employer()
                {
                    MaNhanVien = makhach,
                    TenNhanVien = tenkhach,
                    NgaySinh = ngaySinh,
                    GioiTinh = gioitinh.Text,
                    SoDienThoai = sodienthoai,
                    DiaChi = diachis,
                    Email = emailL,
                    TenChucVu = chucvus,
                };

                _Employer.Update(dTO_Customers);
                guna2MessageDialog5.Show("Dữ liệu được thay đổi thành công!");
                employer.Display();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }
        private bool CheckLimit()
        {
            string sdts = sdt.Text;
            if (sdts.Length < 10)
            {
                guna2MessageDialog1.Show("Số điện thoại phải đủ 10 số");
                sdt.Focus();
                return false;
            }
            else if (sdts.Length > 10)
            {
                guna2MessageDialog1.Show("Số điện thoại đã vượt quá 10 số");
                sdt.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
        }

        private bool CheckCharacterEmail()
        {
            string emailL = email.Text;
            if (!IsValidEmail(emailL))
            {
                guna2MessageDialog1.Show("Email phải hợp lệ và thuộc miền @gmail.com");
                email.Focus();
                return false;
            }
            return true;
        }
        private void guna2Button3_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog4.Show("Bạn có muốn xoá thông tin ở nhân viên này không?");
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(manhanvien.Text);
                _Employer.Delete(id);
                guna2MessageDialog5.Show("Dữ liệu được xoá thành công!");
                employer.Display();
            }
            else
            {
                guna2MessageDialog5.Show("Dữ liệu vẫn được giữ nguyên!");
            }
        }

        private void AddEmployer_Load(object sender, EventArgs e)
        {
        }
      
        private void tennhanvien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void email_Leave(object sender, EventArgs e)
        {
            CheckCharacterEmail();
        }

        private void sdt_Leave(object sender, EventArgs e)
        {
            CheckLimit();
        }

        private void sdt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
      
        private void guna2Separator2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
