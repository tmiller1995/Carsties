"use client";

import { Pagination } from "flowbite-react";

export interface AppPaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChanged: (pageNumber: number) => void;
}

export default function AppPagination({
  currentPage,
  totalPages,
  onPageChanged,
}: AppPaginationProps) {
  return (
    <Pagination
      currentPage={currentPage}
      onPageChange={(e: number) => onPageChanged(e)}
      totalPages={totalPages}
      layout={"pagination"}
      showIcons={true}
      className={"mb-5 text-blue-500"}
    />
  );
}
