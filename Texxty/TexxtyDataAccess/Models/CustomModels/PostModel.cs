﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class PostModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public System.DateTime PublishedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string UrlField { get; set; }
        public bool Draft { get; set; }
        public string PostContent { get; set; }
        public int BlogID { get; set; }
        public int ViewCount { get; set; }
        
        public List<string> Resources = new List<string>
            {
                "[GET]      api/Blogs/{blog_id}/Posts",
                "[POST]     api/Blogs/{blog_id}/Posts",
                "[GET]      api/Blogs/{blog_id}/Posts/{post_id}",
                "[PUT]      api/Blogs/{blog_id}/Posts/{post_id}",
                "[DELETE]   api/Blogs/{blog_id}/Posts/{post_id}",
                "[PUT]      api/Blogs/{blog_id}/Posts/{post_id}/UpdateViewCount",
            };
    }
}