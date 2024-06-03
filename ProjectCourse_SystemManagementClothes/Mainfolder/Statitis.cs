using BLL;
using DTO;
using ProjectCourse_SystemManagementClothes.Warehouse;
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
    public partial class Statitis : Form
    {
        private BLL_Products _Products = new BLL_Products();
        private string connectSQL = @"Data Source=localhost;Initial Catalog=SystemManagementClothes_VOGUE;Integrated Security=True";
        public Statitis()
        {
            InitializeComponent();
        }
        public void Display()
        {
            List<DTO_Products> emp = _Products.GetAll();
            guna2DataGridView1.DataSource = emp;
            guna2DataGridView1.Columns[0].HeaderText = "Mã sản phẩm";
            guna2DataGridView1.Columns[1].HeaderText = "Mã barcode";
            guna2DataGridView1.Columns[2].HeaderText = "Tên sản phẩm";
            guna2DataGridView1.Columns[3].HeaderText = "Giá gốc";
            guna2DataGridView1.Columns[4].HeaderText = "Giá bán";
            guna2DataGridView1.Columns[5].HeaderText = "Đơn vị";
            guna2DataGridView1.Columns[6].HeaderText = "Số lượng tồn kho";
            guna2DataGridView1.Columns[7].HeaderText = "Mô tả";
            guna2DataGridView1.Columns[8].HeaderText = "Gắn nhãn sản phẩm";
            guna2DataGridView1.Columns[9].HeaderText = "Kích thước";
            guna2DataGridView1.Columns[10].HeaderText = "Danh mục sản phẩm";
            guna2DataGridView1.Columns[11].HeaderText = "Tình trạng sản phẩm";
            guna2DataGridView1.Columns[12].HeaderText = "Hình ảnh sản phẩm";
            guna2DataGridView1.Columns[13].HeaderText = "Mã mục sản phẩm";
            guna2DataGridView1.Columns[13].Visible = false;


        }
        private void DisplayIDPro()
        {
            List<DTO_Products> employees = new List<DTO_Products>();

            using (SqlConnection con = new SqlConnection(connectSQL))
            {
                try
                {
                    con.Open();
                    string query = "SELECT idsanpham  FROM tblQuanLySanPham_VOGUE";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DTO_Products dTO_Employer = new DTO_Products()
                        {
                            masanpham = reader.GetInt32(reader.GetOrdinal("idsanpham")),
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
                guna2ComboBox1.DataSource = employees;
                guna2ComboBox1.DisplayMember = "masanpham";
                guna2ComboBox1.ValueMember = "masanpham";
                guna2ComboBox1.Refresh();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sản phẩm để hiển thị.");
            }
        }
        // print
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            //CrystalReport2 CrystalReport2 = new CrystalReport2();
            //CrystalReport2.SetDataSource(BLL_Products.ThongKeTonKho());
            //BaoCaoTonKho baoCaoTonKho = new BaoCaoTonKho();
            //baoCaoTonKho.crystalReportViewer1.ReportSource = CrystalReport2;
            //this.Hide();
            //baoCaoTonKho.Show();
        }

        private void Statitis_Load(object sender, EventArgs e)
        {
            Display();
            DisplayIDPro();
        }
    }
}
