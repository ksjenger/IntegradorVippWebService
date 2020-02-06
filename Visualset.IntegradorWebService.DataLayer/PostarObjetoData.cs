<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using Visualset.IntegradorWebService.DataLayer.ServiceReference;
using System.Windows.Forms;

namespace Visualset.IntegradorWebService.DataLayer
{
    public class PostarObjetoData
    {
        #region Recebe a lista, Faz a chamada no Web Service. O metodo faz outra chamada para guardar o retorno em 2 listas

        public string Postagem(List<Postagem> lVipp)
        {
            string oRetorno = string.Empty;

            //int cont = 0;
            //frm.progressBar.Maximum = lVipp.Count + 1;
            //frm.progressBar.Visible = true;
            //frm.progressBar.Value = 1;
            //frm.labelProgresso.Text = "Transmitindo para o VIPP";
=======
﻿using IntegradorWebService.WSVIPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using IntegradorWebService.VIPP;

namespace IntegradorWebService.VIPP
{
    class PostarObjetoVIPP
    {
        #region Recebe a lista, Faz a chamada no Web Service. O metodo faz outra chamada para guardar o retorno em 2 listas

        public List<string> lRetorno = new List<string>();             

        public static void Postagem(List<Postagem> lVipp, Form1 frm)
        {
            string oRetorno = null;
            int cont = 0;
            frm.progressBar.Maximum = lVipp.Count + 1;
            frm.progressBar.Visible = true;
            frm.progressBar.Value = 1;
            frm.labelProgresso.Text = "Transmitindo para o VIPP";
>>>>>>> 9fa98c482e24165f3582d7135ebcb72f2fd7fb1b

            foreach (Postagem o in lVipp)
            {
                try
                {
<<<<<<< HEAD
                    //cont++;
                    //frm.progressBar.Value++;
=======
                    cont++;                    
                    frm.progressBar.Value++; 
>>>>>>> 9fa98c482e24165f3582d7135ebcb72f2fd7fb1b

                    Postagem oPostagem = new Postagem
                    {
                        Destinatario = o.Destinatario,
                        ContratoEct = o.ContratoEct,
                        NotasFiscais = o.NotasFiscais,
<<<<<<< HEAD
                        PerfilVipp = o.PerfilVipp,
                        Servico = o.Servico,
                        Volumes = o.Volumes
                    };

                    using (PostagemVippSoapClient oSigep = new PostagemVippSoapClient())
=======
                        PerfilVipp = new PerfilVipp()
                        {
                            Usuario = Login.Operfil.Usuario,
                            IdPerfil = Login.Operfil.IdPerfil,
                            Token = Login.Operfil.Token
                        }
                        ,
                        Servico = o.Servico,
                        Volumes = o.Volumes
                    };
                    
                    using (PostagemVipp oSigep = new PostagemVipp())
>>>>>>> 9fa98c482e24165f3582d7135ebcb72f2fd7fb1b
                    {
                        try
                        {
                            oRetorno = oSigep.PostarObjeto(oPostagem).InnerXml;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Erro: " + e.Message + " verifique a conexao com a Internet");
                        }
<<<<<<< HEAD

                        //TrataRetorno.RetornoPostagem(oRetorno);                        
=======
                        TrataRetorno.RetornoPostagem(oRetorno);                        
>>>>>>> 9fa98c482e24165f3582d7135ebcb72f2fd7fb1b
                    }
                }
                catch (NullReferenceException)
                {
<<<<<<< HEAD
                    
                }
                

            }
            return oRetorno;
=======

                }

            }
>>>>>>> 9fa98c482e24165f3582d7135ebcb72f2fd7fb1b
        }
        #endregion

    }
}