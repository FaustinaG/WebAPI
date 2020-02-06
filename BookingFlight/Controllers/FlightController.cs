using BookingFlight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingFlight.Controllers
{
    public class FlightController : ApiController
    {
        public IHttpActionResult PostFlight(Flight flight)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new BookingFlightEntities())
            {
                var flights = new Flight()
                {
                    FlightName = flight.FlightName,
                    TotalSeats = flight.TotalSeats
                };
                ctx.Flights.Add(flights);

                ctx.SaveChanges();
                return Json(new { id = flights.Id });
            }
        }

        public IHttpActionResult PutFlight(Flight flight)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new BookingFlightEntities())
            {
                var flighttobeUpdated = ctx.Flights.Where(x => x.Id == flight.Id).FirstOrDefault<Flight>();
                if(flighttobeUpdated != null)
                {
                    flighttobeUpdated.FlightName = flight.FlightName;
                    flighttobeUpdated.TotalSeats = flight.TotalSeats;
                }

                ctx.SaveChanges();
                
            }
            return Ok();
        }

        public IHttpActionResult GetFlights()
        {
            IList<FlightViewModel> flights = null;

            using (var ctx = new BookingFlightEntities())
            {
                flights = (from flight in ctx.Flights
                           join flightDetail in ctx.FlightDetails
                           on flight.Id equals flightDetail.FlightId
                           select new FlightViewModel
                           {    
                               FlightName = flight.FlightName,
                               TotalSeats = flight.TotalSeats,
                               Id = flight.Id,
                               FlightId = flight.Id,
                               FlightIdTobeCanceled = flight.Id
                           }).ToList<FlightViewModel>();
            }

            if (!flights.Any())
            {
                return NotFound();
            }

            return Ok(flights);
        }
    }
}
