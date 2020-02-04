using IntegradorWebService.ExcelServices;
using IntegradorWebService.Rest;
using IntegradorWebService.Services;
using IntegradorWebService.WSVIPP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using IntegradorWebService.Visualset.IntegradorWebService.Entities;

namespace IntegradorWebService
{
    public partial class Form1 : Form
    {

        List<Postagem> lVipp = new List<Postagem>();
        readonly Rootobject lPerfil = new Rootobject();

        public static string path;
        public static string nomeArquivo;
        public static string caminhoArquivo;
        public static string tipoArquivo;

        public Form1(string usuario, string senha)
        {
            InitializeComponent();
            this.Text = "Importador Visual Personalizado - Versão: " + Application.ProductVersion + "  -  " + usuario;
            btnEnviar.Enabled = false;
            lPerfil = RestPerfilImportacao.ProcessaListaPerfil(usuario, senha);
            comboPerfil.Items.Add("Selecione o Perfil");
            comboPerfil.SelectedIndex = 0;
            for (int i = 0; i < lPerfil.Data.Length; i++)
            {
                comboPerfil.Items.Add(lPerfil.Data[i].IdPerfil + " - " + lPerfil.Data[i].NomePerfil);
            }
            progressBar.Visible = false;

        }


        private void Button2_Click(object sender, EventArgs e)
        {

            int id = comboPerfil.SelectedIndex;

            if (id == 0)
            {
                MessageBox.Show("Selecione o perfil de importação", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                Login.Operfil.IdPerfil = lPerfil.Data[id - 1].IdPerfil;

                #region Chama o metodo para Postar Objeto
                VIPP.PostarObjetoVIPP.Postagem(lVipp, this);

                #endregion

                labelProgresso.Text = "Salvando o arquivo processado...";
                GravaRetornoExcel.GravaRetorno();
                MessageBox.Show("Importação finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                path = null;
                labelPath.Text = "";
                labelProgresso.Text = "";
                btnEnviar.Enabled = false;
                progressBar.Value = 0;
                progressBar.Visible = false;
            }
        }

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
                    tipoArquivo = "txt";
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.visualset.com.br");
            System.Diagnostics.Process.Start("http://vipp.visualset.com.br");
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = Properties.Settings.Default.SaveFile;
                folderBrowserDialog.Description = "Selecione onde salvar o arquivo processado";
                folderBrowserDialog.ShowDialog();
                Properties.Settings.Default.SaveFile = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
