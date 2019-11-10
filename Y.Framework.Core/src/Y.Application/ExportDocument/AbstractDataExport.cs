using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Y.Services
{
    public abstract class AbstractDataExport<T> : IAbstractDataExport
    {
        protected string _sheetName;
        protected string _fileName;
        protected List<T> _headers;
        protected List<T> _type;
        protected IWorkbook _workbook;
        protected ISheet _sheet;
        private const string DefaultSheetName = "Sheet1";

        public IWorkbook Export<T>
              (List<T> exportData, string sheetName = DefaultSheetName)
        {
            _sheetName = sheetName;

            _workbook = new XSSFWorkbook(); //Creating New Excel object
            _sheet = _workbook.CreateSheet(_sheetName); //Creating New Excel Sheet object

            var headerStyle = _workbook.CreateCellStyle(); //Formatting
            var headerFont = _workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);

            WriteData(exportData); //your list object to NPOI excel conversion happens here

            //Header
            var header = _sheet.CreateRow(0);
            for (var i = 0; i < _headers.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(_headers[i].ToString());
                cell.CellStyle = headerStyle;
            }

            for (var i = 0; i < _headers.Count; i++)
            {
                _sheet.AutoSizeColumn(i);
            }

            return _workbook;
        }

        //Generic Definition to handle all types of List
        public abstract void WriteData<T>(List<T> exportData);
    }
}
