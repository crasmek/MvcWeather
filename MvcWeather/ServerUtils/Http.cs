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




        static string GetTommorowDate()
        {
            //var date = new Date();
            DateTime Tommorow = DateTime.Today.AddDays(1);

            //date.setDate(date.getDate() + 1)


            //var dd = String(date.getDate()).padStart(2, '0');

            var dd = Tommorow.Day.ToString();
            if (dd.Length == 1)
                dd = "0" + dd;

            //var mm = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
            var mm = Tommorow.Month.ToString();
            if (mm.Length == 1)
                mm = "0" + mm;

            //var yyyy = date.getFullYear();
            var yyyy = Tommorow.Year.ToString();
            //Tommorow

            var dateStrTommorow = yyyy + '/' + mm + '/' + dd;
            return dateStrTommorow;
        }

        // need to calculate day string
        // js versions:
        static string GetStartDate()
        {
            //var date = new Date();
            //var StartYear = date.getFullYear();
            //var mm = date.getMonth();
            //if (mm < 7)  // 7 is august
            //    StartYear = StartYear - 1;  // b/c count is from previous auguest
            //var startDate = StartYear + "/08/01";
            //return startDate;



            DateTime today = DateTime.Today;
            var StartYear = today.Year;
            var mm = today.Month;
            if (mm < 8)  // in c# , 8 is august (count starts from 1, not like JS)
                StartYear -= 1;

            var startDate = StartYear.ToString() + "/08/01";


            



            return startDate;



        }


        static string GetThisMonthsRainUrl(string stationId, string channelId)
        {
            string res = BaseUrl + "/" + stationId + "/data/" + channelId + "/monthly";
            return res;

        }
        //    const RainTillTodayUrl = `${BaseUrl}/${stationId}/data/${channelId}?from=${GetStartDate()}&to=${GetTommorowDate()}`;

        public static string GetThisMonthsRain(string stationId, string channelId)
        {
            return GetData(GetThisMonthsRainUrl(stationId,channelId));
        }


        public static string GetRainTillToday(string ST_ID, string CH_ID)
        {
            string url = BaseUrl+"/" + ST_ID + "/data/" + CH_ID + "?from=" + GetStartDate() + "&to=" + GetTommorowDate();
            return GetData(url);
        }

        public static string GetRainDaily(string ST_ID, string CH_ID)
        {
            GetStartDate();
            string url = "https://api.ims.gov.il/v1/envista/stations/" + ST_ID + "/data/" + CH_ID + "/daily";
            return GetData(url);
        }



        //https://api.ims.gov.il/v1/envista/stations/{%ST_ID%}/data/{%CH_ID%}/latest
        public static string GetLatest(string ST_ID, string CH_ID)
        {
            GetStartDate(); // for test purposes
            string url = "https://api.ims.gov.il/v1/envista/stations/" + ST_ID + "/data/" + CH_ID + "/latest";
            return GetData(url);
        }

        public static string GetDaily(string ST_ID, string CH_ID)
        {
            GetTommorowDate(); // for test purposes
            GetStartDate(); // for test purposes

            string url = "https://api.ims.gov.il/v1/envista/stations/" + ST_ID + "/data/" + CH_ID + "/daily";
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