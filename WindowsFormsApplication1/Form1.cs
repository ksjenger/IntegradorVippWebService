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


namespace IntegradorWebService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("Abrindo a planilha");
        }

        private void BtnSelecione_Click(object sender, EventArgs e)
        {

            #region Define a formatação da Planilha
            List<FormatacaoPlanilha> lFormatacao = new List<FormatacaoPlanilha>();
            FormatacaoPlanilha oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Observacao1";
            oFormatacao.Coluna = 1;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Conteudo";
            oFormatacao.Coluna = 12;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Destinatario";
            oFormatacao.Coluna = 15;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Endereco";
            oFormatacao.Coluna = 17;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Numero";
            oFormatacao.Coluna = 2;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Bairro";
            oFormatacao.Coluna = 19;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Complemento";
            oFormatacao.Coluna = 20;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "CEP";
            oFormatacao.Coluna = 21;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Cidade";
            oFormatacao.Coluna = 22;
            lFormatacao.Add(oFormatacao);

            oFormatacao = new FormatacaoPlanilha();
            oFormatacao.NomeAtributo = "Destinatario";
            oFormatacao.Coluna = 15;
            lFormatacao.Add(oFormatacao);

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
                List<Postagem> lVipp = new List<Postagem>();
                Postagem oPostagem;

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


                            foreach (FormatacaoPlanilha list in lFormatacao)
                            {
                                oPostagem = new Postagem();
                                String atributo = list.NomeAtributo;
                                int coluna = list.Coluna;
                                String valor = null;

                                //for do numero de celulas 
                                foreach (Excel.Range xlsCells in xlsCell)
                                {
                                    //xlsCells.Column retorna o index em um int;
                                    //xlsCells.Value; retorna o valor do campo em String


                                    if (xlsCells.Column.Equals(coluna))
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
                                    oPostagem.Destinatario.Nome = valor;
                                }
                                else if (atributo.Equals("Endereco"))
                                {
                                    oPostagem.Destinatario.Endereco = valor;
                                }
                                else if (atributo.Equals("Numero"))
                                {
                                    oPostagem.Destinatario.Numero = valor;
                                }
                                else if (atributo.Equals("Bairro"))
                                {
                                    oPostagem.Destinatario.Bairro = valor;

                                }
                                else if (atributo.Equals("Cidade"))
                                {
                                    oPostagem.Destinatario.Cidade = valor;
                                }
                                else if (atributo.Equals("CEP"))
                                {
                                    oPostagem.Destinatario.Cep = valor;
                                }

                                else if (atributo.Equals("Complemento"))
                                {
                                    oPostagem.Destinatario.Complemento = valor;
                                }
                                else if (atributo.Equals("Conteudo"))
                                {
                                    oPostagem.Volumes[1].Conteudo = valor;
                                }
                                else if (atributo.Equals("Observacao1"))
                                {
                                    oPostagem.Volumes[1].ObservacaoVisual = valor;
                                }

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

                            }
                        }

                        WSVippPostar.PostagemVipp oSigep = new WSVippPostar.PostagemVipp();
                        string oRetorno = oSigep.PostarObjeto(lVipp[0]).InnerXml;

                        #endregion

                    }
                }

            }
        }
    }
}