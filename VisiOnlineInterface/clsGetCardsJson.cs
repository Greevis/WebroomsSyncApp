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
    public class clsGetCardsJson
    {

        public clsGetCardsJson(string Door
            )
        {
            door = Door;
        }

        public string door { get; set; }

        public string validTime { get; set; }

    }
}