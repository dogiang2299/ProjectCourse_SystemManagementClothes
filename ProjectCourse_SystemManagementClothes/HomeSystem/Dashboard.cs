using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using ProjectCourse_SystemManagementClothes.Order;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes
{
    public partial class Dashboard : Form
    {
        private Customers customers = new Customers();
        public Dashboard()
        {
            InitializeComponent();
            DisplayMenuBar();

        }
        private void hideSubMenu()
        {
            if(guna2Panel1_SanPham.Visible == true)
            {
                guna2Panel1_SanPham.Visible = false;
            }
            if (guna2Panel1_KhoHang.Visible == true)
            {
                guna2Panel1_KhoHang.Visible = false;
            }
            if (guna2Panel4_KhachHang.Visible == true)
            {
                guna2Panel4_KhachHang.Visible = false;
            }
            if (guna2Panel1_DonHang.Visible == true)
            {
                guna2Panel1_DonHang.Visible = false;
            }

        }
        private void DisplayMenuBar()
        {
            guna2Panel1_SanPham.Visible = false;
            guna2Panel1_KhoHang.Visible = false;
            guna2Panel4_KhachHang.Visible = false;
            guna2Panel1_DonHang.Visible = false;
        }
        private void showMenu(Panel subMenu)
        {
            if(subMenu.Visible == false) 
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;

            }
        }

        private void sanpham_Click(object sender, EventArgs e)
        {
            showMenu(guna2Panel1_SanPham);
        }

        private void khohang_Click(object sender, EventArgs e)
        {
            showMenu(guna2Panel1_KhoHang);
        }

        private void khachhang_Click(object sender, EventArgs e)
        {
            showMenu(guna2Panel4_KhachHang);
        }

        private void danhmucsanpham_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            container(new CategoryProduct());
        }

      

        private void kiemkho_Click(object sender, EventArgs e)
        {
            
        }

        private void sonhaphang_Click(object sender, EventArgs e)
        {
            ImportBook importBook = new ImportBook();
            hideSubMenu();
            container(new ImportBook());
        }

        private void soxuathang_Click(object sender, EventArgs e)
        {
            WarehouseBook warehouseBook = new WarehouseBook();
            hideSubMenu();
            container(new WarehouseBook());
        }


        private void danhmuckhachhang_Click(object sender, EventArgs e)
        {
            CategoryCustomers categoryCustomers = new CategoryCustomers(customers);
            hideSubMenu();
            container(new CategoryCustomers(customers));
        }

        private Form active = null;
       
        private void container(object form)
        {
            if(guna2Panel3_FormChild.Controls.Count > 0)
            {
                guna2Panel3_FormChild.Controls.Clear();
            }
            Form form1 = form as Form;
            form1.TopLevel = false;
            form1.FormBorderStyle = FormBorderStyle.None;
            form1.Dock = DockStyle.Fill;
            guna2Panel3_FormChild.Controls.Add(form1);
            guna2Panel3_FormChild.Tag = form1;
            form1.Show();
        }

        private void trangchu_Click(object sender, EventArgs e)
        {
           
        }

        private void banhang_Click(object sender, EventArgs e)
        {
            container(new Sell());
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            container(new Products());
        }
        private void nhanvien_Click(object sender, EventArgs e)
        {
            container(new Employer());
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            container(new Customers());
        }
      
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            showMenu(guna2Panel1_DonHang);
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            SaleInvoice saleInvoice = new SaleInvoice();
            hideSubMenu();
            container(new SaleInvoice());
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
           // cứ cho là import
            ImportBook import = new ImportBook();
            hideSubMenu();
            container(new ImportBook());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Statitis import = new Statitis();
            hideSubMenu();
            container(new Statitis());
        }
    }
}
