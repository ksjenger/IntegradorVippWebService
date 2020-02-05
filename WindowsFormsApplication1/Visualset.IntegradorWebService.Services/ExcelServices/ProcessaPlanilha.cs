using IntegradorWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegradorWebService.WSVIPP;
using Excel = Microsoft.Office.Interop.Excel;
using ItemConteudo = IntegradorWebService.WSVIPP.ItemConteudo;
using System.Windows;
using IntegradorWebService.Visualset.IntegradorWebService.Entities;
using IntegradorWebService.Visualset.IntegradorWebService.Services.ExcelServices;
using IntegradorWebService.Visualset.IntegradorWebService.Services;

namespace IntegradorWebService.Services
{
    class ProcessaPlanilha
    {

        #region Processa Planilha e retorna uma lista do tipo Postagem
        public static List<Postagem> ListaDePostagem(string path, Form1 frm, string tipoArquivo)
        {
            #region Atributos
            List<Postagem> lVipp = new List<Postagem>();
            #endregion

            #region Recupera a formatação da planilha do Settings.settings
            List<FormatacaoPlanilha> lFormatacao = CarregaFormatacao.ListarFormatacao();
            #endregion

            List<string> lista = new List<string>();
            switch (tipoArquivo.ToUpper())
            {
                #region Processa Txt
                case "TXT":
                    lista = new List<string>();
                    string line;
                    // Read the file and display it line by line.  
                    System.IO.StreamReader file = new System.IO.StreamReader(path);
                    while ((line = file.ReadLine()) != null)
                    {
                        string linhaSelecionada = (from s in line where line.Substring(0, 3).ToUpper().Equals("CLI") select line).FirstOrDefault();
                        if (linhaSelecionada != null)
                        {
                            lista.Add(linhaSelecionada);
                        }
                    }

                    foreach (string l in lista)
                    {

                        foreach (FormatacaoPlanilha oFormatacao in lFormatacao)
                        {
                            //oFormatacao.Valor = (from s in l select l.Substring(oFormatacao.Inicio - 1, l.IndexOf(";"))).ToString();                        
                            oFormatacao.Valor = (from s in l select l.Substring(oFormatacao.Inicio, oFormatacao.Tamanho)).ToString();
                        }
                        lVipp.Add(ProcessaListaDeFormatacao.Processa(lFormatacao));
                    }

                    file.Close();
                    System.Console.ReadLine();

                    break;
                #endregion

                #region Processa CSV
                case "CSV":
                    lista = new List<string>();
                    // Read the file and display it line by line.  
                    System.IO.StreamReader filecsv = new System.IO.StreamReader(path);
                    while ((line = filecsv.ReadLine()) != null)
                    {
                        string linhaSelecionada = (from s in line where line.Substring(0, 3).ToUpper().Equals("CLI") select line).FirstOrDefault();
                        if (linhaSelecionada != null)
                        {
                            lista.Add(linhaSelecionada);
                        }
                    }

                    foreach (string l in lista)
                    {
                        string[] linha = l.Split(';');
                        foreach (FormatacaoPlanilha oFormatacao in lFormatacao)
                        {
                            oFormatacao.Valor = (from s in linha where !linha[oFormatacao.Coluna - 1].Equals(null) select linha[oFormatacao.Coluna-1].ToString().Trim()).FirstOrDefault();
                        }
                        lVipp.Add(ProcessaListaDeFormatacao.Processa(lFormatacao));
                    }

                    filecsv.Close();
                    System.Console.ReadLine();
                    break;

                #endregion

                #region Processa Excel xls Cengage
                case "XLS":
                    Excel.Application xlsAPP = new Excel.Application();
                    int cont = 0;

                    try
                    {

                        Excel.Workbook xlsWorkbook = xlsAPP.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
                        Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;


                        //For que acessa todas as planilhas
                        foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
                        {

                            //Verifica se já existe a aba "WebServiceVipp - ok", a mesma é criada apos a importação do arquivo.
                            if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - ok") || xlsWorksheet.Name.Trim().Equals("WebServiceVipp - Erros"))
                            {
                                MessageBox.Show("O arquivo já foi importado anteriormente, selecione um novo arquivo.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                                return null;
                            }


                            //Acessa a aba da Planilha com o nome "Control Respuesta".
                            else if (xlsWorksheet.Name.Trim().Equals("Control Respuesta"))
                            {
                                Excel.Range xlsWorksRows = xlsWorksheet.Rows;
                                int isbnCount = 0;

                                //for do Numero de linhas
                                foreach (Excel.Range xlsWorkCell in xlsWorksRows)
                                {
                                    Destinatario oDestinatario = new Destinatario();
                                    Servico oServico = new Servico();
                                    WSVIPP.VolumeObjeto[] oVolumeObjetos = new WSVIPP.VolumeObjeto[] { new WSVIPP.VolumeObjeto() };
                                    ItemConteudo[] oItemConteudos;
                                    DeclaracaoConteudo[] oDeclaracaoConteudos = new DeclaracaoConteudo[] { new DeclaracaoConteudo() };
                                    Excel.Range xlsCell = xlsWorkCell.Cells;
                                    int quantidade = 0;


                                    #region Lista de Formatação
                                    //For para percorrer a lista de Formatação

                                    foreach (FormatacaoPlanilha list in lFormatacao)
                                    {
                                        String atributo = list.NomeAtributo;
                                        int coluna = list.Coluna;
                                        Excel.Range AtributoValor = xlsCell.Item[coluna];
                                        string valor = AtributoValor.Text;

                                        if (atributo.Equals("UF"))
                                        {
                                            oDestinatario.UF = valor;
                                        }

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

                                            ItemConteudo oItemConteudo = new ItemConteudo()
                                            {
                                                DescricaoConteudo = valor,
                                                Quantidade = 1,
                                                Valor = "0,00"

                                            };

                                            oItemConteudos = new ItemConteudo[] { oItemConteudo };
                                            oVolumeObjetos[0].DeclaracaoConteudo = new DeclaracaoConteudo()
                                            {
                                                ItemConteudo = oItemConteudos,
                                                PesoTotal = 10
                                            };


                                        }
                                        else if (atributo.Equals("Observacao1"))
                                        {
                                            WSVIPP.VolumeObjeto oVolumeObjeto = new WSVIPP.VolumeObjeto
                                            {
                                                CodigoBarraVolume = valor,
                                                ObservacaoCinco = "" + 1

                                            };
                                            oVolumeObjetos[0].CodigoBarraVolume = oVolumeObjeto.CodigoBarraVolume;
                                            oVolumeObjetos[0].ObservacaoCinco = oVolumeObjeto.ObservacaoCinco;

                                        }
                                        else if (atributo.Equals("Observacao2"))
                                        {
                                            WSVIPP.VolumeObjeto oVolumeObjeto = new WSVIPP.VolumeObjeto
                                            {
                                                CodigoBarraCliente = valor

                                            };
                                            oVolumeObjetos[0].CodigoBarraCliente = oVolumeObjeto.CodigoBarraCliente;

                                        }
                                        else if (atributo.Equals("Quantidade"))
                                        {
                                            try
                                            {
                                                quantidade = int.Parse(valor);
                                            }
                                            catch (System.FormatException)
                                            {
                                                quantidade = 0;
                                            }
                                        }

                                        else if (atributo.Equals("DocumentoDestinatario"))
                                        {
                                            oVolumeObjetos[0].DeclaracaoConteudo.DocumentoDestinatario = valor;
                                        }

                                        else if (atributo.Equals("Servico"))
                                        {
                                            valor = valor.ToUpper();

                                            if (valor.Equals("PAC"))
                                            {
                                                oServico.ServicoECT = "4669";
                                            }
                                            else if (valor.Equals("SEDEX"))
                                            {
                                                oServico.ServicoECT = "4162";
                                            }
                                            else
                                            {
                                                oServico.ServicoECT = valor;
                                            }
                                        }

                                    }//fim do For da Lista de Formatacao

                                    #endregion

                                    #region Instancia o objeto Postagem

                                    Postagem oPostagem = new Postagem()
                                    {
                                        Destinatario = oDestinatario,
                                        Volumes = oVolumeObjetos,
                                        Servico = oServico
                                    };

                                    Postagem oPostagemExistente = (from o in lVipp
                                                                   where o.Volumes[0].CodigoBarraVolume.Equals(
                                                                    oPostagem.Volumes[0].CodigoBarraVolume)
                                                                   select o).FirstOrDefault();
                                    if (oPostagemExistente == null)
                                    {
                                        isbnCount = 1;
                                        lVipp.Add(oPostagem);
                                        cont++;
                                        frm.labelProgresso.Text = "Processando o item " + cont + " da lista";

                                    }
                                    else
                                    {
                                        ItemConteudo[] x = oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo;
                                        Array.Resize(ref x, x.Length + 1);
                                        x[x.Length - 1] = oPostagem.Volumes[0].DeclaracaoConteudo.ItemConteudo[0];
                                        oPostagemExistente.Volumes[0].DeclaracaoConteudo.ItemConteudo = x;
                                        isbnCount++;
                                        oPostagemExistente.Volumes[0].ObservacaoCinco = "" + isbnCount;
                                    }

                                    if (oPostagem.Destinatario.Nome.Equals(string.Empty))
                                    {
                                        break;
                                    }
                                    #endregion


                                }// fim do for que acessa as linhas

                            }// fim do if q acessa a aba da Planilha com o nome "Control Respuesta".

                        }// fim do For que acessa todas as planilhas
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ocorreu um erro com o arquivo, o mesmo é invalido ou está mal formatado", "Erro", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    finally
                    {
                        GC.Collect();

                    }

                    xlsAPP.Quit();
                    break;
                    #endregion
            }

            #endregion

            return lVipp;

        }
    }
}