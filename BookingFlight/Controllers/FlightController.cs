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
            try
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult PutFlight(Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");
                using (var ctx = new BookingFlightEntities())
                {
                    var flighttobeUpdated = ctx.Flights.Where(x => x.Id == flight.Id).FirstOrDefault<Flight>();
                    if (flighttobeUpdated != null)
                    {
                        flighttobeUpdated.FlightName = flight.FlightName;
                        flighttobeUpdated.TotalSeats = flight.TotalSeats;
                    }

                    ctx.SaveChanges();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult GetFlights()
        {
            try
            {
                IList<FlightViewModel> flights = null;

                using (var ctx = new BookingFlightEntities())
                {
                    flights = (from flight in ctx.Flights
                               select new FlightViewModel
                               {
                                   FlightName = flight.FlightName,
                                   TotalSeats = flight.TotalSeats,
                                   Id = flight.Id,
                                   FlightId = flight.Id,
                                   FlightIdTobeCanceled = flight.Id
                               }).Distinct().ToList<FlightViewModel>();
                }

                if (!flights.Any())
                {
                    return NotFound();
                }

                return Ok(flights);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult GetFlightsById( int Id)
        {
            try
            {
                IList<FlightViewModel> flights = null;

                using (var ctx = new BookingFlightEntities())
                {
                    flights = (from flight in ctx.Flights
                               where flight.Id == Id
                               select new FlightViewModel
                               {
                                   FlightName = flight.FlightName,
                                   TotalSeats = flight.TotalSeats,
                                   Id = flight.Id,
                                   FlightId = flight.Id,
                                   FlightIdTobeCanceled = flight.Id
                               }).Distinct().ToList<FlightViewModel>();
                }

                if (!flights.Any())
                {
                    return NotFound();
                }

                return Ok(flights);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult DeleteFlight(int id)
        {
            try {
                if (id <= 0)
                    return BadRequest("Not a valid flight id");

                using (var ctx = new BookingFlightEntities())
                {
                    var flight = ctx.Flights
                        .Where(s => s.Id == id)
                        .FirstOrDefault();

                    ctx.Entry(flight).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
