using AspNetRoleBasedSecurity.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetRoleBasedSecurity.Models;
using Microsoft.AspNet.Identity;


namespace AspNetRoleBasedSecurity.Controllers
{
    public class HomeController : Controller
    {
        PostRepository PostRepo = new PostRepository();
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetNotifications()
        {
           
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();

            var list = NC.GetData();

            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //update session here for get only new added contacts (notification)  

        }

        public JsonResult GetMessages()
        {
            // PostRepository _messageRepository = new PostRepository();
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
            using (OTCEntities2 dc = new OTCEntities2())
            {
                List<notification> Notification = new List<notification>();

                Notification = dc.notifications.Where(a => a.User_name == users).OrderByDescending(a => a.DatePosted).Take(10).ToList();

                return Json(Notification, JsonRequestBehavior.AllowGet);
            }
        }





        public JsonResult GetRecord(string prefix)
        {
            DataSet ds = PostRepo.GetName(prefix);
            List<search> searchlist = new List<search>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchlist.Add(new search
                {
                    Title = dr["Title"].ToString(),
                    Tags = dr["Tags"].ToString(),
                    Id = dr["Id"].ToString()
                });
                
            }
            
            return Json(searchlist, JsonRequestBehavior.AllowGet);
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
    }
}