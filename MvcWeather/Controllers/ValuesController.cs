using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//using System.Web
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;

using System.Web.Script.Serialization;

//using System.Web.sc
//

using MvcWeather.ServerUtils;

namespace MvcWeather.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }




        //[Route("api/Discount/GetData/{CategoryCode}/{Gender}")]
       
        //public string Get(int StationID, int ChannelID)
        //{
        //    return "to be developed...";
        //}

        
        // GET api/values/5
        public string Get(string id)  // 59_1 is beer sheva       
        //public string Get(int id)
        {
            string str = id;
            string jsonData;
            if (str.Length >= 3)
            {
                string StationID = (str.Split('_'))[0];
                string ChannelID = (str.Split('_'))[1];

                jsonData = ServerUtils.Http.GetRainTillToday(StationID, ChannelID);
                //jsonData = ServerUtils.Http.GetThisMonthsRain(StationID, ChannelID);  // temprorary for test purposes, to have less data.....


            }
            else
            {
                //string jsonData = ServerUtils.Http.GetLatest(Convert.ToString(43), Convert.ToString(20));

                jsonData = ServerUtils.Http.GetDaily(Convert.ToString(43), Convert.ToString(20));
            }
            // return "value";
            // need to analize json data 


            //return ServerUtils.Http.testWeather();

            //DataContractJsonSerializer
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 50000000;

            dynamic obj = js.Deserialize<dynamic>(jsonData);
            decimal Total = 0.0M;

            for (int i = 0; i < obj["data"].Length;i++ )
            {



                int PlaceHolderRemoveMe = 0;
                if (obj["data"][i]["channels"][0]["valid"] == true)
                    if (obj["data"][i]["channels"][0]["status"] == 1)
                        if (obj["data"][i]["channels"][0]["value"] > 0 && obj["data"][i]["channels"][0]["value"] < 1000)
                            Total += obj["data"][i]["channels"][0]["value"];
                            

            }
            return Total.ToString();

                //return jsonData;
            //return "2021 value "+id.ToString(); // itay

            // 05-02-2021, now need to build class to retrieve weather data
            //System.Runtime.Serialization.Json.DataContractJsonSerializer        
        }






        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}