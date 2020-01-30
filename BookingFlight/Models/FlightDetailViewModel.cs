using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingFlight.Models
{
    public class FlightDetailViewModel
    {
        public int Id { get; set; }
        public string FlightName { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime JourneyDate { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public decimal? Price { get; set; }
        public int? SeatAvailability { get; set; }
    }

    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "MM-dd-yyyy";
        }
    }
}