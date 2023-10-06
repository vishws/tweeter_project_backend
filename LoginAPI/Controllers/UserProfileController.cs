using System;
using System.Linq;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class UserProfileController : ApiController
    {
        [Route("Api/UserProfile/createprofile")]
        [HttpPost]
        public object CreateProfile(UserProfile Lvm)
        {
            try
            {
                using (dell_trainingEntities3 db = new dell_trainingEntities3())
                {
                    // Check if a user with the same credentials already exists
                    var existingUser = db.UserProfiles.FirstOrDefault(u => u.UserID == Lvm.UserID);

                    if (existingUser == null)
                    {
                        return new Response
                        {
                            Status = "Error",
                            Message = "User does not exist, and the profile cannot be updated!"
                        };
                    }
                    else
                    {
                        // Update the existing user's profile
                        existingUser.ProfilePictureURL = Lvm.ProfilePictureURL;
                        existingUser.Bio = Lvm.Bio;
                        existingUser.FollowerCount = Lvm.FollowerCount;

                        // Save changes to the database
                        db.SaveChanges();

                        return Ok(new Response
                        {
                            Status = "Success",
                            Message = "Profile has been changed successfully"
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

    //for retrieving profile details

    [Route("Api/Login/GetUserProfile")]
        [HttpGet]
        public IHttpActionResult GetUserProfile(int userid)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var userDetail = dbContext.UserProfiles
                        .Where(e => e.UserID == userid)
                        .Select(e => new UserProfileDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserId = (int)e.UserID,
                            profileid=e.Profileid,
                            Bio = e.Bio,
                            profilepictureurl = e.ProfilePictureURL,
                            
                        })
                        .FirstOrDefault();

                    if (userDetail == null)
                    {
                        // User not found, return a 404 Not Found response
                        return NotFound();
                    }

                    // User found, return a 200 OK response with the user details
                    return Ok(userDetail);
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