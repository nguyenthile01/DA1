using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Users.Dto
{
    public class ApiExecuteResult
    {
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int StatusCode { get; set; }
    }
}
