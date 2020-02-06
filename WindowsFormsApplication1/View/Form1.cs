using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visualset.IntegradorWebService.Entities;
using Visualset.IntegradorWebService.Facade;
using Visualset.IntegradorWebService.DataLayer.ServiceReference;
using Microsoft.Office.Interop.Excel;
using IntegradorWebService.VIPP;
using System.IO;
using System.Text;

namespace Visualset.IntegradorWebService.View
{
    public partial class Form1 : Form
    {
        #region Atributos
        List<Postagem> lVipp = null;
        readonly Rootobject lPerfil = null;
        PerfilImportacao oPerfil = null;

        public static string path;
        public static string nomeArquivo;
        public static string caminhoArquivo;
        public static string tipoArquivo;
        #endregion

        #region Construtores        
        public Form1(string usuario, string senha)
        {
            InitializeComponent();
            this.Text = "Importador Visual Personalizado - Versão: " + ProductVersion + "  -  " + usuario;
            btnEnviar.Enabled = false;
            lPerfil = new VIPPRestFacade().ProcessaListaPerfil(usuario, senha);
            comboPerfil.Items.Add("Selecione o Perfil");
            comboPerfil.SelectedIndex = 0;
            for (int i = 0; i < lPerfil.Data.Length; i++)
            {
                comboPerfil.Items.Add(lPerfil.Data[i].IdPerfil + " - " + lPerfil.Data[i].NomePerfil);
            }
            progressBar.Visible = false;
            oPerfil = new PerfilImportacao()
            {
                Usuario = usuario,
                Token = senha
            };

        }
        #endregion

        #region Botao Enviar, envia uma lista ao VIPP


        private void Button2_Click(object sender, EventArgs e)
        {

            int id = comboPerfil.SelectedIndex;

            if (id == 0)
            {
                System.Windows.Forms.MessageBox.Show("Selecione o perfil de importação", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                oPerfil.IdPerfil = lPerfil.Data[id - 1].IdPerfil;

                #region Chama o metodo para Postar Objeto
                string oRetorno = new PostarObjetoFacade().Postagem(lVipp, this);

                #endregion

                labelProgresso.Text = "Salvando o arquivo processado...";



                switch (tipoArquivo)
                {
                    case "txt":
                        GravaRetornoTxt();
                        break;
                    case "CENGAGE - EXCEL":
                        GravaRetornoExcel();
                        break;
                }

                MessageBox.Show("Importação finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                path = null;
                labelPath.Text = "";
                labelProgresso.Text = "";
                btnEnviar.Enabled = false;
                progressBar.Value = 0;
                progressBar.Visible = false;
            }
        }
        #endregion

        #region Grava Retorno txt
        public void GravaRetornoTxt()
        {
            DateTime saveNow = DateTime.Now;
            var sdf = saveNow.ToString("dd-MM-yyyy_hh.mm");


            while (Settings.Default.SaveFile == "")
            {
                MessageBox.Show("Selecione o local para Salvar o arquivo", "Salvar arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.SelectedPath = Settings.Default.SaveFile;
                    folderBrowserDialog.Description = "Selecione onde salvar o arquivo processado";
                    folderBrowserDialog.ShowDialog();
                    Settings.Default.SaveFile = folderBrowserDialog.SelectedPath;
                    Settings.Default.Save();

                }
                string FileName = Settings.Default.SaveFile + "\\" + Form1.nomeArquivo + " " + sdf + ".txt";

                using (StreamWriter file = new System.IO.StreamWriter(FileName, false, new UTF8Encoding(true)))
                {
                    int cont = 0;
                    foreach (RetornoInvalida oRetorno in TrataRetorno.lRetornoInvalida)
                    {
                        string retorno = string.Join(" | ", oRetorno.Nome.Trim(), oRetorno.Erro.Trim(), oRetorno.Observacao.Trim(), oRetorno.Status.Trim());
                        file.WriteLine(retorno);
                    }

                    foreach (RetornoValida oRetorno in TrataRetorno.lRetornoValida)
                    {
                        string retorno = string.Join(" | ", oRetorno.Nome.Trim(), oRetorno.Etiqueta.Trim(), oRetorno.Observacao.Trim(), oRetorno.Status.Trim());
                        file.WriteLine(retorno);
                    }
                    file.Close();
                }
            }
        }

        #endregion

        #region Grava Retorno Excel
        public void GravaRetornoExcel()
        {
            #region Salva a lista de retorno no Excel
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlsWorkbook = xlsApp.Workbooks.Open(Form1.path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            Worksheet newWorksheetErro;
            Worksheet newWorksheetOk;

            try
            {
                //Add a worksheet to the workbook.
                newWorksheetErro = xlsApp.Worksheets.Add();
                newWorksheetOk = xlsApp.Worksheets.Add();


                newWorksheetErro.Name = "WebServiceVipp - Erros";

                //Name the sheet.
                newWorksheetOk.Name = "WebServiceVipp - ok";

                //Get the Cells collection.
                Sheets xlsSheets = xlsWorkbook.Worksheets;

                //For que acessa todas as planilhas
                foreach (Worksheet xlsWorksheet in xlsSheets)
                {
                    #region Grava p retorno na planilha - Ok
                    //Acessa a aba da Planilha com o nome "WebServiceVipp"
                    if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - ok"))
                    {

                        Range xlsWorksRows = xlsWorksheet.Cells;
                        int cont = 1;

                        xlsWorksRows.Item[cont, 1] = "Observação 1";
                        xlsWorksRows.Item[cont, 2] = "Destinatario";
                        xlsWorksRows.Item[cont, 3] = "Status da Postagem";
                        xlsWorksRows.Item[cont, 4] = "Codigo de Rastreio";
                        xlsWorksRows.Item[cont, 5] = "Conteudo";



                        foreach (RetornoValida list in TrataRetorno.lRetornoValida)
                        {
                            cont++;
                            xlsWorksRows.Item[cont, 1] = list.Observacao;
                            xlsWorksRows.Item[cont, 2] = list.Nome;
                            xlsWorksRows.Item[cont, 3] = list.Status;
                            xlsWorksRows.Item[cont, 4] = list.Etiqueta;
                            xlsWorksRows.Item[cont, 5] = list.Observacao5;
                            if (int.Parse(list.Observacao5) > 5)
                            {
                                xlsWorksRows.Item[cont, 5].Interior.ColorIndex = 6;
                            }
                        }
                    }

                    #endregion

                    #region Grava p retorno na planilha - Erro

                    if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - Erros"))
                    {
                        Range xlsWorksRowss = xlsWorksheet.Cells;

                        int cont = 1;
                        xlsWorksRowss.Item[cont, 1] = "Observação 1";
                        xlsWorksRowss.Item[cont, 2] = "Destinatario";
                        xlsWorksRowss.Item[cont, 3] = "Status da Postagem";
                        xlsWorksRowss.Item[cont, 4] = "Quantidade de Erros";

                        foreach (RetornoInvalida list in TrataRetorno.lRetornoInvalida)
                        {
                            cont++;
                            if (!list.Observacao.Equals(string.Empty) || !list.Observacao.Equals(null))
                            {
                                xlsWorksRowss.Item[cont, 1] = list.Observacao;
                                xlsWorksRowss.Item[cont, 2] = list.Nome;
                                xlsWorksRowss.Item[cont, 3] = list.Status;
                                xlsWorksRowss.Item[cont, 4] = list.Erro;
                            }
                        }
                    }
                    #endregion

                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Não foi possivel gravar o retorno no arquivo processado, verifique se a planilha está bloqueada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            DateTime saveNow = DateTime.Now;
            var sdf = saveNow.ToString("dd-MM-yyyy_hh.mm");


            while (Settings.Default.SaveFile == "")
            {
                MessageBox.Show("Selecione o local para Salvar o arquivo", "Salvar arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.SelectedPath = Settings.Default.SaveFile;
                    folderBrowserDialog.Description = "Selecione onde salvar o arquivo processado";
                    folderBrowserDialog.ShowDialog();
                    Settings.Default.SaveFile = folderBrowserDialog.SelectedPath;
                    Settings.Default.Save();
                }
            }

            var nomeArquivo = Settings.Default.SaveFile + "\\" + Form1.nomeArquivo + " " + sdf + ".xlsx";
            xlsApp.ActiveWorkbook.SaveAs(nomeArquivo);
            xlsApp.Quit();
            #endregion
        }
        #endregion

        #region Combo Perfil        
        private void ComboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (path == null)
            {
                btnSelecione.Focus();
            }
            else
            {
                btnEnviar.Focus();
            }
        }
        #endregion  

        #region Abre o Arquivo
        private void Button1_Click_1(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    lVipp = null;
                    tipoArquivo = "csv";
                    path = openFileDialog.FileName;
                    nomeArquivo = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    caminhoArquivo = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                    labelPath.Text = path;
                    labelProgresso.Text = "Importando o Arquivo";
                    #region Processa o Arquivo
                    lVipp = ProcessaPlanilha.ListaDePostagem(path, this, tipoArquivo);
                    #endregion

                    if (lVipp == null)
                    {
                        labelProgresso.Text = "";
                        labelPath.Text = "";
                        path = null;
                    }
                    else
                    {
                        labelProgresso.Text = "Arquivo importado!";
                        btnEnviar.Enabled = true;
                        comboPerfil.Focus();
                    }

                }
            }

        }
        #endregion

        #region Logo Visual
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.visualset.com.br");
            System.Diagnostics.Process.Start("http://vipp.visualset.com.br");
        }
        #endregion

        #region Botao Salvar
        private void Button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = Settings.Default.SaveFile;
                folderBrowserDialog.Description = "Selecione onde salvar o arquivo processado";
                folderBrowserDialog.ShowDialog();
                Settings.Default.SaveFile = folderBrowserDialog.SelectedPath;                
                Settings.Default.Save();
            }
        }
        #endregion
    }
}
