using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace getVideo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload new videos here";

            string new_video_title = "VIDEO_TITLE";          // This should be obtained from DB
            string api_secret = System.Web.Configuration.WebConfigurationManager.AppSettings["VdoCipher_API_Key"]; ;
            string uri = "https://api.vdocipher.com/v2/uploadPolicy/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII))
            {
                writer.Write("clientSecretKey=" + api_secret + "&title=" + new_video_title);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dynamic otp_data;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json_otp = reader.ReadToEnd();
                otp_data = JObject.Parse(json_otp);
            }
            ViewBag.upload_data = otp_data;
            return View();
        }
    }
}
