using System;
namespace Models
{
    public class Bid
    {
        public double amount { get; set; }
        public string bidder { get; set; }
        public User User { get; set; }

    }
}
