using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Net;
using System.IO;


namespace MvcWeather.ServerUtils
{
    public class Http
    {
        // start with rain for one station, let's say:
        // lets try (43,20) for 16/1/2020 - 17/1/2020
        // get it from vscode project about weather 

        // need to get data from c# as structure, 


        //05-01-2021 
        //const ThisMonthsRainUrl = `${BaseUrl}/${stationId}/data/${channelId}/monthly`;
        static string BaseUrl = "https://api.ims.gov.il/v1/envista/stations";
        // example of interpolation in new c#: 
        //$"Length of the hypotenuse of the right triangle with legs of {a} and {b} is {CalculateHypotenuse(a, b)}");

        static string ApiToken = "f058958a-d8bd-47cc-95d7-7ecf98610e47";

        static string testUrl="https://api.ims.gov.il/v1/envista/stations";
        static string heroku = "https://cors-anywhere.herokuapp.com/";
        //static string heroku = string.Empty; // for test purposes only
        static string GetThisMonthsRainUrl(string stationId, string channelId)
        {
            string res = BaseUrl + "/" + stationId + "/data/" + channelId + "/monthly";
            return res;

        }

        //https://api.ims.gov.il/v1/envista/stations/{%ST_ID%}/data/{%CH_ID%}/latest
        public static string GetLatest(string ST_ID, string CH_ID)
        {
            string url = "https://api.ims.gov.il/v1/envista/stations/" + ST_ID + "/data/" + CH_ID + "/latest";
            return GetData(url);
        }

        public static string testWeather()
        {

            return GetData(testUrl);
        }

        public static string GetData(string myUrl)
        {
            string url = heroku + myUrl;
            //string s=$"ddd";
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //       //| SecurityProtocolType.Tls11
            //       //| SecurityProtocolType.Tls12
            //       | SecurityProtocolType.Ssl3;

            try
            {
                WebRequest request = WebRequest.Create(url);
                //'Authorization': `ApiToken ${ApiToken}`
                request.Headers.Add("Origin", "");  // note: this solved the heroku problem!!1

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