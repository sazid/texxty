﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Slugify;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository;
using Texxty.Repository.Classes;

namespace Texxty.Controllers
{
    public class PostController : Controller 
    {
        private readonly PostRepository postrepo;
        private readonly SlugHelper helper;
        private int blogid { get; set; }
        //int blogid;

        public PostController()
        {
            postrepo = new PostRepository();
            helper = new SlugHelper();
            //blogid = Convert.ToInt32(Session["blogID"]);
        }

        
        // GET: Post
        
        public ActionResult Index(int id)
        {
            //return View(postrepo.GetAllPosts(blogid));
            blogid = id;
            ViewBag.BlogID = blogid;
            return View(postrepo.GetAllPosts(id));

        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.BlogID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post p, int BlogID)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BlogID = BlogID;
                return View();
            }
                
            p.ModifiedDate = DateTime.Now;
            p.PublishedDate = DateTime.Now;
            //p.BlogID = blogid;
            p.BlogID = BlogID;
            p.UrlField = helper.GenerateSlug(p.Title.ToString());
            if (p.Draft==false)
            { }
            postrepo.Insert(p);
            return RedirectToAction("Index", new { id = p.BlogID });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(postrepo.Get(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(postrepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Post p)
        {
            

            if (!ModelState.IsValid)
                return View(p);
            p.ModifiedDate = DateTime.Now;

            p.UrlField = helper.GenerateSlug(p.Title.ToString());
                
            postrepo.Update(p);
            return RedirectToAction("Index", new { id = p.BlogID});
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(postrepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var post = postrepo.Get(id);
            postrepo.Delete(id);
            return RedirectToAction("Index", new { id = post.BlogID });
        }


        // This method is for view only from feed to post details
        [HttpGet]
        public ActionResult PostDetails(int id)
        {
            var model = postrepo.Get(id);
            var blodId = model.BlogID;
            var blogRepo = new BlogRepository();
            var blogInfo = blogRepo.Get(blodId);
            ViewBag.UserInfo = blogInfo.User.FullName;            
            return View(model);
        }

    }
}