using IntegradorWebService.WSVIPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace IntegradorWebService.VIPP
{
    class PostarObjeto
    {

        public List<string> lRetorno = new List<string>();

        #region Recebe a lista, Faz a chamada no Web Service. O metodo faz outra chamada para guardar o retorno em 2 listas
        public static void Postagem(List<Postagem> lVipp)
        {
            string oRetorno = null;

            foreach (Postagem o in lVipp)
            {
                Postagem oPostagem = new Postagem
                {
                    Destinatario = o.Destinatario,
                    ContratoEct = o.ContratoEct,
                    NotasFiscais = o.NotasFiscais,
                    PerfilVipp = new PerfilVipp(),
                    Servico = o.Servico,
                    Volumes = o.Volumes,
                };
                oPostagem.PerfilVipp.Usuario = o.PerfilVipp.Usuario;
                oPostagem.PerfilVipp.Token = o.PerfilVipp.Token;
                oPostagem.PerfilVipp.IdPerfil = o.PerfilVipp.IdPerfil;
                PostagemVipp oSigep = new PostagemVipp();
                try
                {
                    oRetorno = oSigep.PostarObjeto(oPostagem).InnerXml;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erro: " + e.Message + " verifique a conexao com a Internet");
                }
                Retorno.RetornoPostagem(oRetorno);
                #endregion

            }
        }
    }
}