//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingFlight
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTicketHistory
    {
        public int Id { get; set; }
        public int UserLoginId { get; set; }
        public int TicketDetailId { get; set; }
    
        public virtual TicketDetail TicketDetail { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
