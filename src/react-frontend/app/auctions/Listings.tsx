"use client";

import React, { useEffect, useState } from "react";
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/_components/AppPagination";
import { getData } from "@/app/actions/auctionSearchActions";
import Filters from "@/app/auctions/Filters";
import { useParamsStore } from "@/app/hooks/useParamsStore";
import { useShallow } from "zustand/react/shallow";
import qs from "query-string";
import { AuctionDataResult } from "@/app/actions/AuctionActionResult";

export default function Listings() {
  const [data, setData] = useState<AuctionDataResult>();
  const params = useParamsStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      pageCount: state.pageCount,
      searchTerm: state.searchTerm,
    })),
  );
  const setParams = useParamsStore((state) => state.setParams);
  const url = qs.stringifyUrl({ url: "", query: params });

  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }

  useEffect(() => {
    getData(url).then((result) => {
      setData(result);
    });
  }, [url]);

  return (
    <>
      <Filters />
      <div className="grid grid-cols-4 gap-6">
        {data?.data.map((auction) => (
          <AuctionCard auction={auction} key={auction.id} />
        ))}
      </div>
      <div className="mt-5 flex justify-center">
        <AppPagination
          totalPages={data?.totalPages || 0}
          currentPage={params.pageNumber}
          onPageChanged={setPageNumber}
        />
      </div>
    </>
  );
}
