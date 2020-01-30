using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingFlight.Models
{
    public class TicketDetailViewModel
    {
        public int Id { get; set; }
        public BookingStatusValues BookingStatus { get; set; }
        public int PassengerCount { get; set; }
        public decimal TotalFare { get; set; }
        public decimal CancellationFare { get; set; }
        public int FlightDetailId { get; set; }

        [ForeignKey("FlightDetailId")]
        public FlightDetailViewModel FlightDetail { get; set; }
    }
}