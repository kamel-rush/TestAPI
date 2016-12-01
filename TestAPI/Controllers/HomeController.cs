using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetData()
        {
  HttpWebRequest request =
     WebRequest.CreateHttp("http://forecast.weather.gov/MapClick.php?lat=42.335722&lon=-83.049944&FcstType=json");

            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            // request.Headers.Add("X-Mashape-Key",value);

           HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            
            string ApiText = rd.ReadToEnd();

            JObject weatherData = JObject.Parse(ApiText);

            //ViewBag.ApiText = weatherData["location"]["region"];
            // ViewBag.ApiText = weatherData["data"]["text"][0];

            foreach (var element in weatherData["data"]["temperature"])
            {
                ViewBag.ApiText += element + ",";
            }

            ViewBag.ApiText = ViewBag.ApiText.Trim(',');

            return View("APIView");



        }


        public ActionResult GetDataxml()
        {
            HttpWebRequest request =
               WebRequest.CreateHttp("http://forecast.weather.gov/MapClick.php?lat=42.335722&lon=-83.049944&FcstType=xml");

            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            // request.Headers.Add("X-Mashape-Key",value);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());

           // string ApiText = rd.ReadToEnd();

            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(rd);

            //ViewBag.ApiText = xdoc["Forecast"]["icon-location"].InnerText;

            foreach (XmlNode n in xdoc.GetElementsByTagName("period"))
            {
                ViewBag.ApiText += n["text"].InnerText+",";

            }

            return View("APIView");

        }
        }
}