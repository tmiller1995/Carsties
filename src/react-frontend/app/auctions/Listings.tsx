import React from "react";
import AuctionCard from "@/app/auctions/AuctionCard";
import { Auction } from "@/app/auctions/Auction";
import { PaginatedResult } from "@/app/_types/PaginatedResult";
import AppPagination from "@/app/_components/AppPagination";

async function getData(): Promise<PaginatedResult<Auction>> {
  const response = await fetch("http://localhost:5050/search?pageSize=4");
  if (!response.ok) throw new Error("Failed to fetch data");
  return response.json();
}

export default async function Listings() {
  const paginatedAuctionResult = await getData();

  return (
    <>
      <div className={`grid grid-cols-4 gap-6`}>
        {paginatedAuctionResult.data &&
          paginatedAuctionResult.data.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
      </div>
      <div className={"mt-5 flex justify-center"}>
        <AppPagination paginatedResult={paginatedAuctionResult} />
      </div>
    </>
  );
}
