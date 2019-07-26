using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IntegradorWebService.VIPP
{
    class Retorno
    {
        public string Nome { get; set; }

        public string Observacao { get; set; }

        public string Status { get; set; }

        public static List<Retorno> lRetorno = new List<Retorno>();

        #region Recebe o XML e retorna uma String com o Status da Solicitacao, Destinatario e Numero do Objeto
        public static string RetornoPostagem(string xmlString)
        {
            string statusPostagem = null;
            string nomeDestinatario = null;
            string observacao = null;
            string etiqueta = null;
            string erros = null;
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

                        erros = erros + " - Erro: " + cont + " " + nodeErros.SelectSingleNode("Descricao").InnerText;
                        cont++;
                    }
                }
            }

            if (statusPostagem.Equals("Valida"))
            {
                mensagem = "Postagem " + statusPostagem + " Etiqueta: " + etiqueta + " - " + nomeDestinatario;
                RetornoValida oRetornoValida = new RetornoValida()
                {
                    Status = statusPostagem,
                    Etiqueta = etiqueta,
                    Nome = nomeDestinatario,
                    Observacao = observacao
                };
                lRetorno.Add(oRetornoValida);
            }
            else
            {
                mensagem = "Postagem " + statusPostagem + " Motivo: ";
                RetornoInvalida oRetornoInvalida = new RetornoInvalida()
                {
                    Nome = nomeDestinatario,
                    Observacao = observacao,
                    Status = statusPostagem,
                    Erro = erros
                };
                lRetorno.Add(oRetornoInvalida);
            }
            return mensagem;
        }
        #endregion

    }
}
