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

        // GET api/values/5
        public string Get(int id)
        {
            string jsonData = ServerUtils.Http.GetLatest(Convert.ToString(43), Convert.ToString(20));
           // return "value";
            // need to analize json data 
          

            //return ServerUtils.Http.testWeather();

            //DataContractJsonSerializer
            JavaScriptSerializer js=new JavaScriptSerializer();

            dynamic obj = js.Deserialize<dynamic>(jsonData);


            return jsonData;
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