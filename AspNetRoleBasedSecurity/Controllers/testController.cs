using AspNetRoleBasedSecurity.Models;
using AspNetRoleBasedSecurity.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AspNetRoleBasedSecurity.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public void CreateBulkIdentityUsers()
        {
            PostRepository PostRepo = new PostRepository();
            var userNames = PostRepo.bulk();

            var una = PostRepo.bulk().Select(d => d.userName);
           

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var users = context.Users.Where(u => una.Contains(u.UserName)).ToList();

                PasswordHasher passwordHasher = new PasswordHasher();

                foreach (var userName in userNames)
                {
                    var user = users.Where(u => u.UserName == userName.userName).FirstOrDefault();

                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            Id = Guid.NewGuid().ToString().ToLower(),
                            UserName = userName.userName,
                            PasswordHash = passwordHasher.HashPassword("Apria@123"),
                            SecurityStamp = Guid.NewGuid().ToString().ToLower(),
                            FirstName = userName.FirstName,
                            LastName = userName.LastName,
                            Email = userName.Email

                        };

                        context.Users.Add(user);
                    }
                }
                context.SaveChanges();
               
            }
            //Response.Redirect("Index");
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)           
        {
            
                var fileName = Path.GetFileName(file.FileName);            

            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Content/Upload/" + fileName);
                file.SaveAs(path);

                string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                //Sheet Name
                excelConnection.Open();
                string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                excelConnection.Close();
                //End

                OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);

                excelConnection.Open();

                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["master"].ConnectionString);

                //Give your Destination table name
                sqlBulk.DestinationTableName = "bulkupload";

                //Mappings
                sqlBulk.ColumnMappings.Add("Id", "Id");
                sqlBulk.ColumnMappings.Add("UserName", "UserName");
                sqlBulk.ColumnMappings.Add("FirstName", "FirstName");
                sqlBulk.ColumnMappings.Add("LastName", "LastName");
                sqlBulk.ColumnMappings.Add("Email", "Email");
               

                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();

                ViewBag.Result = "Successfully Uploaded";
            }
            return View();
        
        }

        public ActionResult SendEmail()
        {
            return View();
        }

           [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("notifications@infraction.com", "Jamil");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Div@051996";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "10.150.5.85",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }


    }
}