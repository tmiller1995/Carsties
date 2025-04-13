"use client";

import { Pagination } from "flowbite-react";
import { PaginatedResult } from "@/app/_types/PaginatedResult";
import { Auction } from "@/app/auctions/Auction";
import { useState } from "react";

export type AppPaginationProps = {
  paginatedResult: PaginatedResult<Auction>;
};

export default function AppPagination({ paginatedResult }: AppPaginationProps) {
  const [pageNumber, setPageNumber] = useState(paginatedResult.pageNumber);

  return (
    <Pagination
      currentPage={pageNumber}
      onPageChange={(e: number) => setPageNumber(e)}
      totalPages={paginatedResult.totalPages}
      layout={"pagination"}
      showIcons={true}
      className={"mb-5 text-blue-500"}
    />
  );
}
