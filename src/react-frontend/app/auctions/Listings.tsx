import React from 'react';
import AuctionCard from "@/app/auctions/AuctionCard";
import {Auction} from "@/app/auctions/Auction";
import {PagedResult} from "@/app/_types/PagedResult";

async function getData(): Promise<PagedResult<Auction>> {
    const response = await fetch('http://localhost:5050/search?pageSize=10');
    if (!response.ok) throw new Error('Failed to fetch data');
    return response.json();
}

export default async function Listings() {
    const paginatedAuctionResult = await getData();

    return (
        <div className={`grid grid-cols-4 gap-6`}>
            {paginatedAuctionResult.data && paginatedAuctionResult.data.map((auction) => (
                <AuctionCard auction={auction} key={auction.id}/>
            ))}
        </div>
    );
}