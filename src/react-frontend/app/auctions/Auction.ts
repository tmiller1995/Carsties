export type Auction = {
    id: string,
    createdAt: Date,
    updatedAt: Date,
    auctionEnd: Date,
    seller: string,
    winner: string,
    make: string,
    model: string,
    year: number,
    color: string,
    mileage: number,
    imageUrl: string,
    status: string,
    reservePrice: number,
    soldAmount: number,
    currentHighBid: string
}