using Service;
using Repository;
using Xunit;
using System;
using Models;

namespace Tests
{
    public class AuctionServiceTests{
        string testUserName =  "jsmith";

        [Fact]
        public void Test_Auction_Created_Successfully(){
            var repo = new AuctionRepository();
            var userRepo = new UsersRepository();
            var user = new User(){UserName = testUserName};
            userRepo.Add(user);
            userRepo.SetLogin(user, true);
            var sut = new AuctionService(repo, userRepo);
            var auctionId = sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(2));
            var auction = repo.FindAuctionById(auctionId);
            Assert.NotNull(auction);
            Assert.Equal(testUserName, auction.Seller.UserName);
            Assert.Equal(auctionId, auction.Id);
        }


        [Fact]
        public void Test_Auction_Cannot_Be_Created_Invalid_StartDate(){
            var repo = new AuctionRepository();
            var sut = new AuctionService(repo, null);
            Assert.Throws<ArgumentException>(()=>sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(-2)));
        }

        [Fact]
        public void Test_Auction_Cannot_Be_Created_User_Not_Authenticated(){
            var repo = new AuctionRepository();
            var userRepo = new UsersRepository();
            var sut = new AuctionService(repo, userRepo);
            Assert.Throws<Exception>(()=>sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(2)));
        }


        [Fact]
        public void Test_Auction_Can_Be_Start(){
            var repo = new AuctionRepository();
            var sut = new AuctionService(repo);
            sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(-2));
        }
    }
}
