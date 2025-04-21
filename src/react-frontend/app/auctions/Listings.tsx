"use client";

import React, { useEffect, useState } from "react";
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/_components/AppPagination";
import { getData } from "@/app/actions/auctionSearchActions";
import { Auction } from "@/app/auctions/Auction";

export default function Listings() {
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const result = await getData(pageNumber);

        if (result.error) {
          setError(result.error);
        } else {
          setAuctions(result.data);
          setPageCount(result.totalPages);
        }
      } catch (err) {
        setError(
          err instanceof Error
            ? err.message
            : "Failed to load auctions. Please try again later.",
        );
        setAuctions([]);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [pageNumber]);

  const handlePageChange = (page: number) => {
    setPageNumber(page);
  };

  return (
    <>
      {loading ? (
        <div className="my-10 flex justify-center">
          <div className="h-12 w-12 animate-spin rounded-full border-t-2 border-b-2 border-blue-500"></div>
        </div>
      ) : error ? (
        <div className="my-10 text-center text-red-500">
          <h3 className="text-lg font-semibold">{error}</h3>
          <button
            onClick={() => fetchData()}
            className="mt-4 rounded bg-blue-500 px-4 py-2 text-white hover:bg-blue-600"
          >
            Retry
          </button>
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

  function fetchData() {
    setLoading(true);
    setError(null);
    getData(pageNumber)
      .then((result) => {
        if (result.error) {
          setError(result.error);
        } else {
          setAuctions(result.data);
          setPageCount(result.totalPages);
        }
      })
      .catch((err) => {
        console.error("Error loading auctions:", err);
        setError("Failed to load auctions. Please try again later.");
        setAuctions([]);
      })
      .finally(() => {
        setLoading(false);
      });
  }
}
