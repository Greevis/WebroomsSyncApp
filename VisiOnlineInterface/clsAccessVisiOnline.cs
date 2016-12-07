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
    public class clsAccessVisiOnline
    {

        public clsAccessVisiOnline()
        {

        }

        public const string crLf = "\r\n";


        #region GetCards

        #region GetCardsFromParameters

        public clsGetCardsResponse GetCardsFromParameters(
            string VisionlineIP,
            string EndpointId,
            string Username,
            string Password,
            string GuestDoor)
        {

            clsGetCardsResponse thisGetCardsResponse = new clsGetCardsResponse();
            thisGetCardsResponse.thisError = new VisiOnlineInterface.clsError();
            thisGetCardsResponse.thisError.ErrorMessage = "";
            thisGetCardsResponse.thisError.Success = 1;

            try
            {
                clsInitialValues thisInitialValues = new VisiOnlineInterface.clsInitialValues(
                    VisionlineIP,
                    EndpointId
                    );

                clsSessionJSON thisSessionJSON = new VisiOnlineInterface.clsSessionJSON(Username, Password);

                if (thisInitialValues.visionlineIP.Length > 0 && thisInitialValues.endpointId.Length > 0)
                {
                    thisGetCardsResponse.thisSessionResponse = EstablishSession(thisInitialValues, thisSessionJSON);
                    if (thisGetCardsResponse.thisError.Success == 1)
                    {
                        clsGetCardsJson thisGetCardsJson = new VisiOnlineInterface.clsGetCardsJson(GuestDoor);
                        thisGetCardsJson.validTime = DateTime.Now.ToString("yyyyMMddTHHmm");

                        thisGetCardsResponse = GetCards(thisInitialValues, thisGetCardsJson, thisGetCardsResponse.thisSessionResponse);
                    }

                }
                else
                {
                    thisGetCardsResponse.thisError.Success = 0;
                    thisGetCardsResponse.thisError.ErrorMessage = "Please check your initial values for visionlineIP: " + thisInitialValues.visionlineIP + " and endpointID:"
                        + thisInitialValues.endpointId;
                }
            }
            catch (WebException e)
            {
                thisGetCardsResponse.thisError.Success = 0;
                thisGetCardsResponse.thisError.ErrorMessage = displayError(e);
                return thisGetCardsResponse;
            }
            catch (Exception e)
            {
                thisGetCardsResponse.thisError.Success = 0;
                thisGetCardsResponse.thisError.ErrorMessage = e.Message;
                return thisGetCardsResponse;
            }


            return thisGetCardsResponse;
        }

        #endregion

        #region GetCards

        public clsGetCardsResponse GetCards(
            clsInitialValues thisInitialValues,
            clsGetCardsJson thisGetCardsJson,
            clsSessionResponse thisSessionResponse)
        {

            clsGetCardsResponse thisGetCardsResponse = new clsGetCardsResponse();
            thisGetCardsResponse.thisError = new clsError();
            thisGetCardsResponse.thisError.Success = 1;
            thisGetCardsResponse.thisCardResponse = new clsCardResponse();
            thisGetCardsResponse.thisSessionResponse = thisSessionResponse;

            string ApiCard = "/api/v1/cards?guestDoor=" + thisGetCardsJson.door;

            if (thisGetCardsJson.validTime != "")
                ApiCard += "&validTime=" + thisGetCardsJson.validTime;

            //string strJsonBody = JsonConvert.SerializeObject(thisGetCardsJson, Formatting.Indented);
            //string contentMd5 = MD5Hash(strJsonBody);
            UTF8Encoding encoding = new UTF8Encoding();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(thisInitialValues.visionlineIP + ApiCard);
            //Disable certificate validity check
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            // REQUEST
            request.Method = "GET";
            request.Date = DateTime.Now;

            //Sign the request
            string newLine = "\n";
            string line1 = string.Format("{0}{1}", request.Method, newLine);
            string line2 = string.Format("{0}", newLine);
            string line3 = string.Format("{0}", newLine);
            string line4 = string.Format("{0}{1}", request.Headers.Get("Date"), newLine);
            string line5 = string.Format("{0}", ApiCard);
            string toSign = line1 + line2 + line3 + line4 + line5;

            //String to be signed with access Cards received from the /sessions call
            //Console.WriteLine(toSign);

            string hashed = HmacSha1Hash(toSign, thisSessionResponse.accessKey);
            string authorisationHeader = "AWS " + thisSessionResponse.id + ":" + hashed;

            Console.WriteLine(authorisationHeader);
            request.Headers["Authorization"] = authorisationHeader;
            //thisGetCardsResponse.Request = strJsonBody + newLine + toSign;
            thisGetCardsResponse.Request = toSign;

            //try
            //{
            //    byte[] bodyData = encoding.GetBytes(strJsonBody);
            //    request.ContentLength = bodyData.Length;
            //    using (System.IO.Stream s = request.GetRequestStream())
            //    {
            //        s.Write(bodyData, 0, bodyData.Length);
            //    }
            //}
            //catch (WebException e)
            //{
            //    thisGetCardsResponse.thisError.Success = 0;
            //    thisGetCardsResponse.thisError.ErrorMessage = displayError(e);
            //    return thisGetCardsResponse;
            //}

            // RESPONSE
            HttpWebResponse response = null;
            string jsonResponse = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();

                        //thisGetCardsResponse.RawResponse = jsonResponse;
                        thisGetCardsResponse.RawResponse = @"{ ""Cards"" : " + jsonResponse + "}";

                        thisGetCardsResponse.thisCardResponse = JsonConvert.DeserializeObject<clsCardResponse>(thisGetCardsResponse.RawResponse);

                    }
                }
            }
            catch (WebException e)
            {

                thisGetCardsResponse.thisError.Success = 0;
                thisGetCardsResponse.thisError.ErrorMessage = displayError(e);
                return thisGetCardsResponse;
            }
            catch (Exception e)
            {
                thisGetCardsResponse.thisError.Success = 0;
                thisGetCardsResponse.thisError.ErrorMessage = e.Message;
                return thisGetCardsResponse;
            }

            return thisGetCardsResponse;

        }

        #endregion


        #endregion

        #region GetDoors

        #region GetDoorsFromParameters

        public clsGetDoorsResponse GetDoorsFromParameters(
            string VisionlineIP,
            string EndpointId,
            string Username,
            string Password)
        {

            clsGetDoorsResponse thisGetDoorsResponse = new clsGetDoorsResponse();
            thisGetDoorsResponse.thisError = new VisiOnlineInterface.clsError();
            thisGetDoorsResponse.thisError.ErrorMessage = "";
            thisGetDoorsResponse.thisError.Success = 1;

//            string ApiCard = "/api/v1/cards?action=mobileAccess&override=true";

            try
            {
                clsInitialValues thisInitialValues = new VisiOnlineInterface.clsInitialValues(
                    VisionlineIP,
                    EndpointId
                    );


                clsSessionJSON thisSessionJSON = new VisiOnlineInterface.clsSessionJSON(Username, Password);


                if (thisInitialValues.visionlineIP.Length > 0 && thisInitialValues.endpointId.Length > 0)
                {
                    thisGetDoorsResponse.thisSessionResponse = EstablishSession(thisInitialValues, thisSessionJSON);
                    if (thisGetDoorsResponse.thisError.Success == 1)
                    {
                        thisGetDoorsResponse = GetDoors(thisInitialValues, thisGetDoorsResponse.thisSessionResponse);
                    }

                }
                else
                {
                    thisGetDoorsResponse.thisError.Success = 0;
                    thisGetDoorsResponse.thisError.ErrorMessage = "Please check your initial values for visionlineIP: " + thisInitialValues.visionlineIP + " and endpointID:"
                        + thisInitialValues.endpointId;
                }
            }
            catch (WebException e)
            {
                thisGetDoorsResponse.thisError.Success = 0;
                thisGetDoorsResponse.thisError.ErrorMessage = displayError(e);
                return thisGetDoorsResponse;
            }
            catch (Exception e)
            {
                thisGetDoorsResponse.thisError.Success = 0;
                thisGetDoorsResponse.thisError.ErrorMessage = e.Message;
                return thisGetDoorsResponse;
            }


            return thisGetDoorsResponse;
        }

        #endregion

        #region GetDoors

        public clsGetDoorsResponse GetDoors(
            clsInitialValues thisInitialValues,
            clsSessionResponse thisSessionResponse)
        {

            clsGetDoorsResponse thisGetDoorsResponse = new clsGetDoorsResponse();
            thisGetDoorsResponse.thisError = new clsError();
            thisGetDoorsResponse.thisError.Success = 1;
            thisGetDoorsResponse.thisDoorResponse = new clsDoorResponse();
            thisGetDoorsResponse.thisSessionResponse = thisSessionResponse;


            UTF8Encoding encoding = new UTF8Encoding();

            string ApiCard = "/api/v1/doors";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(thisInitialValues.visionlineIP + ApiCard);
            //Disable certificate validity check
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            // REQUEST
            request.Method = "GET";
            request.Date = DateTime.Now;

            //Sign the request
            string newLine = "\n";
            string line1 = string.Format("{0}{1}", request.Method, newLine);
            string line2 = string.Format("{0}", newLine);
            string line3 = string.Format("{0}", newLine);
            string line4 = string.Format("{0}{1}", request.Headers.Get("Date"), newLine);
            string line5 = string.Format("{0}", ApiCard);
            string toSign = line1 + line2 + line3 + line4 + line5;

            //String to be signed with access key received from the /sessions call
            //Console.WriteLine(toSign);

            string hashed = HmacSha1Hash(toSign, thisSessionResponse.accessKey);
            string authorisationHeader = "AWS " + thisSessionResponse.id + ":" + hashed;

            Console.WriteLine(authorisationHeader);
            request.Headers["Authorization"] = authorisationHeader;

            thisGetDoorsResponse.Request = toSign;

            // RESPONSE
            HttpWebResponse response = null;
            string jsonResponse = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();

                        thisGetDoorsResponse.RawResponse = @"{ ""Doors"" : " + jsonResponse + "}";

                        thisGetDoorsResponse.thisDoorResponse = JsonConvert.DeserializeObject<clsDoorResponse>(thisGetDoorsResponse.RawResponse);

                    }
                }
            }
            catch (WebException e)
            {

                thisGetDoorsResponse.thisError.Success = 0;
                thisGetDoorsResponse.thisError.ErrorMessage = displayError(e);
                return thisGetDoorsResponse;
            }
            catch (Exception e)
            {
                thisGetDoorsResponse.thisError.Success = 0;
                thisGetDoorsResponse.thisError.ErrorMessage = e.Message;
                return thisGetDoorsResponse;
            }

            return thisGetDoorsResponse;

        }

        #endregion

        #endregion

        #region CreateMobileAccessKey

        #region CreateMobileAccessKeyFromParameters

        public clsCreateKeyResponse CreateMobileAccessKeyFromParameters(
            string VisionlineIP, 
            string EndpointId,
            string Username, 
            string Password,
            ArrayList Doors,
            DateTime ExpireTime)
        {

            clsCreateKeyResponse thisCreateKeyResponse = new clsCreateKeyResponse();
            thisCreateKeyResponse.thisError = new clsError();
            thisCreateKeyResponse.thisError.Success = 1;
            thisCreateKeyResponse.thisError.ErrorMessage = "";
            thisCreateKeyResponse.thisKeyResponse = new clsKeyResponse();


            try
            {
                clsInitialValues thisInitialValues = new VisiOnlineInterface.clsInitialValues(
                    VisionlineIP,
                    EndpointId
                    );


                clsSessionJSON thisSessionJSON = new VisiOnlineInterface.clsSessionJSON(Username, Password);


                if (thisInitialValues.visionlineIP.Length > 0 && thisInitialValues.endpointId.Length > 0)
                {

                    thisCreateKeyResponse.thisSessionResponse = EstablishSession(thisInitialValues, thisSessionJSON);

                    if (thisCreateKeyResponse.thisError.Success == 1)
                    {
                        clsKeyCreateJson thisKeyCreateJson = new VisiOnlineInterface.clsKeyCreateJson(Doors,
                            EndpointId,
                            ExpireTime);

                        thisCreateKeyResponse = CreateMobileAccessKey(thisInitialValues, thisKeyCreateJson, thisCreateKeyResponse.thisSessionResponse);
                    }

                }
                else
                {
                    thisCreateKeyResponse.thisError.Success = 0;
                    thisCreateKeyResponse.thisError.ErrorMessage = "Please check your initial values for visionlineIP: " + thisInitialValues.visionlineIP + " and endpointID:"
                        + thisInitialValues.endpointId;
                }
            }
            catch (WebException e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }
            catch (Exception e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = e.Message;
                return thisCreateKeyResponse;
            }


            return thisCreateKeyResponse;
        }

        #endregion

        #region CreateMobileAccessKey

        public clsCreateKeyResponse CreateMobileAccessKey(
            clsInitialValues thisInitialValues,
            clsKeyCreateJson thisKeyCreateJoinerJson,
            clsSessionResponse thisSessionResponse)
        {

            clsCreateKeyResponse thisCreateKeyResponse = new clsCreateKeyResponse();
            thisCreateKeyResponse.thisError = new clsError();
            thisCreateKeyResponse.thisError.Success = 1;
            thisCreateKeyResponse.thisKeyResponse = new clsKeyResponse();
            thisCreateKeyResponse.thisSessionResponse = thisSessionResponse;

            string ApiCard = "/api/v1/cards?action=mobileAccess&override=true";

            string strJsonBody = JsonConvert.SerializeObject(thisKeyCreateJoinerJson, Formatting.Indented);
            string contentMd5 = MD5Hash(strJsonBody);
            UTF8Encoding encoding = new UTF8Encoding();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(thisInitialValues.visionlineIP + ApiCard);
            //Disable certificate validity check
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            // REQUEST
            request.Method = "POST";
            request.ContentType = thisInitialValues.contentType;
            request.Date = DateTime.Now;
            request.Headers["Content-MD5"] = contentMd5;

            //Sign the request
            string newLine = "\n";
            string line1 = string.Format("{0}{1}", request.Method, newLine);
            string line2 = string.Format("{0}{1}", contentMd5, newLine);
            string line3 = string.Format("{0}{1}", thisInitialValues.contentType, newLine);
            string line4 = string.Format("{0}{1}", request.Headers.Get("Date"), newLine);
            string line5 = string.Format("{0}", ApiCard);
            string toSign = line1 + line2 + line3 + line4 + line5;

            //String to be signed with access key received from the /sessions call
            //Console.WriteLine(toSign);

            string hashed = HmacSha1Hash(toSign, thisSessionResponse.accessKey);
            string authorisationHeader = "AWS " + thisSessionResponse.id + ":" + hashed;

            Console.WriteLine(authorisationHeader);
            request.Headers["Authorization"] = authorisationHeader;
            thisCreateKeyResponse.Request = strJsonBody + newLine + toSign;

            try
            {
                byte[] bodyData = encoding.GetBytes(strJsonBody);
                request.ContentLength = bodyData.Length;
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    s.Write(bodyData, 0, bodyData.Length);
                }
            }
            catch (WebException e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }

            // RESPONSE
            HttpWebResponse response = null;
            string jsonResponse = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();

                        thisCreateKeyResponse.RawResponse = jsonResponse;
                        thisCreateKeyResponse.thisKeyResponse = JsonConvert.DeserializeObject<clsKeyResponse>(jsonResponse);

                    }
                }
            }
            catch (WebException e)
            {

                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }
            catch (Exception e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = e.Message;
                return thisCreateKeyResponse;
            }

            return thisCreateKeyResponse;

        }

        #endregion

        #endregion

        #region CreateMobileAccessKeyJoiner

        #region CreateMobileAccessKeyJoinerFromParameters

        public clsCreateKeyResponse CreateMobileAccessKeyJoinerFromParameters(
            string VisionlineIP,
            string EndpointId,
            string Username,
            string Password,
            ArrayList Doors,
            DateTime ExpireTime)
        {

            clsCreateKeyResponse thisCreateKeyResponse = new clsCreateKeyResponse();
            thisCreateKeyResponse.thisError = new clsError();
            thisCreateKeyResponse.thisError.Success = 1;
            thisCreateKeyResponse.thisError.ErrorMessage = "";
            thisCreateKeyResponse.thisKeyResponse = new clsKeyResponse();


            try
            {
                clsInitialValues thisInitialValues = new VisiOnlineInterface.clsInitialValues(
                    VisionlineIP,
                    EndpointId
                    );


                clsSessionJSON thisSessionJSON = new VisiOnlineInterface.clsSessionJSON(Username, Password);


                if (thisInitialValues.visionlineIP.Length > 0 && thisInitialValues.endpointId.Length > 0)
                {

                    thisCreateKeyResponse.thisSessionResponse = EstablishSession(thisInitialValues, thisSessionJSON);

                    if (thisCreateKeyResponse.thisError.Success == 1)
                    {
                        clsKeyCreateJson thisKeyCreateJson = new clsKeyCreateJson(Doors,
                            EndpointId,
                            ExpireTime);

                        thisCreateKeyResponse = CreateMobileAccessKeyJoiner(thisInitialValues, thisKeyCreateJson, thisCreateKeyResponse.thisSessionResponse);
                    }

                }
                else
                {
                    thisCreateKeyResponse.thisError.Success = 0;
                    thisCreateKeyResponse.thisError.ErrorMessage = "Please check your initial values for visionlineIP: " + thisInitialValues.visionlineIP + " and endpointID:"
                        + thisInitialValues.endpointId;
                }
            }
            catch (WebException e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }
            catch (Exception e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = e.Message;
                return thisCreateKeyResponse;
            }


            return thisCreateKeyResponse;
        }

        #endregion

        #region CreateMobileAccessKeyJoiner

        public clsCreateKeyResponse CreateMobileAccessKeyJoiner(
            clsInitialValues thisInitialValues,
            clsKeyCreateJson thisKeyCreateJson,
            clsSessionResponse thisSessionResponse)
        {

            clsCreateKeyResponse thisCreateKeyResponse = new clsCreateKeyResponse();
            thisCreateKeyResponse.thisError = new clsError();
            thisCreateKeyResponse.thisError.Success = 1;
            thisCreateKeyResponse.thisKeyResponse = new clsKeyResponse();
            thisCreateKeyResponse.thisSessionResponse = thisSessionResponse;

            string ApiCard = "/api/v1/cards?action=mobileAccess&autoJoin=true";

            string strJsonBody = JsonConvert.SerializeObject(thisKeyCreateJson, Formatting.Indented);
            string contentMd5 = MD5Hash(strJsonBody);
            UTF8Encoding encoding = new UTF8Encoding();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(thisInitialValues.visionlineIP + ApiCard);
            //Disable certificate validity check
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            // REQUEST
            request.Method = "POST";
            request.ContentType = thisInitialValues.contentType;
            request.Date = DateTime.Now;
            request.Headers["Content-MD5"] = contentMd5;

            //Sign the request
            string newLine = "\n";
            string line1 = string.Format("{0}{1}", request.Method, newLine);
            string line2 = string.Format("{0}{1}", contentMd5, newLine);
            string line3 = string.Format("{0}{1}", thisInitialValues.contentType, newLine);
            string line4 = string.Format("{0}{1}", request.Headers.Get("Date"), newLine);
            string line5 = string.Format("{0}", ApiCard);
            string toSign = line1 + line2 + line3 + line4 + line5;

            //String to be signed with access key received from the /sessions call
            //Console.WriteLine(toSign);

            string hashed = HmacSha1Hash(toSign, thisSessionResponse.accessKey);
            string authorisationHeader = "AWS " + thisSessionResponse.id + ":" + hashed;

            Console.WriteLine(authorisationHeader);
            request.Headers["Authorization"] = authorisationHeader;
            thisCreateKeyResponse.Request = strJsonBody + newLine + toSign;

            try
            {
                byte[] bodyData = encoding.GetBytes(strJsonBody);
                request.ContentLength = bodyData.Length;
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    s.Write(bodyData, 0, bodyData.Length);
                }
            }
            catch (WebException e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }

            // RESPONSE
            HttpWebResponse response = null;
            string jsonResponse = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();

                        thisCreateKeyResponse.RawResponse = jsonResponse;
                        thisCreateKeyResponse.thisKeyResponse = JsonConvert.DeserializeObject<clsKeyResponse>(jsonResponse);

                    }
                }
            }
            catch (WebException e)
            {

                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = displayError(e);
                return thisCreateKeyResponse;
            }
            catch (Exception e)
            {
                thisCreateKeyResponse.thisError.Success = 0;
                thisCreateKeyResponse.thisError.ErrorMessage = e.Message;
                return thisCreateKeyResponse;
            }

            return thisCreateKeyResponse;

        }

        #endregion

        #endregion

        #region EstablishSession

        public clsSessionResponse EstablishSession(
            clsInitialValues thisInitialValues,
            clsSessionJSON thisSessionJSON
            )
        {
            clsSessionResponse thisSessionResponse = new VisiOnlineInterface.clsSessionResponse();
            thisSessionResponse.thisError = new VisiOnlineInterface.clsError();
            thisSessionResponse.thisError.ErrorMessage = "";
            thisSessionResponse.thisError.Success = 1;


            string strJsonBody = JsonConvert.SerializeObject(thisSessionJSON, Formatting.None);
            string contentMd5 = MD5Hash(strJsonBody);
            UTF8Encoding encoding = new UTF8Encoding();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(thisInitialValues.visionlineIP + thisInitialValues.apiSession);
            //Disable certificate validity check
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            // REQUEST
            request.Method = "POST";
            request.ContentType = thisInitialValues.contentType;
            request.Date = DateTime.Now;
            request.Headers["Content-MD5"] = contentMd5;

            try
            {
                byte[] bodyData = encoding.GetBytes(strJsonBody);
                request.ContentLength = bodyData.Length;
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    s.Write(bodyData, 0, bodyData.Length);
                }
            }
            catch (WebException e)
            {
                thisSessionResponse.thisError.Success = 0;
                thisSessionResponse.thisError.ErrorMessage = displayError(e);
                return thisSessionResponse;
            }
            catch (Exception e)
            {
                thisSessionResponse.thisError.Success = 0;
                thisSessionResponse.thisError.ErrorMessage = e.Message;
                return thisSessionResponse;

            }

            // RESPONSE
            HttpWebResponse response = null;
            string jsonResponse = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        jsonResponse = sr.ReadToEnd();
                        //Console.WriteLine("==================================================");
                        //Console.WriteLine("SESSION REQUEST");
                        //Console.WriteLine("==================================================");

                        //Save accessKey & sessionId for later use when we are creating key
                        thisSessionResponse = JsonConvert.DeserializeObject<clsSessionResponse>(jsonResponse);
                        //Console.WriteLine(String.Format("Response: {0}", JsonConvert.SerializeObject(thisSessionResponse)));

                    }
                }
            }
            catch (WebException e)
            {
                thisSessionResponse.thisError.Success = 0;
                thisSessionResponse.thisError.ErrorMessage = displayError(e);
                return thisSessionResponse;
            }
            catch (Exception e)
            {
                thisSessionResponse.thisError.Success = 0;
                thisSessionResponse.thisError.ErrorMessage = e.Message;
                return thisSessionResponse;
            }

            return thisSessionResponse;
        }

        #endregion

        #region  Helper methods 

        public string HmacSha1Hash(string stringToSign, string accessKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(accessKey);
            var inputBytes = Encoding.UTF8.GetBytes(stringToSign);
            var hmac = new HMACSHA1(keyBytes);
            var bits = hmac.ComputeHash(inputBytes);
            var result = Convert.ToBase64String(hmac.ComputeHash(inputBytes));
            return result;
        }

        public string MD5Hash(string input)
        {
            System.Security.Cryptography.MD5 hs = System.Security.Cryptography.MD5.Create();
            byte[] bytes = Encoding.Default.GetBytes(input);
            string result = Convert.ToBase64String(hs.ComputeHash(bytes));
            return result;
        }

        public string displayError(WebException e)
        {
            string thisRetVal = "";
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                Console.WriteLine("");
                thisRetVal = "ERROR:";
                thisRetVal += "Status Code: " + ((HttpWebResponse)e.Response).StatusCode + crLf;
                thisRetVal += "Status Description: " + ((HttpWebResponse)e.Response).StatusDescription + crLf;
                using (Stream data = e.Response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    thisRetVal += text + crLf;
                }
            }
            else
            {
                thisRetVal += e.Message + crLf;
            }

            return thisRetVal;
        }
    }

    #endregion

}
