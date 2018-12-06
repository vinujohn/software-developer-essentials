using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        private ICollection<Auction> _auctions;

        public AuctionRepository(){
            _auctions =  new List<Auction>();
        }

        public void Add(Auction auction)
        {
            _auctions.Add(auction);
        }

        public Auction FindAuctionById(string auctionId) {
            return _auctions.FirstOrDefault(x => x.Id == auctionId);
        }
    }
}
