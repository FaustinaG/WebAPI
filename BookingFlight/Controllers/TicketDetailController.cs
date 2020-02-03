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
        public IHttpActionResult PostTicketBooking(TicketDetail ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new BookingFlightEntities())
            {
                var tickets = new TicketDetail()
                {
                    BookingStatus = ticket.BookingStatus,
                    PassengerCount = ticket.PassengerCount,
                    TotalFare = ticket.TotalFare,
                    CancellationFare = ticket.CancellationFare,
                    FlightDetailId = ticket.FlightDetailId
                };
                ctx.TicketDetails.Add(tickets); 

                ctx.SaveChanges();
                return Json(new { id = tickets.Id });
            }

        }

        [Route("api/TicketDetail/GetTicketDetail/{userId}")]
        public IHttpActionResult GetTicketDetail(int userId)
        {
            IList<TicketDetailViewModel> tickets = null;

            using (var ctx = new BookingFlightEntities())
            {
                tickets = (from flight in ctx.Flights
                           join flightDetail in ctx.FlightDetails
                           on flight.Id equals flightDetail.FlightId
                           join ticket in ctx.TicketDetails
                           on flightDetail.Id equals ticket.FlightDetailId
                           join history in ctx.UserTicketHistories
                           on ticket.Id equals history.TicketDetailId
                           where userId == history.UserLoginId
                           && ticket.BookingStatus == BookingStatusValues.Confirmed
                           && flightDetail.Departure >= DateTime.Now
                           select new TicketDetailViewModel
                           {
                               FlightName = flight.FlightName,
                               JourneyDate = flightDetail.Departure,
                               FromCity = flightDetail.FromCity,
                               ToCity = flightDetail.ToCity,
                               Price = flightDetail.Price,
                               PassengerCount = ticket.PassengerCount,
                               TotalFare = ticket.TotalFare,
                               BookingStatus = "Confirmed",
                               Id = ticket.Id
                           }).ToList<TicketDetailViewModel>();
            }

            if (!tickets.Any())
            {
                return NotFound();
            }

            return Ok(tickets);
        }

        [Route("api/TicketDetail/PutTicketCancellation/{ticketId}")]
        public IHttpActionResult PutTicketCancellation(int ticketId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new BookingFlightEntities())
            {
                var ticketToBeCancelled = ctx.TicketDetails.Where(x=>x.Id == ticketId).FirstOrDefault<TicketDetail>();

                if(ticketToBeCancelled != null)
                {
                    ticketToBeCancelled.BookingStatus = BookingStatusValues.Cancelled;
                }

                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
