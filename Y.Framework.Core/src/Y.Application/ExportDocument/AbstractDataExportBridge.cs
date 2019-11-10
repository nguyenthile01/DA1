using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
//using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Y.Services
{
    public class AbstractDataExportBridge : AbstractDataExport<string>
    {
        public AbstractDataExportBridge()
        {
            _headers = new List<string>();
            _type = new List<string>();
        }

        public override void WriteData<T>(List<T> exportData)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _type.Add(type.Name);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                  prop.PropertyType);
                //string name = Regex.Replace(prop.Text, "([A-Z])", " $1").Trim(); //space separated 
                var name = prop.DisplayName ?? Regex.Replace(prop.Name, "([A-Z])", " $1").Trim(); //space separated ;
                _headers.Add(name);
            }

            foreach (T item in exportData)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var sheetRow = _sheet.CreateRow(i + 1);
                for (var j = 0; j < table.Columns.Count; j++)
                {
                    var row1 = sheetRow.CreateCell(j);

                    string type = _type[j].ToLower();
                    var currentCellValue = table.Rows[i][j];

                    if (currentCellValue != null &&
                        !string.IsNullOrEmpty(Convert.ToString(currentCellValue)))
                    {
                        switch (type)
                        {
                            case "string":
                                row1.SetCellValue(Convert.ToString(currentCellValue));
                                break;
                            case "boolean":
                                row1.SetCellValue(Convert.ToString(currentCellValue));
                                break;
                            case "int32":
                                row1.SetCellValue(Convert.ToInt32(currentCellValue));
                                break;
                            case "double":
                            case "decimal":
                                row1.SetCellValue(Convert.ToDouble(currentCellValue));
                                break;
                            case "datetime":
                                row1.SetCellValue(((DateTime)currentCellValue).ToString("MM/dd/yyyy"));
                                break;
                        }
                    }
                    else
                    {
                        row1.SetCellValue(string.Empty);
                    }
                }
            }
        }
    }
}
