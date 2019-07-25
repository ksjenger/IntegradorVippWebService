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
                    PerfilVipp = new PerfilVipp()
                };
                oPostagem.PerfilVipp.Usuario = o.PerfilVipp.Usuario;
                oPostagem.PerfilVipp.Token = o.PerfilVipp.Token;
                oPostagem.PerfilVipp.IdPerfil = o.PerfilVipp.IdPerfil;
                oPostagem.Servico = o.Servico;
                oPostagem.Volumes = o.Volumes;
                PostagemVipp oSigep = new PostagemVipp();
                oRetorno = oSigep.PostarObjeto(oPostagem).InnerXml;
                RetornoPostagem(oRetorno);
            }

        }
        #endregion

        public static string RetornoPostagem(string xmlString)
        {
            string statusPostagem = null;
            string nomeDestinatario = null;
            string observacao = null;
            string etiqueta = null;
            string lista = null;
            string mensagem = null;
            int cont = 1;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Retorno>" + xmlString + "</Retorno>");
            XmlNodeList retornoXmlChild = doc.ChildNodes;

            foreach (XmlNode node in retornoXmlChild)
            {
                statusPostagem = node.SelectSingleNode("StatusPostagem").InnerText;
                XmlNodeList destinatario = node.SelectNodes("Destinatario");
                XmlNodeList volumes = node.SelectNodes("Volumes");
                XmlNodeList listaErros = node.SelectNodes("ListaErros");

                foreach (XmlNode nodeDestinatario in destinatario)
                {
                    nomeDestinatario = nodeDestinatario.SelectSingleNode("Nome").InnerText;
                }

                foreach (XmlNode nodeVolume in volumes)
                {

                    XmlNodeList VolumeObjetos = nodeVolume.SelectNodes("VolumeObjeto");



                    foreach (XmlNode nodeVolumeObjeto in VolumeObjetos)
                    {
                        observacao = nodeVolumeObjeto.SelectSingleNode("CodigoBarraVolume").InnerText;
                        etiqueta = nodeVolumeObjeto.SelectSingleNode("Etiqueta").InnerText;
                    }

                }

                foreach (XmlNode nodeListaErros in listaErros)
                {
                    XmlNodeList erro = nodeListaErros.SelectNodes("Erro");

                    foreach (XmlNode nodeErros in nodeListaErros)
                    {
                        
                        lista = lista + " - Erro: " + cont + " " + nodeErros.SelectSingleNode("Descricao").InnerText;
                        cont++;
                    }
                }
            }

            if (statusPostagem.Equals("Valida"))
            {
                mensagem = "Postagem " + statusPostagem + " Etiqueta: " + etiqueta + " - " + nomeDestinatario;
            }
            else
            {
                mensagem = "Postagem " + statusPostagem + " Motivo: ";
            }

            return mensagem;

        }

    }



}
