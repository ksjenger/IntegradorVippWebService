namespace IntegradorWebService
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtIdPerfil = new System.Windows.Forms.TextBox();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.lbnSenha = new System.Windows.Forms.Label();
            this.labelIdPerfil = new System.Windows.Forms.Label();
            this.labelIcon = new System.Windows.Forms.Label();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(98, 63);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(119, 20);
            this.txtUsuario.TabIndex = 0;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(98, 92);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(119, 20);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // txtIdPerfil
            // 
            this.txtIdPerfil.Location = new System.Drawing.Point(98, 120);
            this.txtIdPerfil.Name = "txtIdPerfil";
            this.txtIdPerfil.Size = new System.Drawing.Size(119, 20);
            this.txtIdPerfil.TabIndex = 2;
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(46, 66);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(46, 13);
            this.labelUsuario.TabIndex = 3;
            this.labelUsuario.Text = "Usuario:";
            // 
            // lbnSenha
            // 
            this.lbnSenha.AutoSize = true;
            this.lbnSenha.Location = new System.Drawing.Point(46, 95);
            this.lbnSenha.Name = "lbnSenha";
            this.lbnSenha.Size = new System.Drawing.Size(41, 13);
            this.lbnSenha.TabIndex = 4;
            this.lbnSenha.Text = "Senha:";
            // 
            // labelIdPerfil
            // 
            this.labelIdPerfil.AutoSize = true;
            this.labelIdPerfil.Location = new System.Drawing.Point(42, 120);
            this.labelIdPerfil.Name = "labelIdPerfil";
            this.labelIdPerfil.Size = new System.Drawing.Size(45, 13);
            this.labelIdPerfil.TabIndex = 5;
            this.labelIdPerfil.Text = "Id Perfil:";
            // 
            // labelIcon
            // 
            this.labelIcon.AutoSize = true;
            this.labelIcon.Image = global::WindowsFormsApplication1.Properties.Resources.logo_visualset;
            this.labelIcon.Location = new System.Drawing.Point(217, 21);
            this.labelIcon.Name = "labelIcon";
            this.labelIcon.Size = new System.Drawing.Size(0, 13);
            this.labelIcon.TabIndex = 6;
            // 
            // btnEntrar
            // 
            this.btnEntrar.Location = new System.Drawing.Point(45, 158);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(91, 23);
            this.btnEntrar.TabIndex = 7;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.BtnEntrar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(167, 158);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(92, 23);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.BtnSair_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Digite os dados para acessar";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 203);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.labelIcon);
            this.Controls.Add(this.labelIdPerfil);
            this.Controls.Add(this.lbnSenha);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.txtIdPerfil);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtUsuario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtIdPerfil;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label lbnSenha;
        private System.Windows.Forms.Label labelIdPerfil;
        private System.Windows.Forms.Label labelIcon;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label1;
    }
}