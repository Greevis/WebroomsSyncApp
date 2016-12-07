using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RhsSync
{
    public class clsWelmanError
    {
        public bool success;
        /// <summary>
        /// A technical description of the issue if the operation was unSuccessful 
        /// </summary>
        public string logFileDescription;
        /// <summary>
        /// A helpful description of the issue suitable for a user to see
        /// </summary>
        public string errorForUser;
        /// <summary>
        /// An Welman Error Number
        /// </summary>
        public int errNum;

    }


}
