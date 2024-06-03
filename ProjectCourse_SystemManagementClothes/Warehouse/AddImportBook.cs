using BLL;
using DTO;
using Guna.UI2.WinForms;
using ProjectCourse_SystemManagementClothes.Order;
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

namespace ProjectCourse_SystemManagementClothes.Warehouse
{
    public partial class AddImportBook : Form
    {
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        private BLL_ImportBook book = new BLL_ImportBook();
        private BLL_Products _Products = new BLL_Products();
        private ImportBook bookk;
        public AddImportBook(ImportBook importBook)
        {
            InitializeComponent();
            this.bookk = importBook;
        }
       
        public void DisplayImport()
        {
            List<DTO_Products> emp = _Products.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";//
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";//
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";//
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";//
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";//
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";//
            guna2DataGridView1.Columns[13].HeaderText = "Mã danh mục sản phẩm";//
            // VISIBLE
            guna2DataGridView1.Columns[1].Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            guna2DataGridView1.Columns[7].Visible = false;
            guna2DataGridView1.Columns[9].Visible = false;
            guna2DataGridView1.Columns[12].Visible = false;
            guna2DataGridView1.Columns[10].Visible = false;
            guna2DataGridView1.Columns[11].Visible = false;
            guna2DataGridView1.Columns[13].Visible = false;
        }
        private int thanhtiens = 0;
        private void Up()
        {
            int soluongsaunhap;
            int gianhaphang;
            if (int.TryParse(guna2TextBox4.Text, out gianhaphang) && int.TryParse(soluongnhapkho.Text, out soluongsaunhap))
            {
                thanhtiens = gianhaphang * soluongsaunhap;
            }
            else
            {
                thanhtiens = 0;
            }
            guna2HtmlLabel6.Text = thanhtiens.ToString();
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                guna2TextBox2.Enabled = false;
                guna2TextBox3.Enabled = false;
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                guna2TextBox2.Text = selectedRow.Cells[0].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells[2].Value.ToString();
                guna2TextBox4.Text = selectedRow.Cells[3].Value.ToString();
                soluongtonkho.Text = selectedRow.Cells[6].Value.ToString();
            }
        }

        private void AddImportBook_Load(object sender, EventArgs e)
        {
           
            DisplayImport();
            DisplayEmployer();
            DisplaySupplier();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            soluongtonkho.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            soluongnhapkho.Text = "";
        }
        private void DisplayEmployer()
        {
            List<DTO_Employer> employees = new List<DTO_Employer>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                try
                {
                    con.Open();
                    string query = "SELECT MaNhanVien, TenNhanVien FROM tbl_Quan_Ly_Nhan_Vien";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DTO_Employer dTO_Employer = new DTO_Employer()
                        {
                            MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                            TenNhanVien = reader["TenNhanVien"].ToString()
                        };
                        employees.Add(dTO_Employer);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message);
                }
            }
            if (employees.Count > 0)
            {
                manhanviens.DataSource = employees;
                manhanviens.DisplayMember = "MaNhanVien";
                manhanviens.ValueMember = "MaNhanVien";
                manhanviens.Refresh(); 
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhân viên để hiển thị.");
            }
        }

        private void DisplaySupplier()
        {
            List<DTO_Supplier> employees = new List<DTO_Supplier>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                con.Open();
                string query = "SELECT manhacc, tennhacc FROM tbl_QuanLyNhaCungCap";
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTO_Supplier dTO_Employer = new DTO_Supplier()
                    {
                        manhacc = reader["manhacc"].ToString(),
                        tennhacc = reader["tennhacc"].ToString()
                    };
                    employees.Add(dTO_Employer);
                }
            }

            manhacungcap.DataSource = employees;
            manhacungcap.DisplayMember = "manhacc";
            manhacungcap.ValueMember = "manhacc";
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(soluongnhapkho.Text))
            {
                guna2MessageDialog1.Show("Thông tin không được để trống! Vui lòng nhập đầy đủ");
                return;
            }

            try
            {
                int maSanPham = Convert.ToInt32(guna2TextBox2.Text);
                string tenSanPham = guna2TextBox3.Text;
                int donGiaNhap = Convert.ToInt32(guna2TextBox4.Text);
                int soluongTonkho = Convert.ToInt32(soluongtonkho.Text);
                int soLuongNhapKho = Convert.ToInt32(soluongnhapkho.Text);
                int tongtiennhap = Convert.ToInt32(guna2HtmlLabel6.Text);
                List<DTO_ImportBookItem> chiTietHoaDon = new List<DTO_ImportBookItem>
                {
                    new DTO_ImportBookItem
                    {
                        MaSanPham = maSanPham,
                        TenSanPham = tenSanPham,
                        DonGiaNhap = donGiaNhap,
                        SoLuongTonKho = soluongTonkho,
                        SoLuongNhapKho = soLuongNhapKho
                    }
                };

                var nhapKhochinh = new DTO_ImportBook
                {
                    MaNhanVien = Convert.ToInt32(manhanviens.SelectedValue),
                    TenNhanVien = tennhanvien.Text,
                    MaNhaCungCap = manhacungcap.Text,
                    TenNhaCungCap = tennhacungcap.Text,
                    TongTienNhap = tongtiennhap,
                    NgayNhapKho = DateTime.Now
                };

                var result = book.Add(nhapKhochinh, chiTietHoaDon);

                if (result.Success)
                {
                    DialogResult dialogResult = guna2MessageDialog2.Show("Thêm nhập kho thành công! Mã phiếu nhập là: "  + nhapKhochinh.MaNhapKho);
                    if(dialogResult == DialogResult.OK)
                    {
                        bookk.DisplayImportBook();
                        DisplayImport();
                    }

                }
                else
                {
                    MessageBox.Show("Thêm nhập kho thất bại: " + result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho các trường giá và số lượng." + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void manhanviens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manhanviens.SelectedItem is DTO_Employer employer)
            {
                tennhanvien.Text = employer.TenNhanVien.ToString();
            }
        }

        private void manhacungcap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manhacungcap.SelectedItem is DTO_Supplier dto)
            {
                tennhacungcap.Text = dto.tennhacc.ToString();
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            Up();
        }
    }
}
