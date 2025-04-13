"use client";

import React, { useEffect, useState } from "react";
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/_components/AppPagination";
import { getData } from "@/app/actions/auctionActions";
import { Auction } from "@/app/auctions/Auction";
import { PaginatedResult } from "@/app/_types/PaginatedResult";

export default function Listings() {
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);

  useEffect(() => {
    getData(pageNumber).then((result: PaginatedResult<Auction>) => {
      setAuctions(result.data);
      setPageCount(result.totalPages);
    });
  }, [pageNumber]);

  if (auctions.length === 0) return <h3>Loading...</h3>;

  return (
    <>
      <div className={"grid grid-cols-4 gap-6"}>
        {auctions &&
          auctions.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
      </div>
      <div className={"mt-5 flex justify-center"}>
        <AppPagination
          totalPages={pageCount}
          currentPage={pageNumber}
          onPageChanged={setPageNumber}
        />
      </div>
    </>
  );
}
