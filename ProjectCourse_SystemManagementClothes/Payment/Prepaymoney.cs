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
    public partial class Prepaymoney : Form
    {
        public string LabelTextFromConfirmForm { get; set; }

        private Confirm confirm1;
        public Prepaymoney(Confirm confirm)
        {
            InitializeComponent();
            this.confirm1 = confirm;
        }
        private int tienkhachtra = 0;
        
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out tienkhachtra))
            {
                if (tienkhachtra < 0)
                {
                    MessageBox.Show("Số tiền không thể âm.");
                    return;
                }
                else if (tienkhachtra > 0 && tienkhachtra < (confirm1.tongtienatca))
                {
                    int tienkhachconno = confirm1.tongtienatca - tienkhachtra;
                    guna2HtmlLabel5.Text = tienkhachconno.ToString();
                }
                else if (tienkhachtra >= confirm1.tongtienatca)
                {

                    guna2HtmlLabel5.Text = "0";
                }
            }

            else
            {
                MessageBox.Show("Số tiền không phù hợp");
            }
        }
        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
        }

        private void Prepaymoney_Load(object sender, EventArgs e)
        {
            guna2HtmlLabel7.Text = LabelTextFromConfirmForm;
        }
        private void guna2HtmlLabel5_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
