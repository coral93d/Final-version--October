using AspNetRoleBasedSecurity.Models;
using AspNetRoleBasedSecurity.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI;
using System.Data.Entity;

using System.IO;
using PagedList;

namespace AspNetRoleBasedSecurity.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        PostRepository PostRepo = new PostRepository();

        // GET: Post
        [Authorize]
        public ActionResult AddPost()
        {
            return View();
        }
        // POST: Employee/AddEmployee

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostModel Post)
        {
            try
            {
                if (Post.Tags != null)
                {
                    if (ModelState.IsValid)
                    {
                        PostRepository PostRepo = new PostRepository();

                        if (PostRepo.AddPost(Post))
                        {
                            var submitteduser = System.Web.HttpContext.Current.User.Identity.GetUserName();
                            var Infractiontitle = Post.Title;
                                

                            ViewBag.Message = "Added successfully!!! (Submitted For Admin Review)";
                            PostRepository Profilerepo = new PostRepository();
                            //.Where(User => User.Title.StartsWith(search)
                            //.Select(Modelitems => Modelitems.getProcess.ToString()).FirstOrDefault()
                            //var Infractionusertag = Model.tagfilter.Where(Modelitems => Modelitems.user.Equals(item.user)).
                                                                                         //Select(Modelitems => Modelitems.getProcess.ToString()).FirstOrDefault();

                            var loginuser = System.Web.HttpContext.Current.User.Identity.GetUserName();
                            var loginusertrack = Profilerepo.Users().Where(Users => Users.user == loginuser).Select(Users => Users.getProcess.ToString()).FirstOrDefault();
                            var loginAdmin = Profilerepo.Users().Where(Users => Users.getProcess == loginusertrack).ToList();

                            var _ctx = new ApplicationDbContext();
                            // var roles = context.Roles.ToList();
                            var usersWithRoles = (from user in _ctx.Users
                                                  select new
                                                  {
                                                      Usernamee = user.UserName,
                                                      Email = user.Email,
                                                      RoleNames = (from userRole in user.Roles
                                                                  
                                                                   join role in _ctx.Roles  on userRole.RoleId equals role.Id
                                                                   where role.Name == "Admin"
                                                                   select role.Name).ToList()
                                                  }).ToList().Select(p => new UserViewModel()

                                                  {
                                                      Usernamee = p.Usernamee,
                                                      Emaill = p.Email,
                                                      Rolee = string.Join(",", p.RoleNames)
                                                  });
                            var roleuser = usersWithRoles.Where(Users => Users.Rolee == "Admin").ToList();
                            //  var user = await UserManager.FindAsync(model.UserName, model.Password);
                            foreach (var item in loginAdmin)
                            {
                                var emailuser = roleuser.Where(Users => Users.Usernamee == item.user).Select(Users => Users.Emaill.ToString()).FirstOrDefault();
                               
                                if (emailuser != null) {
                                    string subject = submitteduser + " " + "from " + loginusertrack + " " + "posted infraction for review ";

                                    string body = PopulateBody(submitteduser, loginusertrack, Infractiontitle);

                                 SendHtmlFormattedEmail (emailuser, subject, body);



                                }
                            }                            
                        }
                        else
                        {
                            ViewBag.Message = "Duplicate Entry";
                        }
                        //  ModelState.Clear();
                    }
                }

                return View();
            }
            catch
            {

                return View();
            }

        }


        private string PopulateBody(string submitteduser, string loginusertrack, string Infractiontitle)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
              
             body = body.Replace("{UserName}", submitteduser);
                body = body.Replace("{Track}", loginusertrack);
                body = body.Replace("{message}", Infractiontitle);
                return body;
        }


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("notification@infraction.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                //smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
               // System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
               // NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
              //  NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
               // smtp.UseDefaultCredentials = true;
               // smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
               // host.ConnectType = SmtpConnectType.ConnectTryTLS;
                smtp.Send(mailMessage);
            }
        }



        // GET: Employee/GetAllPostDetails 
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllPost(string search)
        {
            var loguser = System.Web.HttpContext.Current.User.Identity.GetUserName();
            ViewBag.Loguser = loguser;
            PostRepository PostRepo = new PostRepository();
            PostRepository PostRepo2 = new PostRepository();
            PostRepository PostRepo3 = new PostRepository();
            PostRepository PostRepo4 = new PostRepository();

            ModelState.Clear();

            var post = PostRepo.GetAllPost().OrderByDescending(Post => Post.DatePosted);
            var comments = PostRepo2.Getcomments();
            var Tags = PostRepo3.Tags();
            var popular = PostRepo.GetAllPost().OrderByDescending(Post => Post.PageView).Take(6);
            var tagfilter = PostRepo4.Users();
            var filter = PostRepo.GetAllPost();

            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = (PostRepo.GetAllPost().Where(User => User.Title.StartsWith(search, StringComparison.InvariantCultureIgnoreCase) || User.user.StartsWith(search, StringComparison.InvariantCultureIgnoreCase) || search == null).ToList());
            }

            return View(new PostDetails { Post = post, Comments = comments, popular = popular, Track = Tags, tagfilter = tagfilter, Filter = filter });
        }
        // GET: Employee/GetAllUserDetails    
        public ActionResult GetAllPostUser(string Searchby, string search)
        {
            //int recordsPerPage = 2;

            PostRepository PostRepo = new PostRepository();
            PostRepository PostRepo2 = new PostRepository();
            PostRepository PostRepo3 = new PostRepository();
            PostRepository PostRepo4 = new PostRepository();
            List<PostDetails> file = new List<PostDetails>();
            ModelState.Clear();

            var post = PostRepo.GetAllPost().OrderByDescending(Post => Post.DatePosted);
            var popular = PostRepo.GetAllPost().OrderByDescending(Post => Post.PageView).Take(6);
            var comments = PostRepo2.Getcomments();
            var Tags = PostRepo3.Tags();
            var tagfilter = PostRepo4.Users();
            var filter = PostRepo.GetAllPost();


            if (!string.IsNullOrWhiteSpace(search))
            {
                // string temp = Tags as string;
               // var user_track = tagfilter.Where(Modelitems => (Modelitems.user.Equals())).Select(Modelitems => Modelitems.getProcess.ToString()).FirstOrDefault();
                // List<Tags> Tagss = Tags.Split(',').ToList();
                filter = (PostRepo.GetAllPost().Where(User => User.Title.StartsWith(search, StringComparison.InvariantCultureIgnoreCase) || (User.getTags.Split(',',' ').Contains(search, StringComparer.InvariantCultureIgnoreCase)) || search == null).ToList());
            }

            return View(new PostDetails { Post = post, Comments = comments, popular = popular, Track = Tags, tagfilter = tagfilter, Filter = filter });
        }

        // View details from comment& Infrac tab 
        [Authorize]
        public ActionResult ViewDetails(int id, PostModel obj)
        {

            var userss = System.Web.HttpContext.Current.User.Identity.GetUserName();
            PostRepository PostRepo = new PostRepository();
            PostRepository PostRepo1 = new PostRepository();
            PostRepository PostRepo4 = new PostRepository();
            ViewBag.ArticleId = id;
            var comments = db.ArticlesComments.Where(d => d.ArticleId.Equals(id)).ToList();
            ViewBag.Comments = comments;
            var ratings = db.ArticlesComments.Where(d => d.ArticleId.Equals(id)).ToList();
            var com = db.ArticlesComments.Where(d => d.ArticleId.Equals(id) && (d.Com_mark) == true).ToList();
            if (ratings.Count() > 0 && com.Count() > 0)
            {
                var ratingSum = com.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = com.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }
            var tagfilter = PostRepo4.UserTag().Where(User => User.userr.Equals(userss)).Select(User => User.getProcess.ToString()).FirstOrDefault();
            //var Loginusertag = Model.tagfilter.Where(Modelitems => Modelitems.user == ViewBag.loguser).Select(Modelitems => Modelitems.getProcess.ToString()).FirstOrDefault();
            ViewBag.Tags = tagfilter;

            PostRepo1.ViewDetails(obj);

            return View(PostRepo.GetAllPost().Find((Post => Post.Id == id)));

        }

        // Testing code    
        [HttpPost]
        public ActionResult Updatecheck(int id, bool newValue)
        {
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
            PostRepository PostRepo = new PostRepository();
            var dbrec = db.ArticlesComments.Find(id);

            dbrec.Status_Com = newValue;
            dbrec.Marked_by = users;
            dbrec.Marked_on = DateTime.Now;
            db.SaveChanges();

            return Json(true);
        }

        // GET:get user details from tab   
        public ActionResult UserProfile()
        {
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();
            PostRepository PostRepo = new PostRepository();
            ModelState.Clear();
            return View(PostRepo.UserProfile().OrderByDescending(Post => Post.DatePosted));

        }
        // GET: Employee/GetAllUserDetails    
        public ActionResult EditProfile()
        {
            ProfileRepository PostRepo = new ProfileRepository();
            return View(PostRepo.GetMasterUser());
        }

        [HttpPost]
        public ActionResult EditProfile(Profile Post)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    PostRepository PostRepo = new PostRepository();
                    if (PostRepo.EditProfile(Post))
                    {

                        ViewBag.Message = "Added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }



        public ActionResult EditPostDetails(int id)
        {
            PostRepository PostRepo = new PostRepository();



            return View(PostRepo.GetAllPost().Find((Post => Post.Id == id)));

        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditPostDetails(int id, PostModel obj)
        {
            try
            {
                PostRepository PostRepo = new PostRepository();

                PostRepo.UpdatePost(obj);

                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("GetAllPost");
                }
                else
                {
                    return RedirectToAction("GetAllPostUser");
                }

            }
            catch
            {
                return View();
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(int id, Tags obj, PostModel objj)
        {
            PostRepository PostRepo = new PostRepository();
            ModelState.Clear();
            // Debug.WriteLine(PostRepo.Tags().Find((Track => Track.Id == id)));
            ///   System.Diagnostics.Trace.WriteLine(PostRepo.Tags());
            return View(PostRepo.Tags().Find((Track => Track.Id == id)));

        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Users(string search)
        {
            PostRepository PostRepo = new PostRepository();
            ModelState.Clear();
            var users = PostRepo.Users();
            if (!string.IsNullOrWhiteSpace(search))
            {
                users = (PostRepo.Users().Where(User => User.user.StartsWith(search) || search == null).ToList());
            }
            return View(new AllUsers { Users = users });

        }



        public ActionResult Tags(string search)
        {
            PostRepository PostRepo = new PostRepository();
            ModelState.Clear();
            var track = PostRepo.Tags();
            if (!string.IsNullOrWhiteSpace(search))
            {
                track = (PostRepo.Tags().Where(User => User.Track_Name.StartsWith(search) || search == null).ToList());
            }
            return View(new Tagg { Track = track });

        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("GetAllPost");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Delete(int id, int ids)
        {

            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            db.ArticlesComments.Remove(articlesComment);
            db.SaveChanges();
            return RedirectToAction("ViewDetails", "Post", new { id = ids });


        }

        // POST: ArticlesComments/Delete/5

        public ActionResult CommentsList()
        {
            return View(db.ArticlesComments.ToList());
        }



        public ActionResult EditTag(int id)
        {
            PostRepository PostRepo = new PostRepository();

            return View(PostRepo.Tags().Find((Post => Post.Id == id)));

        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]
        public ActionResult EditTag(int id, Tags obj)
        {
            try
            {
                PostRepository PostRepo = new PostRepository();

                PostRepo.UpdateTags(obj);
                return RedirectToAction("Contact", new { id = id });
            }
            catch
            {
                return View();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------
       
       
    }
    }

   