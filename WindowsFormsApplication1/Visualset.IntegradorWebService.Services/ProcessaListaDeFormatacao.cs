using IntegradorWebService.Visualset.IntegradorWebService.Entities;
using IntegradorWebService.WSVIPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegradorWebService.Visualset.IntegradorWebService.Services
{
    public class ProcessaListaDeFormatacao
    {

        public static Postagem Processa(List<FormatacaoPlanilha> lFormatacao)
        {            
            Destinatario oDestinatario = new Destinatario();
            ContratoEct oContrato = new ContratoEct();
            Servico oServico = new Servico();
            VolumeObjeto[] oVolume = new VolumeObjeto[] { new VolumeObjeto() };
            ItemConteudo oItemConteudo = new ItemConteudo();
            DeclaracaoConteudo oDeclaracaoConteudo = new DeclaracaoConteudo();
            NotaFiscal[] oNotaFiscal = new NotaFiscal[] { new NotaFiscal() };


            foreach (FormatacaoPlanilha oFormatacao in lFormatacao)
            {
                switch (oFormatacao.NomeAtributo)
                {
                    #region Destinatario
                    case "Endereco":
                        oDestinatario.Endereco = oFormatacao.Valor;
                        break;
                    case "Numero":
                        oDestinatario.Numero = oFormatacao.Valor;
                        break;
                    case "Complemento":
                        oDestinatario.Complemento = oFormatacao.Valor;
                        break;

                    case "Bairro":
                        oDestinatario.Bairro = oFormatacao.Valor;
                        break;
                    case "Cidade":
                        oDestinatario.Cidade = oFormatacao.Valor;
                        break;
                    case "UF":
                        oDestinatario.UF = oFormatacao.Valor;
                        break;
                    case "Cep":
                        oDestinatario.Cep = oFormatacao.Valor;
                        break;
                    case "CnpjCpf":
                        oDestinatario.CnpjCpf = oFormatacao.Valor;
                        break;
                    case "Nome":
                        oDestinatario.Nome = oFormatacao.Valor;
                        break;
                    case "SegundaLinhaDestinatario":
                        oDestinatario.SegundaLinhaDestinatario = oFormatacao.Valor;
                        break;
                    case "Telefone":
                        oDestinatario.Telefone = oFormatacao.Valor;
                        break;
                    case "TelefoneSMS":
                        oDestinatario.Celular = oFormatacao.Valor;
                        break;
                    case "Email":
                        oDestinatario.Email = oFormatacao.Valor;
                        break;
                    #endregion

                    #region Contrato
                    case "NrContrato":
                        oContrato.NrContrato = oFormatacao.Valor;
                        break;
                    case "CodigoAdministrativo":
                        oContrato.CodigoAdministrativo = oFormatacao.Valor;
                        break;
                    case "NrCartao":
                        oContrato.NrContrato = oFormatacao.Valor;
                        break;
                    #endregion

                    #region Servico
                    case "ServicoECT":
                        //oServico.ServicoECT = oFormatacao.Valor;
                        oServico.ServicoECT = "4162";
                        break;
                    #endregion

                    #region Volumes

                    case "Peso":
                        oVolume[0].Peso = oFormatacao.Valor;
                        break;
                    case "Altura":
                        oVolume[0].Altura = oFormatacao.Valor;
                        break;
                    case "Largura":
                        oVolume[0].Largura = oFormatacao.Valor;
                        break;
                    case "Comprimento":
                        oVolume[0].Comprimento = oFormatacao.Valor;
                        break;
                    case "CodigoBarraVolume":
                        oVolume[0].CodigoBarraVolume = oFormatacao.Valor;
                        break;
                    case "CodigoBarraCliente":
                        oVolume[0].CodigoBarraCliente = oFormatacao.Valor;
                        break;

                    case "ValorDeclarado":
                        oVolume[0].ValorDeclarado = oFormatacao.Valor;
                        break;
                    case "PosicaoVolume":
                        oVolume[0].PosicaoVolume = oFormatacao.Valor;
                        break;
                    case "ChaveRoteamento":
                        oVolume[0].ChaveRoteamento = oFormatacao.Valor;
                        break;
                    case "ObservacaoVisual":
                        oVolume[0].ObservacaoVisual = oFormatacao.Valor;
                        break;
                    case "ObservacaoQuatro":
                        oVolume[0].ObservacaoQuatro = oFormatacao.Valor;
                        break;
                    case "ObservacaoCinco":
                        oVolume[0].ObservacaoCinco = oFormatacao.Valor;
                        break;
                    case "Conteudo":
                        oVolume[0].Conteudo = oFormatacao.Valor;
                        break;

                    #region Declaracao Conteudo
                    case "DocumentoRemetente":
                        oDeclaracaoConteudo.DocumentoRemetente = oFormatacao.Valor;
                        break;
                    case "DocumentoDestinatario":
                        oDeclaracaoConteudo.DocumentoDestinatario = oFormatacao.Valor;
                        break;
                    case "PesoTotal":
                        oDeclaracaoConteudo.PesoTotal = int.Parse(oFormatacao.Valor);
                        break;

                    #region Item Counteudo                
                    case "DescricaoConteudo":
                        oItemConteudo.DescricaoConteudo = oFormatacao.Valor;
                        break;
                    case "Quantidade":
                        oItemConteudo.Quantidade = int.Parse(oFormatacao.Valor);
                        break;
                    case "Valor":
                        oItemConteudo.Valor = oFormatacao.Valor;
                        break;
                    #endregion
                    #endregion

                    case "AdicionaisVolume":
                        oVolume[0].AdicionaisVolume = oFormatacao.Valor;
                        break;
                    case "VlrACobrar":
                        oVolume[0].VlrACobrar = oFormatacao.Valor;
                        break;
                    case "Etiqueta":
                        oVolume[0].Etiqueta = oFormatacao.Valor;
                        break;
                        #endregion
                }
             
            }

            return new Postagem()
            {
                Destinatario = oDestinatario,
                ContratoEct = oContrato,
                NotasFiscais = oNotaFiscal,
                Servico = oServico,
                Volumes = oVolume
            };            
        }



    }
}
