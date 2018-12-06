using Service;
using Repository;
using Xunit;
using System;

namespace Tests
{
    public class AuctionServiceTests{
        string testUserName =  "jsmith";

        [Fact]
        public void Test_Auction_Created_Successfully(){
            var repo = new AuctionRepository();
            var sut = new AuctionService(repo);
            var auctionId = sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(2));
            var auction = repo.FindAuctionById(auctionId);
            Assert.NotNull(auction);
            Assert.Equal(testUserName, auction.Seller.UserName);
            Assert.Equal(auctionId, auction.Id);
        }


        [Fact]
        public void Test_Auction_Cannot_Be_Createded_Invalid_StartDate(){
            var repo = new AuctionRepository();
            var sut = new AuctionService(repo);
            sut.CreateAuction(testUserName, DateTime.UtcNow.AddDays(-2));
        }
    }
}
