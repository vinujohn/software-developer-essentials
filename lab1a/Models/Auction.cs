using System;

namespace Models
{
    public class Auction{
        public string Id { get; set; }
        public User Seller { get; set; }
        public DateTime StartTime { get; set; }
    }
}