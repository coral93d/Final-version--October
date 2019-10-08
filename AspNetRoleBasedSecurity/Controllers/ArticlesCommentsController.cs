using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetRoleBasedSecurity.Models;
using Microsoft.AspNet.Identity;
using AspNetRoleBasedSecurity.Repository;

namespace AspNetRoleBasedSecurity.Controllers
{
    public class ArticlesCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         PostRepository PostRepo = new PostRepository();

        [HttpPost]            
        public ActionResult Updatecheck(int id, bool newValue)
        {
            PostRepository PostRepo = new PostRepository();
            var dbrec = db.ArticlesComments.Find(id);

            dbrec.Com_mark = newValue;
            db.SaveChanges();

            return Json(true);
        }



        // GET: ArticlesComments
        public ActionResult Index()
        {
            return View(db.ArticlesComments.ToList());
        }

        // GET: ArticlesComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var articleId = int.Parse(form["ArticleId"]);
            var rating = int.Parse(form["Rating"]);
            var users = System.Web.HttpContext.Current.User.Identity.GetUserName();

            if (User.IsInRole("Admin")) { 
                ArticlesComment artComment = new ArticlesComment()
            {
                ArticleId = articleId,
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now,
                User_n = users,
                Com_mark = false
                };
                db.ArticlesComments.Add(artComment);
                db.SaveChanges();
            }
            else
            {
                ArticlesComment artComment1 = new ArticlesComment()
                {
                    ArticleId = articleId,
                    Comments = comment,
                    Rating = rating,
                    ThisDateTime = DateTime.Now,
                    User_n = users
                };
                db.ArticlesComments.Add(artComment1);
                db.SaveChanges();

            }
           

            return RedirectToAction("ViewDetails", "Post", new { id = articleId });
        }

        // POST: ArticlesComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] ArticlesComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                db.ArticlesComments.Add(articlesComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articlesComment);
        }

        // GET: ArticlesComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] ArticlesComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articlesComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticlesComment articlesComment = db.ArticlesComments.Find(id);
            db.ArticlesComments.Remove(articlesComment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
