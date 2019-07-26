using IntegradorWebService.WSVIPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IntegradorWebService.VIPP
{
    class PostarObjeto
    {

        public List<string> lRetorno = new List<string>();

        #region Recebe a lista, Faz a chamada no Web Service. O metodo retorna um String com o Xml Response
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
                oRetorno = oSigep.PostarObjeto(oPostagem).InnerXml;
                Retorno.RetornoPostagem(oRetorno);
            }
        }
        #endregion

        
    }

}
