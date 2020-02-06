using Visualset.IntegradorWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IntegradorWebService.VIPP
{
    public class TrataRetorno
    {
        #region Atributos
        public static List<RetornoValida> lRetornoValida = new List<RetornoValida>();
        public static List<RetornoInvalida> lRetornoInvalida = new List<RetornoInvalida>();
        string statusPostagem = null;
        string nomeDestinatario = null;
        string observacao = null;
        string observacao5 = null;
        string etiqueta = null;
        string erros = null;
        string mensagem = null;
        string tipoErro = null;
        string mensagemErro = null;
        int cont = 1;
        #endregion

        #region Retorno Postagem
        public void RetornoPostagem(string xmlString)
        {
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
                        try
                        {
                            observacao = nodeVolumeObjeto.SelectSingleNode("CodigoBarraVolume").InnerText;
                        }
                        catch (NullReferenceException)
                        {
                            observacao = "Não Informado";
                        }

                        try
                        {
                            observacao5 = nodeVolumeObjeto.SelectSingleNode("ObservacaoCinco").InnerText;
                        }
                        catch (NullReferenceException)
                        {

                        }

                        try
                        {
                            etiqueta = nodeVolumeObjeto.SelectSingleNode("Etiqueta").InnerText;
                        }
                        catch (NullReferenceException)
                        {
                            etiqueta = "Erro";
                        }
                    }
                }

                foreach (XmlNode nodeListaErros in listaErros)
                {
                    XmlNodeList erro = nodeListaErros.SelectNodes("Erro");
                    cont = 1;
                    erros = "";
                    mensagem = "";
                    tipoErro = "";

                    foreach (XmlNode nodeErros in erro)
                    {
                        tipoErro = nodeErros.SelectSingleNode("TipoErro").InnerText;

                        if (tipoErro.Equals("Excecao"))
                        {
                            mensagem = nodeErros.SelectSingleNode("Mensagem").InnerText;
                            mensagemErro = mensagemErro + "| Erro " + cont + " - " + mensagem + " " + erros;
                            cont++;

                        }
                        else if (tipoErro.Equals("Validacao"))
                        {
                            mensagem = nodeErros.SelectSingleNode("Atributo").InnerText;
                            erros = nodeErros.SelectSingleNode("Descricao").InnerText;
                            mensagemErro = mensagemErro + "| Erro " + cont + " - " + mensagem + " " + erros;
                            cont++;
                        }
                    }
                }
            }

            #region Retorna postagem Valida
            if (statusPostagem.Equals("Valida"))
            {
                mensagem = "Postagem " + statusPostagem + " Etiqueta: " + etiqueta + " - " + nomeDestinatario;
                RetornoValida oRetornoValida = new RetornoValida()
                {
                    Status = statusPostagem,
                    Etiqueta = etiqueta,
                    Nome = nomeDestinatario,
                    Observacao = observacao,
                    Observacao5 = observacao5

                };
                lRetornoValida.Add(oRetornoValida);
            }
            #endregion

            #region Retorna postagem Invalida
            else
            {
                mensagem = "Postagem " + statusPostagem + " Motivo: ";
                RetornoInvalida oRetornoInvalida = new RetornoInvalida()
                {
                    Nome = nomeDestinatario,
                    Observacao = observacao,
                    Status = statusPostagem,
                    Erro = mensagemErro,
                    Observacao5 = observacao5
                };
                lRetornoInvalida.Add(oRetornoInvalida);
            }
            #endregion
        }
        #endregion

    }
}
