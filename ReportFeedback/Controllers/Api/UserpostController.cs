using ReportFeedback.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReportFeedback.Controllers.Api
{
    public class UserpostController : ApiController
    {
        ReportFeedbackDBEntities db = new ReportFeedbackDBEntities();

        [HttpGet]
        [Route("api/Userpost/UserPost")]
        public IHttpActionResult UserPost()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var posts = (from p in db.Post_tb
                         join u in db.User_tb on p.PostId equals u.UserId
                         select new
                         {
                             PostId = p.PostId,
                             PostDetails = p.PostDetails,
                             PostDate = p.PostDate,
                             UserId = p.UserId,
                             UserName = u.UserName
                         });


            return Ok(posts);
        }

        [HttpGet]
        [Route("api/Userpost/UserComment")]
        public IHttpActionResult UserComment()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var comments = (from c in db.Comment_tb
                            join u in db.User_tb on c.UserId equals u.UserId
                            select new
                            {
                                CommentId = c.CommentId,
                                CommentDetails = c.CommentDetails,
                                CommentDate = c.CommentDate,
                                LikeCount = c.LikeCount,
                                DislikeCount = c.DislikeCount,
                                UserId = c.UserId,
                                PostId = c.PostId,
                                UserName = u.UserName
                            });


            return Ok(comments);
        }
        [HttpGet]
        [Route("api/Userpost/GetPagedMinus")]
        public IHttpActionResult GetPagedMinus()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var posts = (from p in db.Post_tb
                         join u in db.User_tb on p.PostId equals u.UserId
                         select new
                         {
                             PostId = p.PostId,
                             PostDetails = p.PostDetails,
                             PostDate = p.PostDate,
                             UserId = p.UserId,
                             UserName = u.UserName
                         });
            var count = posts.Count();
            var take = posts.Take(count - 1).ToList();
            return Ok(take);
        }
        [HttpGet]
        [Route("api/Userpost/GetPagedMore")]
        public IHttpActionResult GetPagedMore()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var posts = (from p in db.Post_tb
                         join u in db.User_tb on p.PostId equals u.UserId
                         select new
                         {
                             PostId = p.PostId,
                             PostDetails = p.PostDetails,
                             PostDate = p.PostDate,
                             UserId = p.UserId,
                             UserName = u.UserName
                         });
            var count = posts.Count();
            var take = posts.Take(count + 1).ToList();
            return Ok(take);
        }
        [HttpGet]
        [Route("api/Userpost/Search")]
        public IHttpActionResult Search(string item)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var posts = (from p in db.Post_tb
                         join u in db.User_tb on p.PostId equals u.UserId
                         select new
                         {
                             PostId = p.PostId,
                             PostDetails = p.PostDetails,
                             PostDate = p.PostDate,
                             UserId = p.UserId,
                             UserName = u.UserName
                         });
           
            List<object> postList = new List<object>();
            foreach (var post in posts)
            {

                if (post.PostDetails.Contains(item))
                {
                    postList.Add(post);

                }
                if (post.UserName.Contains(item))
                {
                    postList.Add(post);
                }

            }

            return Ok(postList);
        }
        [HttpPost]
        [Route("api/Userpost/UserCommentUpdate")]
        public IHttpActionResult UserCommentUpdate(Comment_tb comment)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Comment_tb comObj = db.Comment_tb.Where(x => x.CommentId == comment.CommentId).FirstOrDefault();
            comObj.CommentId = comment.CommentId;
            comObj.CommentDetails = comment.CommentDetails;
            comObj.CommentDate = comment.CommentDate;
            comObj.LikeCount = comment.LikeCount;
            comObj.DislikeCount = comment.DislikeCount;
            comObj.UserId = comment.UserId;
            comObj.PostId = comment.PostId;
            db.SaveChanges();
            return Ok();
        }



    }
}
