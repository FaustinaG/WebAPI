using BookingFlight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingFlight.Controllers
{
    public class FlightDetailController : ApiController
    {
        public IHttpActionResult GetFlights()
        {
            IList<FlightDetailViewModel> flights = null;

            using (var ctx = new BookingFlightEntities())
            {
                flights = (from flight in ctx.Flights
                          join flightDetail in ctx.FlightDetails
                          on flight.Id equals flightDetail.FlightId
                          select new FlightDetailViewModel
                          {
                              Id = flightDetail.Id,
                              FlightName = flight.FlightName,
                              Departure = flightDetail.Departure,
                              Arrival = flightDetail.Arrival,
                              JourneyDate = flightDetail.Departure,
                              FromCity = flightDetail.FromCity,
                              ToCity = flightDetail.ToCity,
                              Price = flightDetail.Price,
                              SeatAvailability = flightDetail.SeatAvailability,
                          }).ToList<FlightDetailViewModel>();
            }

            if (!flights.Any())
            {
                return NotFound();
            }

            return Ok(flights);
        }

        public IHttpActionResult GetFlightsById(int id)
        {
            IList<FlightDetailViewModel> flights = null;

            using (var ctx = new BookingFlightEntities())
            {
                flights = (from flight in ctx.Flights
                           join flightDetail in ctx.FlightDetails
                           on flight.Id equals flightDetail.FlightId
                           where flightDetail.Id == id
                           select new FlightDetailViewModel
                           {
                               Id = flightDetail.Id,
                               FlightName = flight.FlightName,
                               Departure = flightDetail.Departure,
                               Arrival = flightDetail.Arrival,
                               JourneyDate = flightDetail.Departure,
                               FromCity = flightDetail.FromCity,
                               ToCity = flightDetail.ToCity,
                               Price = flightDetail.Price,
                               SeatAvailability = flightDetail.SeatAvailability,
                           }).ToList<FlightDetailViewModel>();
            }

            if (!flights.Any())
            {
                return NotFound();
            }

            return Ok(flights);
        }

        public IHttpActionResult GetFlightDetailsByFlightId(int id)
        {
            IList<FlightDetailViewModel> flights = null;

            using (var ctx = new BookingFlightEntities())
            {
                flights = (from flight in ctx.Flights
                           join flightDetail in ctx.FlightDetails
                           on flight.Id equals flightDetail.FlightId
                           where flightDetail.FlightId == id
                           select new FlightDetailViewModel
                           {
                               Id = flightDetail.Id,
                               FlightName = flight.FlightName,
                               Departure = flightDetail.Departure,
                               Arrival = flightDetail.Arrival,
                               JourneyDate = flightDetail.Departure,
                               FromCity = flightDetail.FromCity,
                               ToCity = flightDetail.ToCity,
                               Price = flightDetail.Price,
                               SeatAvailability = flightDetail.SeatAvailability,
                           }).ToList<FlightDetailViewModel>();
            }

            if (!flights.Any())
            {
                return NotFound();
            }

            return Ok(flights);
        }

        public IHttpActionResult PostFlightDetail(FlightDetail flight)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new BookingFlightEntities())
            {
                var flights = new FlightDetail()
                {
                    FromCity = flight.FromCity,
                    ToCity = flight.ToCity,
                    Departure = flight.Departure,
                    Arrival = flight.Arrival,
                    Price = flight.Price,
                    SeatAvailability = flight.SeatAvailability,
                    FlightId = flight.FlightId
                };
                ctx.FlightDetails.Add(flights);

                ctx.SaveChanges();
                
            }
            return Ok();
        }
    }
}
