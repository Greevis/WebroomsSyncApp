using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace VisiOnlineInterface
{

    public class clsInitialValues
    {
        //public clsInitialValues()
        //{

        //    // Values you need to provide based on your environment

        //    visionlineIP = "https://127.0.0.1"; //Example : https://127.0.0.1
        //    endpointId = "guestFolder123"; //Example endpointId
        //    keyExpireTime = DateTime.Now.AddDays(1).ToString("yyyyMMddTHHmm"); //Default 1 day (24h) valid key, modify if required


        //    // Static values, should not be modified
        //    contentType = "application/json; charset=utf-8";
        //    apiSession = "/api/v1/sessions";
        //    apiCard = "/api/v1/cards?action=mobileAccess&override=true";
        //}

        public clsInitialValues(string VisionlineIP,
            string EndpointId
            //string ApiCard
            )
        {
            visionlineIP = VisionlineIP;
            endpointId = EndpointId;
            //keyExpireTime = DateTime.Now.AddDays(1).ToString("yyyyMMddTHHmm");
            contentType = "application/json; charset=utf-8";
            apiSession = "/api/v1/sessions";
            //apiCard = ApiCard;
//            apiCard = "/api/v1/cards?action=mobileAccess&override=true";
        }

        //public clsInitialValues(string VisionlineIP, 
        //    string EndpointId
        //    //DateTime ExpireTime,
        //    //string ApiCard
        //    )
        //{
        //    visionlineIP = VisionlineIP;
        //    endpointId = EndpointId;

        //    //keyExpireTime = ExpireTime.ToString("yyyyMMddTHHmm"); //Default 1 day (24h) valid key, modify if required
        //    // Static values, should not be modified
        //    contentType = "application/json; charset=utf-8";
        //    apiSession = "/api/v1/sessions";
        //    //apiCard = ApiCard;
        //    //apiCard = "/api/v1/cards?action=mobileAccess&override=true";
        //}

        public string visionlineIP { get; set; }
        public string apiSession { get; set; }
        //public string apiCard { get; set; }
        public string contentType { get; set; }
        //public string keyExpireTime { get; set; }
        public string endpointId { get; set; }
    }


}
