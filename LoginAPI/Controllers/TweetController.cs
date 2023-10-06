using System;
using System.Linq;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class TweetController : ApiController
    {
        [Route("Api/Tweet/createtweet")]
        [HttpPost]
        public object CreateTweet(Tweet Lvm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                    // Add a breakpoint or log the errors to see why the validation is failing.
                    // For example, you can log the errors using a logging framework or Console.WriteLine.

                    return BadRequest(string.Join(", ", errors));
                }
                else
                {
                    using (dell_trainingEntities3 db = new dell_trainingEntities3())
                    {
                        // Create a new tweet object
                        Tweet Em = new Tweet
                        {
                            UserID = Lvm.UserID,
                            Content = Lvm.Content,
                            CreatedAt = DateTime.Now // You can set the creation timestamp here

                        };

                        // Add the tweet to the database
                        db.Tweets.Add(Em);
                        db.SaveChanges();

                        return Ok(new Response
                        {
                            Status = "Success",
                            TweetID = Em.TweetID,
                            Message = "Tweet has been successfully created"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log the error, or return an appropriate error response
                return InternalServerError(ex);
            }
        }

        [Route("Api/Tweet/retweet")]
        [HttpPost]
        public object ReTweet(Retweet Lvm)
        {
            try
            {
                using (dell_trainingEntities3 db = new dell_trainingEntities3())
                {
                    // Create a new retweet object
                    Retweet Em = new Retweet
                    {
                        UserID = Lvm.UserID,
                        TweetID = Lvm.TweetID,
                        CreatedAt = DateTime.Now // You can set the creation timestamp here
                    };

                    // Add the tweet to the database
                    db.Retweets.Add(Em);
                    db.SaveChanges();

                    return Ok(new Response
                    {
                        Status = "Success",
                        Message = "Retweet registered sucessfully"
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log the error, or return an appropriate error response
                return InternalServerError(ex);
            }
        }

        [Route("Api/Tweet/like")]
        [HttpPost]
        public object Likes(Like Lvm)
        {
            try
            {
                using (dell_trainingEntities3 db = new dell_trainingEntities3())
                {
                    // Create a new like object
                    Like Em = new Like
                    {
                        UserID = Lvm.UserID,
                        TweetID = Lvm.TweetID,
                        CreatedAt = DateTime.Now // You can set the creation timestamp here
                    };

                    // Add the tweet to the database
                    db.Likes.Add(Em);
                    db.SaveChanges();

                    return Ok(new Response
                    {
                        Status = "Success",
                        Message = "Like has been registered"
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log the error, or return an appropriate error response
                return InternalServerError(ex);
            }
        }

        [Route("Api/Tweet/comment")]
        [HttpPost]
        public object Comment(Comment Lvm)
        {
            try
            {
                using (dell_trainingEntities3 db = new dell_trainingEntities3())
                {
                    // Create a new like object
                    Comment Em = new Comment
                    {
                        UserID = Lvm.UserID,
                        TweetID = Lvm.TweetID,
                        Content = Lvm.Content,
                        CreatedAt = DateTime.Now // You can set the creation timestamp here
                    };

                    // Add the tweet to the database
                    db.Comments.Add(Em);
                    db.SaveChanges();

                    return Ok(new Response
                    {
                        Status = "Success",
                        TweetID = (int)Em.TweetID,
                        Message = "Comment has been added sucessfully"
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log the error, or return an appropriate error response
                return InternalServerError(ex);
            }
        }

        //for retrieving tweet details

        [Route("Api/Tweet/GetTweet")]
        [HttpGet]
        public IHttpActionResult GetTweet(int userid)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var tweetDetail = dbContext.Tweets
                        .Where(e => e.UserID == userid)
                        .Select(e => new GetTweetDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserID = (int)e.UserID,
                            TweetID = e.TweetID,
                            Content = e.Content,
                            CreatedAt = (DateTime)e.CreatedAt
                        })
                        .ToList();

                    if (tweetDetail == null)
                    {
                        // User not found, return a 404 Not Found response
                        return NotFound();
                    }

                    // User found, return a 200 OK response with the user details
                    return Ok(tweetDetail);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, log the error, and return an error response
                return InternalServerError(ex);
            }
        }

        //for retrieving tweet comments details

        [Route("Api/Tweet/GetComments")]
        [HttpGet]
        public IHttpActionResult GetComments(int userid)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var tweetDetail = dbContext.Comments
                        .Where(e => e.UserID == userid)
                        .Select(e => new GetCommentsDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserID = (int)e.UserID,
                            Content = e.Content,
                            CommentID = e.CommentID,
                            TweetID = (int)e.TweetID,
                            CreatedAt = (DateTime)e.CreatedAt
                        })
                       .ToList();

                    if (tweetDetail == null)
                    {
                        // User not found, return a 404 Not Found response
                        return NotFound();
                    }

                    // User found, return a 200 OK response with the user details
                    return Ok(tweetDetail);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, log the error, and return an error response
                return InternalServerError(ex);
            }
        }

        //for retrieving tweet comments details

        [Route("Api/Tweet/GetLikes")]
        [HttpGet]
        public IHttpActionResult GetLikes(int userid)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var tweetDetail = dbContext.Likes
                        .Where(e => e.UserID == userid)
                        .Select(e => new GetLikesDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserID = (int)e.UserID,
                            LikeID = e.LikeID,
                            TweetID = (int)e.TweetID,

                            CreatedAt = (DateTime)e.CreatedAt
                        })
                        .ToList();

                    if (tweetDetail == null)
                    {
                        // User not found, return a 404 Not Found response
                        return NotFound();
                    }

                    // User found, return a 200 OK response with the user details
                    return Ok(tweetDetail);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, log the error, and return an error response
                return InternalServerError(ex);
            }
        }

        //for retrieving retweet details

        [Route("Api/Tweet/GetReTweets")]
        [HttpGet]
        public IHttpActionResult GetReTweets(int userid)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var tweetDetail = dbContext.Retweets
                        .Where(e => e.UserID == userid)
                        .Select(e => new GetReTweetsDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserID = (int)e.UserID,
                            ReTweetID = (int)e.RetweetID,
                            TweetID = (int)e.TweetID,
                            CreatedAt = (DateTime)e.CreatedAt
                        })
                        .ToList();

                    if (tweetDetail == null)
                    {
                        // User not found, return a 404 Not Found response
                        return NotFound();
                    }

                    // User found, return a 200 OK response with the user details
                    return Ok(tweetDetail);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, log the error, and return an error response
                return InternalServerError(ex);
            }
        }
    }
}