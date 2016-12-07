using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisiOnlineInterface
{
    public class clsCreateKeyResponse
    {

        public clsCreateKeyResponse()
        {

        }

        public clsSessionResponse thisSessionResponse { get; set; }

        public clsKeyResponse thisKeyResponse { get; set; }

        public clsError thisError { get; set; }

        public string Request { get; set; }

        public string RawResponse { get; set; }

    }
}
