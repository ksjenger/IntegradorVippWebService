using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using IntegradorWebService.VIPP;

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

            //Add a worksheet to the workbook.
            newWorksheetErro = xlsApp.Worksheets.Add();
            newWorksheetOk = xlsApp.Worksheets.Add();

            //Name the sheet.
            newWorksheetErro.Name = "WebServiceVipp - Erros";

            //Name the sheet.
            newWorksheetOk.Name = "WebServiceVipp - ok";

            //Get the Cells collection.
            Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;

            //For que acessa todas as planilhas
            foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
            {
                //Acessa a aba da Planilha com o nome "WebServiceVipp"
                if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - ok"))
                {
                    Excel.Range xlsWorksRows = xlsWorksheet.Cells;
                    int cont = 0;
                    foreach (RetornoValida list in Retorno.lRetornoValida)
                    {
                        cont++;
                        xlsWorksRows.Item[cont, 1] = list.Observacao;
                        xlsWorksRows.Item[cont, 2] = list.Nome;
                        xlsWorksRows.Item[cont, 3] = list.Status;
                        xlsWorksRows.Item[cont, 4] = list.Etiqueta;
                    }
                }

                if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - Erros"))
                {
                    Excel.Range xlsWorksRowss = xlsWorksheet.Cells;

                    int cont = 0;
                    foreach (RetornoInvalida list in Retorno.lRetornoInvalida)
                    {
                        cont++;
                        if (list.Observacao.Equals(string.Empty))
                        {
                            break;
                        }
                        else
                        {
                            xlsWorksRowss.Item[cont, 1] = list.Observacao;
                            xlsWorksRowss.Item[cont, 2] = list.Nome;
                            xlsWorksRowss.Item[cont, 3] = list.Status;
                            xlsWorksRowss.Item[cont, 4] = list.Erro;
                        }
                    }
                }
            }
            xlsApp.ActiveWorkbook.SaveAs(Form1.caminho);
            xlsApp.Quit();
            #endregion
        }

    }
}

