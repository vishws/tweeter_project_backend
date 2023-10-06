using System;// For the Exception class
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IdentityModel.Tokens.Jwt;// For the JWT
using System.Linq;// For the .ToList() and .FirstOrDefault() methods
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web.Http;// For the API controller
using System.Web.Http.Cors;
using LoginAPI.Models;// For the DTO
//using System.Web.Http.Cors;

//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Text;

namespace LoginAPI.Controllers
{
    public class LoginController : ApiController
    {
        //For user login
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public HttpResponseMessage Login(LoginAPI.Models.Login Lg)
        {
            dell_trainingEntities3 DB = new dell_trainingEntities3();

            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Response { Status = "Error", Message = "Invalid Data.", Errors = errors });
            }
            else
            {
                // Retrieve the user's record from the database based on their username
                var user = DB.Employeemasters.FirstOrDefault(u => u.UserName == Lg.UserName);

                if (user == null)
                {
                    // User not found, indicating failed login
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new Response { Status = "Invalid", Message = "User doesn't exist!" });
                }

                // Verify the entered password against the stored hashed password
                if (PasswordHasher.VerifyPassword(Lg.Password, user.Password))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName), // Include user name
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Include user ID
                // Add other claims as needed
                new Claim("Email", user.Email), // Include user email
                new Claim("Username", user.UserName), // Include user username
                 new Claim("ContactNo", user.ContactNo), // Include user ContactNo
                 new Claim("Address", user.Address), // Include user Address
                // Add more claims here as needed
            };

                    // Generate a 256-bit key
                    byte[] keyBytes = new byte[32]; // 32 bytes = 256 bits
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(keyBytes);
                    }

                    // Convert the key bytes to a base64-encoded string
                    string base64Key = Convert.ToBase64String(keyBytes);

                    // Use base64Key as your secret key for JWT
                    var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(base64Key));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:53678",
                        audience: "https://localhost:53678",
                        claims: claims, // Include claims in the token
                        expires: DateTime.Now.AddMinutes(30), // Adjust token expiration as needed
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    // Create a custom response object to include more information
                    var response = new
                    {
                        Token = tokenString,
                        UserName = user.UserName,
                        UserId = user.UserId,
                        Email = user.Email,
                        ContactNo = user.ContactNo,
                        Addresss = user.Address
                        // Add more properties here as needed
                    };

                    // Return the custom response as JSON in the response body
                    return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
                }
                else
                {
                    // Passwords do not match, indicating failed login
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new Response { Status = "Invalid", Message = "Incorrect password entered!" });
                }
            }

        }

        //For new user Registration
        [Route("Api/Login/createcontact")]
        [HttpPost]
        public object createcontact(Registration Lvm)
        {
            try
            {
                dell_trainingEntities3 db = new dell_trainingEntities3();

                // Check if a user with the same credentials already exists
                var existingUser = db.Employeemasters.FirstOrDefault(u =>
                    u.UserName == Lvm.UserName ||
                    u.Email == Lvm.Email
                );

                if (existingUser == null)
                {
                    if (!ModelState.IsValid)
                    {
                        // If the model state is not valid, return validation errors
                        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                        return new Response { Status = "Error", Message = "Invalid Data.", Errors = errors };
                    }
                    else
                    {
                        // Create a new user object
                        Employeemaster Em = new Employeemaster();
                        if (Em.UserId == 0)
                        {
                            Em.UserName = Lvm.UserName;

                            // Hash the password using the PasswordHasher class
                            Em.Password = PasswordHasher.HashPassword(Lvm.Password);

                            Em.Email = Lvm.Email;
                            Em.ContactNo = Lvm.ContactNo;
                            Em.Address = Lvm.Address;

                            db.Employeemasters.Add(Em);
                            db.SaveChanges();
                            return new Response
                            { Status = "Success", Message = "Profile has been successfully created" };
                        }
                    }
                }
                else
                {
                    return new Response
                    { Status = "Error", Message = "User already exists" };
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Log or handle the exception as needed
                Console.WriteLine(exceptionMessage);

                throw new DbEntityValidationException(exceptionMessage);
            }

            return new Response
            { Status = "Error", Message = "Invalid Data." };

           
        }


        //for retrieving user details

        [Route("Api/Login/GetUserDetails")]
        [HttpGet]
        public IHttpActionResult GetUserDetails(string username)
        {
            try
            {
                using (var dbContext = new dell_trainingEntities3())
                {
                    // Retrieve user details based on the username
                    var userDetail = dbContext.Employeemasters
                        .Where(e => e.UserName == username)
                        .Select(e => new EmployeemasterDTO
                        {
                            // Map the properties you need from Employeemaster to the DTO
                            // For example:
                            UserId = e.UserId,
                            UserName = e.UserName,

                            //Password=e.Password,
                            Email = e.Email,
                            ContactNo = e.ContactNo,
                            Address = e.Address,
                            //IsApporved = (int)e.IsApporved,
                            //Status = (int)e.Status
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


        // For resetting password
        [Route("Api/Login/ResetPassword")]
        [HttpPost]
        public HttpResponseMessage ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                // Check if the model state is not valid
                if (!ModelState.IsValid)
                {
                    // If the model state is not valid, return validation errors
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new Response { Status = "Error", Message = "Invalid Data.", Errors = errors });
                }

                using (dell_trainingEntities3 db = new dell_trainingEntities3())
                {
                    // Check if a user with the given username exists
                    var user = db.Employeemasters.FirstOrDefault(u => u.UserName == resetPasswordModel.UserName);

                    if (user != null)
                    {
                        // Update the user's password to the new password
                        user.Password = PasswordHasher.HashPassword(resetPasswordModel.NewPassword);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, new Response { Status = "Success", Message = "Password reset successful." });
                    }
                    else
                    {
                        // User not found
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new Response { Status = "Error", Message = "User not found." });
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Log or handle the exception as needed
                Console.WriteLine(exceptionMessage);

                throw new DbEntityValidationException(exceptionMessage);
            }
        }

    }


}
