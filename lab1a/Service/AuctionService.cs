using System;
using Repository;
using Models;

namespace Service
{
    public class AuctionService
    {
        private readonly IAuctionRepository _repository;
        private readonly IUsersRepository _usersRepository;

        public AuctionService(IAuctionRepository repository, IUsersRepository usersRepository){
            _repository = repository;
            _usersRepository = usersRepository;
        }

        public string CreateAuction(string sellerUserName, DateTime auctionStartDateTime){
            if (auctionStartDateTime <= DateTime.UtcNow){
                throw new ArgumentException("start date must be in the future");
            }

            var foundUser = _usersRepository.FindUserByUserName(sellerUserName);
            if (foundUser == null || !foundUser.IsLoggedIn) {
                throw new Exception("user not found or not logged in");
            }

            var auction = new Auction{
                Id = Guid.NewGuid().ToString(),
                Seller = new User(){UserName = sellerUserName},
                StartTime = auctionStartDateTime,
            };

            _repository.Add(auction);

            return auction.Id;
        }

        public void Start(string auctionId){
            var auction = _repository.FindAuctionById(auctionId);
            if (auction == null){
                throw new ArgumentException("could not find auction");
            }
            auction.StartedOn = DateTime.UtcNow;
            _repository.Save(auction);
        }

        public void Stop(string auctionId){
            var auction = _repository.FindAuctionById(auctionId);
            if (auction == null){
                throw new ArgumentException("could not find auction");
            }
            auction.EndedOn = DateTime.UtcNow;
            _repository.Save(auction);
        public bool CreateBid(string auctionID, double bidAmount, string bidderUserName){
            // auction must be started
            // if 1st can be equal or greater than price
            // if not 1st must be higher then currentHighestBid
            var selectedAuction = _repository.FindAuctionById(auctionID);
            if(DateTime.UtcNow < selectedAuction.StartTime){
                return false;
            }
            if(bidAmount < selectedAuction.HighestBid.amount){
                return false;
            }
            


            return true;
        }
    }
}
