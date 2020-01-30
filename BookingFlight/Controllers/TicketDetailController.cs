using BookingFlight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingFlight.Controllers
{
    public class TicketDetailController : ApiController
    {
        public IHttpActionResult PostTicketBooking(TicketDetailViewModel ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new BookingFlightEntities())
            {
                ctx.TicketDetails.Add(new TicketDetail()
                {
                    BookingStatus = ticket.BookingStatus,
                    PassengerCount = ticket.PassengerCount,
                    TotalFare = ticket.TotalFare,
                    CancellationFare = ticket.CancellationFare,
                    FlightDetailId = ticket.FlightDetailId
                }); 

                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
