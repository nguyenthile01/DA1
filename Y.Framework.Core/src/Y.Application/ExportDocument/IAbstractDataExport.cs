using System.Collections.Generic;
using System.Net.Http;
using NPOI.SS.UserModel;

namespace Y.Services
{
    public interface IAbstractDataExport
    {
        IWorkbook Export<T>
            (List<T> exportData, string sheetName );
    }
}
