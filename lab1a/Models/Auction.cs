using System;

namespace Models
{
    public class Auction{
        public string Id { get; set; }
        public User Seller { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime? StartedOn {get; set;}
        public DateTime? EndedOn {get; set;}
        public Bid HighestBid { get; set; }
        public bool HasStarted { get; set; }
        public bool HasEnded { get; set; }
    }
}
