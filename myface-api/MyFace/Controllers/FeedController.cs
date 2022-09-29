using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFace.Helpers;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;
using System;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("feed")]
    public class FeedController : ControllerBase
    {
        private readonly IPostsRepo _posts;
        private readonly IAuthRepo _auth;

        public FeedController(IPostsRepo posts, IAuthRepo auth)
        {
            _posts = posts;
            _auth =  auth;
        }

        [HttpGet("")]
        public ActionResult<FeedModel> GetFeed([FromQuery] FeedSearchRequest searchRequest,  [FromHeader] string authorization)
        {
            string decodedUsernamePassword = AuthenticationHelper.DecodeAuthentication(authorization);
            var usernameAndPassword = AuthenticationHelper.SplitUserNamePassword(decodedUsernamePassword);
            if(usernameAndPassword == null)
            {
                Console.WriteLine("bad news");
                 return Unauthorized();
            }
            
            
                    string username = usernameAndPassword?.Item1;
                    string password = usernameAndPassword?.Item2;           

                    Console.WriteLine(username);
                    Console.WriteLine(password);        
            
        
            if(_auth.ValidateUsernamePassword(username, password))
            {
                 var posts = _posts.SearchFeed(searchRequest);
                 var postCount = _posts.Count(searchRequest);
                 return FeedModel.Create(searchRequest, posts, postCount);
            }
            else
            {
                return Unauthorized();
            }   
        }
    }
}
