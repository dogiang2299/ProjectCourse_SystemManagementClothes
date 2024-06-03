using ProjectCourse_SystemManagementClothes.Mainfolder;
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

namespace ProjectCourse_SystemManagementClothes.DetailForm
{
    public partial class ArrangePro : Form
    {
        private Products products = new Products();
        private Sell sell1 = new Sell();
        public ArrangePro(Products pro)
        {
            InitializeComponent();
            products = pro;
        }
        public ArrangePro(Sell sell)
        {
            InitializeComponent();
            sell1 = sell;
        }


        private void guna2Button1_again_Click(object sender, EventArgs e)
        {
            products.Display();
            sell1.Display();
        }

        private void guna2Button2_apply_Click(object sender, EventArgs e)
        {

            if (guna2RadioButton1_AZ.Checked)
            {
               
               // products.SortAndDisplayData("Từ A  -   Z");
                sell1.SortAndDisplayData("Từ A  -   Z");
            }
            else if (guna2RadioButton2_ZA.Checked)
            {
               // products.SortAndDisplayData("Từ Z  -   A");
                sell1.SortAndDisplayData("Từ Z  -   A");
            }
            //
            else if (guna2RadioButton1_new.Checked)
            {
               // products.SortAndDisplayData("Mới nhất");
                sell1.SortAndDisplayData("Mới nhất");

            }
            else if (guna2RadioButton3_banchay.Checked)
            {
               // products.SortAndDisplayData("Sản phẩm bán chạy");
                sell1.SortAndDisplayData("Sản phẩm bán chạy");
            }
            if (guna2RadioButton_capthap.Checked)
            {
               // products.SortAndDisplayData("Còn hàng từ cao đến thấp");
                sell1.SortAndDisplayData("Còn hàng từ cao đến thấp");
            }
            else if (guna2RadioButton2_thapcao.Checked)
            {
                //products.SortAndDisplayData("Còn hàng từ thấp đến cao");
                sell1.SortAndDisplayData("Còn hàng từ thấp đến cao");
            }
            else if(guna2RadioButton4_giacaothap.Checked)
            {
                // products.SortAndDisplayData("Giá từ cao đến thấp");
                sell1.SortAndDisplayData("Giá từ cao đến thấp");
            }
            else if (guna2RadioButton5_giathapcao.Checked)
            {
             // products.SortAndDisplayData("Giá từ thấp đến cao");
                sell1.SortAndDisplayData("Giá từ thấp đến cao");
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }
    }
}
