"use client";

import React, { useEffect, useState } from "react";
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/_components/AppPagination";
import { PaginatedResult } from "@/app/_types/PaginatedResult";
import { AuctionsApiResponse } from "@/app/auctions/AuctionsApiResponse";
import { Auction } from "@/app/auctions/Auction";

export default function Listings() {
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await fetch(
          `/api/auctions?pageNumber=${pageNumber}&pageSize=10`,
        );

        if (!response.ok) {
          throw new Error(`Error fetching auctions: ${response.status}`);
        }

        const result: PaginatedResult<AuctionsApiResponse> =
          await response.json();
        setAuctions(result.data.auctions);
        setPageCount(result.totalPages);
      } catch (error) {
        console.error("Error loading auctions:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [pageNumber]);

  const handlePageChange = (page: number) => {
    setPageNumber(page);
    window.scrollTo(0, 0);
  };

  return (
    <>
      {loading ? (
        <div className="my-10 flex justify-center">
          <div className="h-12 w-12 animate-spin rounded-full border-t-2 border-b-2 border-blue-500"></div>
        </div>
      ) : auctions.length === 0 ? (
        <div className="my-10 text-center">
          <h3 className="text-lg font-semibold">No auctions found</h3>
        </div>
      ) : (
        <div className={"grid grid-cols-4 gap-6"}>
          {auctions.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
        </div>
      )}

      {pageCount > 0 && (
        <div className={"mt-5 flex justify-center"}>
          <AppPagination
            totalPages={pageCount}
            currentPage={pageNumber}
            onPageChanged={handlePageChange}
          />
        </div>
      )}
    </>
  );
}
