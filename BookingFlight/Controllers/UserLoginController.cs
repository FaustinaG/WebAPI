using BookingFlight.Models;
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
            IList<UserLoginViewModel> users = null;

            using (var ctx = new BookingFlightEntities())
            {
                users = (from user in ctx.UserLogins
                         where user.UserName == UserName && user.Password == Password
                         select new UserLoginViewModel
                         {
                               Id = user.Id,
                               UserName = user.UserName,
                               Password = user.Password,
                               TypeOfUser = user.TypeOfUser
                         }).ToList<UserLoginViewModel>();
            }

            return Ok(users);
        }
    }
}
