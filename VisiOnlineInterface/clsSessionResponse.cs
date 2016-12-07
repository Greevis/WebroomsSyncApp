using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisiOnlineInterface
{
    public class clsSessionResponse
    {
        // JSON Response object for session query
        public clsSessionResponse()
        {
        }
        public string accessKey { get; set; }
        public string id { get; set; }

        public clsError thisError { get; set; }
    }

}