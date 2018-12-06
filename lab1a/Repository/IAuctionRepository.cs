using Models;

namespace Repository
{
    public interface IAuctionRepository{
        void Add(Auction auction);
        Auction FindAuctionById(string auctionId);
    }
}
