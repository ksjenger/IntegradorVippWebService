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
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook xlsWorkbook = xlsApp.Workbooks.Open(Form1.path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            Excel.Worksheet newWorksheet;
            Excel.Worksheet newWorksheets;
            //Add a worksheet to the workbook.
            newWorksheet = xlsApp.Worksheets.Add();
            newWorksheets = xlsApp.Worksheets.Add();

            //Name the sheet.
            newWorksheet.Name = "WebServiceVipp - Erros";

            //Name the sheet.
            newWorksheets.Name = "WebServiceVipp - ok";


            //Get the Cells collection.
            Excel.Sheets xlsSheets = xlsWorkbook.Worksheets;

            //For que acessa todas as planilhas
            foreach (Excel.Worksheet xlsWorksheet in xlsSheets)
            {
                //Acessa a aba da Planilha com o nome "WebServiceVipp"
                if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - Erros"))
                {
                    Excel.Range xlsWorksRows = xlsWorksheet.Rows;
                    Excel.Range xlsCell = xlsWorksheet.Cells;

                    //for do Numero de linhas
                    foreach (Excel.Range xlsWorkCell in xlsWorksRows)
                    {
                        foreach (RetornoInvalida lRet in Retorno.lRetornoInvalida)
                        {
                            xlsCell.Item[1] = lRet.Nome;
                            xlsCell.Item[2] = lRet.Observacao;
                            xlsCell.Item[3] = lRet.Status;
                            xlsCell.Item[4] = lRet.Erro;
                        }
                        if (xlsWorkCell.Row > Retorno.lRetornoInvalida.Count)
                        {
                            break;
                        }
                    }
                }


                //Acessa a aba da Planilha com o nome "WebServiceVipp"
                if (xlsWorksheet.Name.Trim().Equals("WebServiceVipp - ok"))
                {
                    Excel.Range xlsWorksRows = xlsWorksheet.Rows;
                    Excel.Range xlsCell = xlsWorksheet.Cells;

                    //for do Numero de linhas
                    foreach (Excel.Range xlsWorkCell in xlsWorksRows)
                    {

                        foreach (RetornoValida lRet in Retorno.lRetornoValida)
                        {
                            xlsCell.Item[1] = lRet.Nome;
                            xlsCell.Item[2] = lRet.Observacao;
                            xlsCell.Item[3] = lRet.Status;
                            xlsCell.Item[4] = lRet.Etiqueta;
                        }
                        if(xlsWorkCell.Row > Retorno.lRetornoValida.Count)
                        {
                            break;
                        }
                    }
                }


            }
            xlsApp.ActiveWorkbook.SaveAs(Form1.caminho);
            xlsApp.Quit();
        }
    }
}

