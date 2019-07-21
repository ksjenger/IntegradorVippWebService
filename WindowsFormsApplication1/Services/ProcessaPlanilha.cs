using IntegradorWebService;
using IntegradorWebService.WSVippPostar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace IntegradorWebService.Services
{
    class ProcessaPlanilha
    {
        #region Processa Planilha
        public static List<PostagemVipp> ListaDePostagem(String path)
        {
            #region Recupera a formatação da planilha do Settings.settings
            List<FormatacaoPlanilha> lFormatacao = new List<FormatacaoPlanilha>();
            lFormatacao = FormatacaoPlanilha.ListarFormatacao();
            #endregion

            List<PostagemVipp> lVipp = new List<PostagemVipp>();
            Excel.Application xlsAPP = new Excel.Application();
            Excel.Workbook xlsWorkbook = xlsAPP.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;

            //For que acessa todas as planilhas
            foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
            {
                //Acessa a aba da Planilha com o nome "Control Respuesta".
                if (xlsWorksheet.Name.Trim().Equals("Control Respuesta"))
                {
                    Excel.Range xlsWorksRows = xlsWorksheet.Rows;

                    //for do Numero de linhas
                    foreach (Excel.Range xlsWorkCell in xlsWorksRows)
                    {
                        Destinatario oDestinatario = new Destinatario();
                        VolumeObjeto[] oVolumeObjetos = new VolumeObjeto[] { new VolumeObjeto() };
                        ItemConteudo[] oItemConteudos;
                        DeclaracaoConteudo[] oDeclaracaoConteudos = new DeclaracaoConteudo[] { new DeclaracaoConteudo() };
                        Excel.Range xlsCell = xlsWorkCell.Cells;

                        //For para percorrer a lista de Formatação

                        foreach (FormatacaoPlanilha list in lFormatacao)
                        {
                            String atributo = list.NomeAtributo;
                            int coluna = list.Coluna;
                            Excel.Range AtributoValor = xlsCell.Item[coluna];
                            string valor = AtributoValor.Text;

                            if (atributo.Equals("Destinatario"))
                            {
                                oDestinatario.Nome = valor;
                            }
                            else if (atributo.Equals("Endereco"))
                            {
                                oDestinatario.Endereco = valor;
                            }
                            else if (atributo.Equals("Numero"))
                            {
                                oDestinatario.Numero = valor;
                            }
                            else if (atributo.Equals("Bairro"))
                            {
                                oDestinatario.Bairro = valor;
                            }
                            else if (atributo.Equals("Cidade"))
                            {
                                oDestinatario.Cidade = valor;
                            }
                            else if (atributo.Equals("CEP"))
                            {
                                oDestinatario.Cep = valor;
                            }
                            else if (atributo.Equals("Complemento"))
                            {
                                oDestinatario.Complemento = valor;
                            }
                            else if (atributo.Equals("Conteudo"))
                            {
                                ItemConteudo oItemConteudo = new ItemConteudo(valor);
                                oItemConteudos = new ItemConteudo[] { oItemConteudo };
                                oVolumeObjetos[0].DeclaracaoConteudo = new DeclaracaoConteudo(oItemConteudos);
                            }
                            else if (atributo.Equals("Observacao1"))
                            {
                                VolumeObjeto oVolumeObjeto = new VolumeObjeto();
                                oVolumeObjeto.CodigoBarraCliente = valor;
                                oVolumeObjetos[0].CodigoBarraCliente = oVolumeObjeto.CodigoBarraCliente;
                            }
                        }//fim do For da Lista de Formatacao

                        PerfilVipp oPerfilVipp = new PerfilVipp("webservice", "testewebservice", "606");

                        PostagemVipp oPostagemVipp = new PostagemVipp(null, null, oDestinatario, null, null, oVolumeObjetos);


                        PostagemVipp oPostagemExistente = (from o in lVipp
                                                           where o.Volumes[0].CodigoBarraCliente.Equals(
                                           (oPostagemVipp.Volumes[0].CodigoBarraCliente))
                                                           select o).FirstOrDefault();
                        if (oPostagemExistente == null)
                        {
                            lVipp.Add(oPostagemVipp);
                        }
                        else
                        {
                            
                            ItemConteudo[] x = oPostagemVipp.Volumes[0].DeclaracaoConteudo.ItemConteudo;
                            Array.Resize(ref x, x.Length + 1);
                            x[x.Length - 1] = oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo[0];
                            oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo = x;
                            lVipp.Add(oPostagemExistente);
                        }

                        if (oPostagemVipp.Destinatario.Nome.Equals(string.Empty))
                        {
                            break;
                        }


                    }// fim do for que acessa as linhas

                }// fim do if q acessa a aba da Planilha com o nome "Control Respuesta".

                //WSVippPostar.PostagemVipp oSigep = new WSVippPostar.PostagemVipp();
            }// fim do For que acessa todas as planilhas
            return lVipp;
        }
        #endregion

    }
}
