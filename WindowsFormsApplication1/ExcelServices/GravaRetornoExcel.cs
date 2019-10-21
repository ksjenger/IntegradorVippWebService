using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using IntegradorWebService.VIPP;
using System.Windows.Forms;
using System.Windows;
using System;

namespace IntegradorWebService.ExcelServices
{
    class GravaRetornoExcel
    {
        public static void GravaRetorno()
        {
            #region Salva a lista de retorno no Excel
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook xlsWorkbook = xlsApp.Workbooks.Open(Form1.path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            Excel.Worksheet newWorksheetErro;
            Excel.Worksheet newWorksheetOk;

            try
            {
                //Add a worksheet to the workbook.
                newWorksheetErro = xlsApp.Worksheets.Add();
                newWorksheetOk = xlsApp.Worksheets.Add();


                newWorksheetErro.Name = "WebServiceVipp - Erros";

                //Name the sheet.
                newWorksheetOk.Name = "WebServiceVipp - ok";

                //Get the Cells collection.
                Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;

                //For que acessa todas as planilhas
                foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
                {
                    #region Grava p retorno na planilha - Ok
                    //Acessa a aba da Planilha com o nome "WebServiceVipp"
                    if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - ok"))
                    {

                        Excel.Range xlsWorksRows = xlsWorksheet.Cells;
                        int cont = 1;

                        xlsWorksRows.Item[cont, 1] = "Observação 1";
                        xlsWorksRows.Item[cont, 2] = "Destinatario";
                        xlsWorksRows.Item[cont, 3] = "Status da Postagem";
                        xlsWorksRows.Item[cont, 4] = "Codigo de Rastreio";
                        xlsWorksRows.Item[cont, 5] = "Conteudo";



                        foreach (RetornoValida list in Retorno.lRetornoValida)
                        {
                            cont++;
                            xlsWorksRows.Item[cont, 1] = list.Observacao;
                            xlsWorksRows.Item[cont, 2] = list.Nome;
                            xlsWorksRows.Item[cont, 3] = list.Status;
                            xlsWorksRows.Item[cont, 4] = list.Etiqueta;
                            xlsWorksRows.Item[cont, 5] = list.Observacao5;
                            if (int.Parse(list.Observacao5) > 5)
                            {
                                xlsWorksRows.Item[cont, 5].Interior.ColorIndex = 6;
                            }
                        }
                    }

                    #endregion

                    #region Grava p retorno na planilha - Erro

                    if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - Erros"))
                    {
                        Excel.Range xlsWorksRowss = xlsWorksheet.Cells;

                        int cont = 1;
                        xlsWorksRowss.Item[cont, 1] = "Observação 1";
                        xlsWorksRowss.Item[cont, 2] = "Destinatario";
                        xlsWorksRowss.Item[cont, 3] = "Status da Postagem";
                        xlsWorksRowss.Item[cont, 4] = "Quantidade de Erros";

                        foreach (RetornoInvalida list in Retorno.lRetornoInvalida)
                        {
                            cont++;
                            if (!list.Observacao.Equals(string.Empty) || !list.Observacao.Equals(null))
                            {
                                xlsWorksRowss.Item[cont, 1] = list.Observacao;
                                xlsWorksRowss.Item[cont, 2] = list.Nome;
                                xlsWorksRowss.Item[cont, 3] = list.Status;
                                xlsWorksRowss.Item[cont, 4] = list.Erro;
                            }
                        }
                    }
                    #endregion

                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                System.Windows.MessageBox.Show("Não foi possivel gravar o retorno no arquivo processado, verifique se a planilha está bloqueada", "Erro", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            DateTime saveNow = DateTime.Now;
            var sdf = saveNow.ToString("dd-MM-yyyy_hh.mm");


            while(Properties.Settings.Default.SaveFile == "")
            {
                    System.Windows.MessageBox.Show("Selecione o local para Salvar o arquivo", "Salvar arquivo", MessageBoxButton.OK, MessageBoxImage.Information);
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.SelectedPath = Properties.Settings.Default.SaveFile;
                        folderBrowserDialog.Description = "Selecione onde salvar o arquivo processado";
                        folderBrowserDialog.ShowDialog();
                        Properties.Settings.Default.SaveFile = folderBrowserDialog.SelectedPath;
                        Properties.Settings.Default.Save();
                    }                
            }

            var nomeArquivo =  Properties.Settings.Default.SaveFile + "\\" + Form1.nomeArquivo + " " + sdf + ".xlsx";
            xlsApp.ActiveWorkbook.SaveAs(nomeArquivo);
            xlsApp.Quit();
            #endregion
        }

    }
}


