using AspNetRoleBasedSecurity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AspNetRoleBasedSecurity.Models;
using AspNetRoleBasedSecurity.Repository;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace AspNetRoleBasedSecurity
{
    public class NotificationComponent
    {
        //Here we will add a function for register notification (will add sql dependency)  
        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["master"].ConnectionString;
            string sqlCommand = @"SELECT [Id],[Title],[Tags],[User_name],[Status] from [dbo].[Infractio] where [DatePosted] > @DatePosted";
            //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency  
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@DatePosted", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we must have to execute the command here  
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now  
                }
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record  
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client  
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");
                //re-register notification  
                RegisterNotification(DateTime.Now);
            }
        }



        public List<Infractio> GetData()
        {
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();



            if (users == "SuperAdmin")
            {

                using (OTCEntities dc = new OTCEntities())
                {
                    var difdate = (DateTime.Now.AddDays(-5));

                    return dc.Infractios.Where(a => a.Status == "Pending" && a.DatePosted <= difdate).OrderByDescending(a => a.DatePosted).ToList();
                }

            }

            else
            {

                using (OTCEntities dc = new OTCEntities())
                {
                    return dc.Infractios.Where(a => a.Status == "Pending").OrderByDescending(a => a.DatePosted).ToList();
                }

            }

        }

    }
}

