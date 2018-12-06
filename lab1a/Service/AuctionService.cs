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
    }
}
