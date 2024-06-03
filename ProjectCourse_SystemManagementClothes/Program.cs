using ProjectCourse_SystemManagementClothes.DetailForm;
using ProjectCourse_SystemManagementClothes.Mainfolder;
using ProjectCourse_SystemManagementClothes.Models;
using ProjectCourse_SystemManagementClothes.Order;
using ProjectCourse_SystemManagementClothes.Subfolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCourse_SystemManagementClothes
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SaleInvoice());
        }
    }
}
