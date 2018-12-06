using System;
using Repository;
using Models;

namespace Service
{
    public class AuctionService
    {
        private readonly IAuctionRepository _repository;

        public AuctionService(IAuctionRepository repository){
            _repository = repository;
        }

        public string CreateAuction(string sellerUserName, DateTime auctionStartDateTime){
            
            
            var auction = new Auction{
                Id = Guid.NewGuid().ToString(),
                Seller = new User(){UserName = sellerUserName},
                StartTime = auctionStartDateTime,
            };

            _repository.Add(auction);

            return auction.Id;
        }
    }
}
