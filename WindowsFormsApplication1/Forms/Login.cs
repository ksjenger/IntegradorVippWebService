using IntegradorWebService.VIPP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace IntegradorWebService
{
    public partial class Login : Form
    {
        
        internal static PerfilImportacao Operfil { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            Operfil = new PerfilImportacao()
            {
                Usuario = txtUsuario.Text,
                Token = txtSenha.Text,
                IdPerfil = txtIdPerfil.Text
            };

            Form1 frm = new Form1();
            frm.Show();
            Hide();
            
        }
    }
}
