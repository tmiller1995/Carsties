"use client";

import { Pagination } from "flowbite-react";

export type AppPaginationProps = {
  currentPage: number;
  totalPages: number;
  onPageChanged: (pageNumber: number) => void;
};

export default function AppPagination({
  currentPage,
  totalPages,
  onPageChanged,
}: AppPaginationProps) {
  // Ensure we have valid values to avoid rendering issues
  const validTotalPages = Math.max(1, totalPages);
  const validCurrentPage = Math.min(Math.max(1, currentPage), validTotalPages);

  return (
    <Pagination
      currentPage={validCurrentPage}
      onPageChange={onPageChanged}
      totalPages={validTotalPages}
      layout={"pagination"}
      showIcons={true}
      className={"mb-5 text-blue-500"}
    />
  );
}
