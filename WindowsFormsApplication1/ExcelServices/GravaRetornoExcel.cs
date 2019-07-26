using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace IntegradorWebService.ExcelServices
{
    class GravaRetornoExcel
    {
        public static void GravaRetorno()
        {
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook xlsWorkbook = xlsApp.Workbooks.Open(Form1.path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "", false, false, 0, false, false, false);
            Excel.Worksheet newWorksheet;
            //Add a worksheet to the workbook.
            //newWorksheet = xlsApp.Worksheets.Add();
            xlsApp.Worksheets.Add();
            //Name the sheet.
            //newWorksheet.Name = "New_Sheet";

            //Get the Cells collection.
            //Excel.Range cells = newWorksheet.Cells;


            xlsApp.ActiveWorkbook.SaveAs(Form1.path);
            xlsApp.Quit();
        }

    }
}
