using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace VisiOnlineInterface
{
    public class clsKeyCreateJson
    {
        public clsKeyCreateJson(ArrayList Doors,
            string EndPointId,
            DateTime ExpireTime
            )
        {
            var doorOp = new clsKeyCreateJsonDoorOperations();
            doorOp.doors = new List<string> { };

            for (int counter = 0; counter < Doors.Count; counter++)
                doorOp.doors.Add((string)Doors[counter]);

            doorOp.operation = "guest";

            doorOperations = new List<clsKeyCreateJsonDoorOperations> { doorOp };

            format = "rfid48";
            label = "%ROOMRANGE%:%UUID%:%CARDNUM%";
            description = "%SITENAME%:%USERID%";
            endPointID = EndPointId;
            expireTime = ExpireTime.ToString("yyyyMMddTHHmm");
        }

        public string expireTime { get; set; }
        public string format { get; set; }
        public string endPointID { get; set; }
        public string label { get; set; }
        public string description { get; set; }

        public IList<clsKeyCreateJsonDoorOperations> doorOperations { get; set; }

    }
}