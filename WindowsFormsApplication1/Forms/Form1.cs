using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using IntegradorWebService.Services;
using IntegradorWebService.WSVIPP;
using IntegradorWebService.ExcelServices;

namespace IntegradorWebService
{
    public partial class Form1 : Form
    {

        List<Postagem> lVipp = new List<Postagem>();

        public static string path;
        public static string caminho;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelecione_Click(object sender, EventArgs e)
        {
            #region Serialização do arquivo XML 
            /*
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<FormatacaoPlanilha>));
            var subReq = new List<FormatacaoPlanilha>();

            using (var sww = new StringWriter())
            {
               using (XmlWriter writer = XmlWriter.Create(sww))
               {
                   xsSubmit.Serialize(writer, lFormatacao);
                   var plainTextBytes = Encoding.UTF8.GetBytes(sww.ToString());
                   string x = Convert.ToBase64String(plainTextBytes);
               }
            }
            */
            #endregion

            #region Abre o Arquivo
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    path = openFileDialog.FileName;
                }
            }
            #endregion

            lVipp = ProcessaPlanilha.ListaDePostagem(path);



            #region Chama o metodo para Postar Objeto
            VIPP.PostarObjeto.Postagem(lVipp);
            #endregion


            #region Salvar arquivo processado
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.FileName = "ArquivoProcessado.xlsx";
                saveFileDialog.Title = "Salvar o Arquivo Processado";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    caminho = saveFileDialog.FileName;
                    GravaRetornoExcel.GravaRetorno();
                }
            }

            #endregion

            GC.Collect();
            Close();
        }
    }
}
