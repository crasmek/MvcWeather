﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Net;
using System.IO;


namespace MvcWeather.ServerUtils
{
    public class Http
    {
        //05-01-2021 
        //const ThisMonthsRainUrl = `${BaseUrl}/${stationId}/data/${channelId}/monthly`;
        static string BaseUrl = "https://api.ims.gov.il/v1/envista/stations";
        //$"Length of the hypotenuse of the right triangle with legs of {a} and {b} is {CalculateHypotenuse(a, b)}");

        static string ApiToken = "f058958a-d8bd-47cc-95d7-7ecf98610e47";

        static string testUrl="https://api.ims.gov.il/v1/envista/stations";

        static string GetThisMonthsRainUrl(string stationId,string channelId)
        {
            string res = BaseUrl + "/" + stationId + "/data/" + channelId + "/monthly";
            return res;
        }

        public static string testWeather()
        {

            return GetData(testUrl);
        }

        public static string GetData(string url)
        {
            //string s=$"ddd";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   //| SecurityProtocolType.Tls11
                   //| SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;

            try
            {
                WebRequest request = WebRequest.Create(url);
                //'Authorization': `ApiToken ${ApiToken}`
                request.Headers.Add("Authorization", "ApiToken "+ ApiToken);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                StreamReader sr = new StreamReader(data);
                string html = sr.ReadToEnd();

                return html; 


            }
            catch (System.Exception ex)
            {
                var ext = ex.Message;
                return ext;
            }
        }

    }
}