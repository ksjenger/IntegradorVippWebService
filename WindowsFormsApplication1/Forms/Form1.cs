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
using IntegradorWebService.WSVippPostar;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using IntegradorWebService.XmlWsVIPP;

namespace IntegradorWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelecione_Click(object sender, EventArgs e)
        {

            #region Define a formatação da Planilha
            List<FormatacaoPlanilha> lFormatacao = new List<FormatacaoPlanilha>();
            FormatacaoPlanilha oFormatacao = new FormatacaoPlanilha();
            #endregion

            #region Serialização do arquivo XML 
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

            #endregion


            #region Recupera a formatação da planilha do App.Config

            lFormatacao = FormatacaoPlanilha.ListarFormatacao();

            #endregion

            #region Abre o Arquivo
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                String path = openFileDialog.FileName;
                Excel.Application xlsAPP = new Excel.Application();
                Excel.Workbook xlsWorkbook = xlsAPP.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
                Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;
                #endregion


                #region Processa Planilha
                List<PostagemVipp> lVipp = new List<PostagemVipp>();
                PostagemVipp oPostagem;

                //For que acessa todas as planilhas
                foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
                {

                    //Acessa a aba da Planilha com o nome "Control Respuesta".
                    if (xlsWorksheet.Name.Trim().Equals("Control Respuesta"))
                    {

                        Excel.Range xlsWorksRows = xlsWorksheet.Rows;

                        //for do Numero de linhas
                        foreach (Excel.Range xlsWorkCell in xlsWorksRows)
                        {

                            Excel.Range xlsCell = xlsWorkCell.Cells;

                            //For para percorrer a lista de Formatação
                            Destinatario oDestinatario = new Destinatario();
                            XmlWsVIPP.VolumeObjeto[] oVolumeObjetos = new XmlWsVIPP.VolumeObjeto[] { new XmlWsVIPP.VolumeObjeto() };
                            XmlWsVIPP.ItemConteudo[] oItemConteudos;
                            XmlWsVIPP.Volumes[] oVolumes = new XmlWsVIPP.Volumes[] { new Volumes() };
                            //PostagemVipp oPostagemExistente = new PostagemVipp();

                            foreach (FormatacaoPlanilha list in lFormatacao)
                            {

                                String atributo = list.NomeAtributo;
                                int coluna = list.Coluna;
                                String valor = null;

                                //for do numero de celulas 
                                foreach (Excel.Range xlsCells in xlsCell)
                                {

                                    //
                                    if (xlsCells.Column.Equals(list.Coluna))
                                    {
                                        valor = xlsCells.Value;
                                    }
                                    else if (xlsCells.Column > 100)
                                    {
                                        break;
                                    }
                                }

                                //MessageBox.Show(" " + coluna + " " + atributo + " " + valor);
                                if (atributo.Equals("Destinatario"))
                                {
                                    oDestinatario.Nome = valor;
                                }
                                else if (atributo.Equals("Endereco"))
                                {
                                    oDestinatario.Endereco = valor;
                                }
                                else if (atributo.Equals("Numero"))
                                {
                                    oDestinatario.Numero = valor;
                                }
                                else if (atributo.Equals("Bairro"))
                                {
                                    oDestinatario.Bairro = valor;

                                }
                                else if (atributo.Equals("Cidade"))
                                {
                                    oDestinatario.Cidade = valor;
                                }
                                else if (atributo.Equals("CEP"))
                                {
                                    oDestinatario.Cep = valor;
                                }

                                else if (atributo.Equals("Complemento"))
                                {
                                    oDestinatario.Complemento = valor;
                                }
                                else if (atributo.Equals("Conteudo"))
                                {
                                    XmlWsVIPP.ItemConteudo oItemConteudo = new XmlWsVIPP.ItemConteudo(valor);
                                    oItemConteudos = new XmlWsVIPP.ItemConteudo[] { oItemConteudo };
                                    XmlWsVIPP.DeclaracaoConteudo oDeclaracao = new XmlWsVIPP.DeclaracaoConteudo(oItemConteudos);
                                    oVolumeObjetos[0].DeclaracaoConteudo = oDeclaracao;
                                }
                                else if (atributo.Equals("Observacao1"))
                                {
                                    XmlWsVIPP.VolumeObjeto oVolumeObjeto = new XmlWsVIPP.VolumeObjeto();
                                    oVolumeObjeto.CodigoBarraCliente = valor;
                                    oVolumeObjetos[oVolumeObjetos.Length - 1].CodigoBarraVolume = oVolumeObjeto.CodigoBarraCliente;
                                }

                            }

                            oVolumes[oVolumes.Length - 1].VolumeObjeto = oVolumeObjetos;
                            PostagemVipp oPostagemVipp = new PostagemVipp(null, null, oDestinatario, null, null, oVolumes);
                                                                                                            

                            PostagemVipp oPostagemExistente = (from o in lVipp where o.Volumes[0].VolumeObjeto[0].CodigoBarraCliente.Equals
                                                               (oPostagemVipp.Volumes[0].VolumeObjeto[0].CodigoBarraCliente) select o).FirstOrDefault();

                            if (oPostagemExistente == null)
                            {
                                lVipp.Add(oPostagemVipp);
                            }
                            else
                            {
                                XmlWsVIPP.ItemConteudo[] x = oPostagemExistente.Volumes[0].VolumeObjeto[0].DeclaracaoConteudo.ItemConteudo;
                                Array.Resize(ref x, x.Length);
                                x[x.Length] = oPostagemVipp.Volumes[0].VolumeObjeto[0].DeclaracaoConteudo.ItemConteudo[0];
                                oPostagemExistente.Volumes[0].VolumeObjeto[0].DeclaracaoConteudo.ItemConteudo = x;
                            }
                            // oPostagem.Volumes[0].DeclaracaoConteudo.ItemConteudo[1];*/

                        }
                    }

                    WSVippPostar.PostagemVipp oSigep = new WSVippPostar.PostagemVipp();
                    //string oRetorno = oSigep.PostarObjeto(lVipp[0]).InnerXml;

                    #endregion

                }
            }
        }
    }
}