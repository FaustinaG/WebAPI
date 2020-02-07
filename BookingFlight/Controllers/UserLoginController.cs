﻿using BookingFlight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingFlight.Controllers
{
    
    public class UserLoginController : ApiController
    {
        public IHttpActionResult PostUser(UserLogin user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new BookingFlightEntities())
            {
                ctx.UserLogins.Add(new UserLogin()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    TypeOfUser = user.TypeOfUser
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        [Route("api/UserLogin/GetUser/{UserName}/{Password}")]
        public IHttpActionResult GetUser(string UserName, string Password)
        {
            UserLogin userlogin = null;

            using (var ctx = new BookingFlightEntities())
            {
                 userlogin = (from user in ctx.UserLogins
                          where user.UserName == UserName && user.Password == Password
                          select user).FirstOrDefault() ;
            }
            if (userlogin != null)
            {
                return Json(new { id = userlogin.Id, UserType = userlogin.TypeOfUser });
            }
            else
            {
                return Json(new { });
            }
        }
    }
}
