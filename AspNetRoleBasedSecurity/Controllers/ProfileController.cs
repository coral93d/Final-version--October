using AspNetRoleBasedSecurity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspNetRoleBasedSecurity.Models;
using Microsoft.AspNet.Identity;

namespace AspNetRoleBasedSecurity.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        ProfileRepository PostRepo = new ProfileRepository();
        // GET: Profile

        [Authorize]
        public ActionResult MasterDetail()
        {
            ProfileRepository objDet = new ProfileRepository();
            ProfileRepository objDets = new ProfileRepository();
            ProfileRepository objDetss = new ProfileRepository();

            var post = objDet.GetMasterDetails();
            var userprofile = objDets.GetMasterUser();
            var commentg = objDetss.Getcommentss();
             return View(new MasterDetails { Post = post, UserProfile = userprofile,Commentg= commentg });
        }
        [Authorize]
        public ActionResult ProfileDetail(string user)
        {
            ProfileRepository objDet = new ProfileRepository();
            ProfileRepository objDets = new ProfileRepository();
            ProfileRepository objDetss = new ProfileRepository();
           


            var post = objDet.GetProfileDetails(user);
            var userprofile = objDets.GetProfileUser(user);
            var comments = objDetss.Getcommentss();

            return View(new profileDetail { Post = post, UserProfile = userprofile,Comments= comments });
        }

      
        public ActionResult EditProfile(string user)
        {
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();

            ProfileRepository PostRepo = new ProfileRepository();
            return View(PostRepo.GetMasterUsers().Find((Post => Post.user == users)));
        }

        [HttpPost]

        public ActionResult EditProfile(string user,Profilee obj)

        {
            try
            {
                
                    ProfileRepository PostRepo = new ProfileRepository();
                    
                    PostRepo.EditProfile(obj);
                    return RedirectToAction("MasterDetail");

              
            }
            catch
            {
                return View();
            }
        }


    }
}