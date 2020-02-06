using System;
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

            foreach (Postagem o in lVipp)
            {
                try
                {
                    //cont++;
                    //frm.progressBar.Value++;

                    Postagem oPostagem = new Postagem
                    {
                        Destinatario = o.Destinatario,
                        ContratoEct = o.ContratoEct,
                        NotasFiscais = o.NotasFiscais,
                        PerfilVipp = o.PerfilVipp,
                        Servico = o.Servico,
                        Volumes = o.Volumes
                    };

                    using (PostagemVippSoapClient oSigep = new PostagemVippSoapClient())
                    {
                        try
                        {
                            oRetorno = oSigep.PostarObjeto(oPostagem).InnerXml;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Erro: " + e.Message + " verifique a conexao com a Internet");
                        }

                        //TrataRetorno.RetornoPostagem(oRetorno);                        
                    }
                }
                catch (NullReferenceException)
                {
                    
                }
                

            }
            return oRetorno;
        }
        #endregion

    }
}