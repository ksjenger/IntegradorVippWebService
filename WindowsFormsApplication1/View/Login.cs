using System;
using System.Windows.Forms;
using Visualset.IntegradorWebService.Entities;
using Visualset.IntegradorWebService.Facade;

namespace IntegradorWebService
{
    public partial class Login : Form
    {
        #region Atributos
        internal static PerfilImportacao Operfil { get; set; }
        #endregion

        #region Construtores
        public Login()
        {
            InitializeComponent();
            this.Text = "Login - Versão: " + Application.ProductVersion;

        }
        #endregion

        #region Metodos Privados

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {

            Operfil = new PerfilImportacao()
            {
                Usuario = txtUsr.Text,
                Token = txtPwd.Text
            };

            if (txtUsr.Text.Length < 6)
            {
                MessageBox.Show("O usuário deve conter no minimo 6 caracteres", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPwd.Text.Length < 6)
            {
                MessageBox.Show("A senha deve conter no minimo 6 caracteres", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (new VIPPRestFacade().Logar(txtUsr.Text, txtPwd.Text))
            {
                Hide();
                new Form1(txtUsr.Text, txtPwd.Text).ShowDialog();
                Application.Exit();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
