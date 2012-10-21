using System;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimeo.API;
using System.Configuration;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;


using LifeTube.Models;

namespace LifeTube.Controllers
{
    public class VideoController : Controller
    {
        LifeTubeEntities1 db = new LifeTubeEntities1();
        
        [HttpPost]
        public static YouTubeRequest GetRequest1()
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings("LifeTube",
                                  ConfigurationManager.AppSettings["YouTubeAPIKey"],
                                  ConfigurationManager.AppSettings["YouTubeUsername"],
                                  ConfigurationManager.AppSettings["YouTubePassword"]);

            YouTubeRequest request = new YouTubeRequest(settings);
            Google.YouTube.Video newVideo = new Google.YouTube.Video();

            newVideo.Title = "My first Movie";
            newVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));
            newVideo.Keywords = "cars, funny";
            newVideo.Description = "My description";
            newVideo.Tags.Add(new MediaCategory("mydevtag, anotherdevtag", YouTubeNameTable.DeveloperTagSchema));
            newVideo.YouTubeEntry.Private = false;

            newVideo.YouTubeEntry.setYouTubeExtension("location", "Somerville, MA");
            var token = request.CreateFormUploadToken(newVideo);
            var strToken = token.Token;
            var strFormAction = token.Url + "?nexturl=http://[ LifeTube ]/form/post-video-step2.aspx?Complete=1";

            //Session["YTRequest"] = request;

            return request;
            //return View(request);
        }
       
        public ActionResult GetRequest()
        {
            //GetRequest1();
            return View();
        }
        protected void PrintForm()
        {
            String url = Session["form_upload_url"] as string;
            String redirect = Session["form_upload_redirect"] as string;
            url += "?nexturl=" + System.Web.HttpUtility.UrlEncode(redirect);
            String token = Session["form_upload_token"] as string;
            Response.Write("<form action='" + url + "' method='post' enctype='multipart/form-data'>");
            Response.Write("<input type='file' name='file'/>");
            Response.Write("<input type='hidden' name='token' value='" + token + "'/>");
            Response.Write("<input type='submit' value='Upload'/>");
            Response.Write("</form>");
        }
        public ActionResult Videos()
        {
            var allVideos=(from v in db.Videos
                               where v.userid==GlobalVariables.UserId
                               select new VideosModel()
                               {
                                   videoid=Convert.ToInt32(v.videoid),
                                   userid=Convert.ToInt32(v.userid),
                                   videoUrl=v.videoUrl,
                                   storedOn=Convert.ToDateTime (v.storedOn),
                                }
                               ).ToList();
            return View();
        }
      
        public ActionResult Success(int status, string id)
	    {
	        if (status != 200) // Upload was not successful. Checking the status code.
	        {
	            ViewData["UploadError"] = true;
	 
	            return RedirectToAction("Upload");
	        }
	       return RedirectToAction("Manage");
	 
	    }
   }
}
