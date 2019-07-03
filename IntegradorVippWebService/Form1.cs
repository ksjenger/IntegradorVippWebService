using System;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using IntegradorVippWebService.ServiceReference1;
using System.Linq;

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

            #region Abre o Arquivo
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String nomeArquivo = openFileDialog1.SafeFileName;
                Excel.Application xlsAPP = new Excel.Application();
                Excel.Workbook xlsWorkbook = xlsAPP.Workbooks.Open(nomeArquivo, 0, true, 5, "", "", false, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
                Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;
                //String rows = Excel.ListRow();


                #endregion

                #region Processa Planilha
                List<Postagem> lVipp = new List<Postagem>();

                Postagem oPostagem;
                foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
                {
                    if (xlsWorksheet.Name.Trim().Equals("nome da planilha"))
                    {
                        
                        //while -- do Numero de linhas

                        oPostagem = new Postagem();


                        Postagem oPostagemExistente = (from o in lVipp where o.Volumes[0].ObservacaoVisual.Equals(oPostagem.Volumes[0].ObservacaoVisual) select o).FirstOrDefault();
                        if (oPostagemExistente.Destinatario.Nome.Equals(string.Empty))
                        {
                            lVipp.Add(oPostagem);
                        }
                        else
                        {
                            ItemConteudo[] x = oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo;
                            Array.Resize(ref x, x.Length);
                            x[x.Length] = oPostagem.Volumes[0].DeclaracaoConteudo.ItemConteudo[0];

                            oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo = x;
                        }


                        //oPostagem.Volumes[0].DeclaracaoConteudo.ItemConteudo[0].

                        //fim while -- do Numero de linhas
                    }
                }
                WSVippPostar.PostagemVipp oSigep = new WSVippPostar.PostagemVipp();
                string oRetorno = oSigep.PostarObjeto(lVipp[0]).InnerXml;

                #endregion
            }
        }
    }
}
