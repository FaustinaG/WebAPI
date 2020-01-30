using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingFlight.Models
{
    public class UserLoginViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType TypeOfUser { get; set; }
    }
}