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
                switch (oFormatacao.NomeAtributo.ToUpper())
                {
                    #region Destinatario
                    case "ENDERECO":
                        oDestinatario.Endereco = oFormatacao.Valor.Trim();
                        break;
                    case "NUMERO":
                        oDestinatario.Numero = oFormatacao.Valor.Trim();
                        break;
                    case "COMPLEMENTO":
                        oDestinatario.Complemento = oFormatacao.Valor.Trim();
                        break;

                    case "BAIRRO":
                        oDestinatario.Bairro = oFormatacao.Valor.Trim();
                        break;
                    case "CIDADE":
                        oDestinatario.Cidade = oFormatacao.Valor.Trim();
                        break;
                    case "UF":
                        oDestinatario.UF = oFormatacao.Valor.Trim();
                        break;
                    case "CEP":
                        oDestinatario.Cep = oFormatacao.Valor.Trim();
                        break;
                    case "CNPJCPF":
                        oDestinatario.CnpjCpf = oFormatacao.Valor.Trim();
                        break;
                    case "NOME":
                        oDestinatario.Nome = oFormatacao.Valor.Trim();
                        break;
                    case "SEGUNDALINHADESTINATARIO":
                        oDestinatario.SegundaLinhaDestinatario = oFormatacao.Valor.Trim();
                        break;
                    case "TELEFONE":
                        oDestinatario.Telefone = oFormatacao.Valor.Trim();
                        break;
                    case "TELEFONESMS":
                        oDestinatario.Celular = oFormatacao.Valor.Trim();
                        break;
                    case "EMAIL":
                        oDestinatario.Email = oFormatacao.Valor.Trim();
                        break;
                    #endregion

                    #region Contrato
                    case "NRCONTRATO":
                        oContrato.NrContrato = oFormatacao.Valor.Trim();
                        break;
                    case "CODIGOADMINISTRATIVO":
                        oContrato.CodigoAdministrativo = oFormatacao.Valor.Trim();
                        break;
                    case "NRCARTAO":
                        oContrato.NrContrato = oFormatacao.Valor.Trim();
                        break;
                    #endregion

                    #region Servico
                    case "SERVICOECT":
                        oServico.ServicoECT = oFormatacao.Valor.Trim();
                        break;
                    #endregion

                    #region Volumes

                    case "PESO":
                        oVolume[0].Peso = oFormatacao.Valor.Trim();
                        break;
                    case "ALTURA":
                        oVolume[0].Altura = oFormatacao.Valor.Trim();
                        break;
                    case "LARGURA":
                        oVolume[0].Largura = oFormatacao.Valor.Trim();
                        break;
                    case "COMPRIMENTO":
                        oVolume[0].Comprimento = oFormatacao.Valor.Trim();
                        break;
                    case "CODIGOBARRAVOLUME":
                        oVolume[0].CodigoBarraVolume = oFormatacao.Valor.Trim();
                        break;
                    case "CODIGOBARRACLIENTE":
                        oVolume[0].CodigoBarraCliente = oFormatacao.Valor.Trim();
                        break;

                    case "VALORDECLARADO":
                        oVolume[0].ValorDeclarado = oFormatacao.Valor.Trim();
                        break;
                    case "POSICAOVOLUME":
                        oVolume[0].PosicaoVolume = oFormatacao.Valor.Trim();
                        break;
                    case "CHAVEROTEAMENTO":
                        oVolume[0].ChaveRoteamento = oFormatacao.Valor.Trim();
                        break;
                    case "OBSERVACAOVISUAL":
                        oVolume[0].ObservacaoVisual = oFormatacao.Valor.Trim();
                        break;
                    case "OBSERVACAOQUATRO":
                        oVolume[0].ObservacaoQuatro = oFormatacao.Valor.Trim();
                        break;
                    case "OBSERVACAOCINCO":
                        oVolume[0].ObservacaoCinco = oFormatacao.Valor.Trim();
                        break;
                    case "CONTEUDO":
                        oVolume[0].Conteudo = oFormatacao.Valor.Trim();
                        break;

                    #region Declaracao Conteudo
                    case "DOCUMENTOREMETENTE":
                        oDeclaracaoConteudo.DocumentoRemetente = oFormatacao.Valor.Trim();
                        break;
                    case "DOCUMENTODESTINATARIO":
                        oDeclaracaoConteudo.DocumentoDestinatario = oFormatacao.Valor.Trim();
                        break;
                    case "PESOTOTAL":
                        oDeclaracaoConteudo.PesoTotal = int.Parse(oFormatacao.Valor.Trim());
                        break;

                    #region Item Counteudo                
                    case "DESCRICAOCONTEUDO":
                        oItemConteudo.DescricaoConteudo = oFormatacao.Valor.Trim();
                        break;
                    case "QUANTIDADE":
                        oItemConteudo.Quantidade = int.Parse(oFormatacao.Valor.Trim());
                        break;
                    case "VALOR":
                        oItemConteudo.Valor = oFormatacao.Valor.Trim();
                        break;
                    #endregion
                    #endregion

                    case "ADICIONAISVOLUME":
                        oVolume[0].AdicionaisVolume = oFormatacao.Valor.Trim();
                        break;
                    case "VLRACOBRAR":
                        oVolume[0].VlrACobrar = oFormatacao.Valor.Trim();
                        break;
                    case "ETIQUETA":
                        oVolume[0].Etiqueta = oFormatacao.Valor.Trim();
                        oServico.ServicoECT = "4162"; //personalizacao
                        break;
                    #endregion

                    #region Notas Fiscais
                    case "DTNOTAFISCAL":
                        oNotaFiscal[0].DtNotaFiscal = oFormatacao.Valor.Trim();
                        break;
                    case "SERIENOTAFISCAL":
                        oNotaFiscal[0].SerieNotaFiscal = oFormatacao.Valor.Trim();
                        break;
                    case "NRNOTAFISCAL":
                        oNotaFiscal[0].NrNotaFiscal = oFormatacao.Valor.Trim();
                        break;
                    case "VLRNOTAFISCAL":
                        oNotaFiscal[0].VlrTotalNota = oFormatacao.Valor.Trim();
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
