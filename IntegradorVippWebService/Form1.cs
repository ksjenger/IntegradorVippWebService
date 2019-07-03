using System;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace IntegradorVippWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String nomeArquivo = openFileDialog1.SafeFileName;
                Excel.Application xlsAPP = new Excel.Application();
                Excel.Workbook xlsWorkbook = xlsAPP.Workbooks.Open(nomeArquivo, 0, true, 5, "", "", false, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);

            }
        }
    }
}
