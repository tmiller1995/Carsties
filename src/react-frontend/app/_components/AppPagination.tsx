"use client";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowLeft,
  faArrowRight,
} from "@awesome.me/kit-7f69c3900d/icons/sharp/thin";
import { PaginatedResult } from "@/app/_types/PaginatedResult";
import { Auction } from "@/app/auctions/Auction";
import { useRouter, usePathname, useSearchParams } from "next/navigation";
import { useCallback } from "react";

interface Props {
  data: PaginatedResult<Auction>;
  onPageChange?: (pageNumber: number) => void;
  onPageSizeChange?: (pageSize: number) => void;
}

export default function AppPagination({
  data,
  onPageChange,
  onPageSizeChange,
}: Props) {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();
  const {
    pageNumber,
    pageSize,
    hasNextPage,
    hasPreviousPage,
    totalCount,
    totalPages,
    data: items,
  } = data;

  // Calculate range being viewed
  const startIndex = (pageNumber - 1) * pageSize + 1;
  const endIndex = Math.min(startIndex + pageSize - 1, totalCount);

  // Create a new URLSearchParams instance from current params
  const createQueryString = useCallback(
    (params: { [key: string]: string }) => {
      const newParams = new URLSearchParams(searchParams.toString());

      Object.entries(params).forEach(([name, value]) => {
        newParams.set(name, value);
      });

      return newParams.toString();
    },
    [searchParams],
  );

  // Handle page navigation
  const handlePageChange = (page: number) => {
    if (onPageChange) {
      onPageChange(page);
    } else {
      router.push(
        `${pathname}?${createQueryString({ pageNumber: page.toString() })}`,
      );
    }
  };

  // Handle page size change
  const handlePageSizeChange = (newSize: number) => {
    if (onPageSizeChange) {
      onPageSizeChange(newSize);
    } else {
      // When changing page size, go back to page 1
      router.push(
        `${pathname}?${createQueryString({
          pageSize: newSize.toString(),
          pageNumber: "1",
        })}`,
      );
    }
  };

  // Generate array of page numbers to show
  const getPageNumbers = () => {
    const pages: (number | string)[] = [];

    // Always show first page
    pages.push(1);

    // Calculate range of pages to show around current page
    const rangeStart = Math.max(2, pageNumber - 1);
    const rangeEnd = Math.min(totalPages - 1, pageNumber + 1);

    // Add ellipsis if there's a gap after page 1
    if (rangeStart > 2) {
      pages.push("...");
    }

    // Add pages in the range
    for (let i = rangeStart; i <= rangeEnd; i++) {
      pages.push(i);
    }

    // Add ellipsis if there's a gap before the last page
    if (rangeEnd < totalPages - 1) {
      pages.push("...");
    }

    // Always show last page if we have more than 1 page
    if (totalPages > 1) {
      pages.push(totalPages);
    }

    return pages;
  };

  // Don't show pagination if there's only one page
  if (totalPages <= 1) return null;

  return (
    <div>
      {/* Page size selector and range display */}
      <div className="mb-4 flex items-center justify-between text-sm text-gray-500">
        <div className="flex items-center">
          <span>Show</span>
          <select
            className="mx-2 rounded border p-1"
            value={pageSize}
            onChange={(e) => handlePageSizeChange(Number(e.target.value))}
          >
            {[5, 10, 20, 50].map((size) => (
              <option key={size} value={size}>
                {size}
              </option>
            ))}
          </select>
          <span>per page</span>
        </div>

        <div>
          Showing {startIndex}-{endIndex} of {totalCount} items
        </div>
      </div>

      {/* Pagination navigation */}
      <nav className="flex items-center justify-between border-t border-gray-200 px-4 sm:px-0">
        <div className="-mt-px flex w-0 flex-1">
          <button
            onClick={() => handlePageChange(pageNumber - 1)}
            disabled={!hasPreviousPage}
            className={`inline-flex items-center border-t-2 border-transparent pt-4 pr-1 text-sm font-medium ${
              hasPreviousPage
                ? "text-gray-500 hover:border-gray-300 hover:text-gray-700"
                : "cursor-not-allowed text-gray-300"
            }`}
          >
            <FontAwesomeIcon
              icon={faArrowLeft}
              aria-hidden="true"
              className="mr-3 size-5 text-gray-400"
            />
            Previous
          </button>
        </div>
        <div className="hidden md:-mt-px md:flex">
          {getPageNumbers().map((page, index) =>
            typeof page === "number" ? (
              <button
                key={index}
                onClick={() => handlePageChange(page)}
                aria-current={pageNumber === page ? "page" : undefined}
                className={`inline-flex items-center border-t-2 px-4 pt-4 text-sm font-medium ${
                  pageNumber === page
                    ? "border-indigo-500 text-indigo-600"
                    : "border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700"
                }`}
              >
                {page}
              </button>
            ) : (
              <span
                key={index}
                className="inline-flex items-center border-t-2 border-transparent px-4 pt-4 text-sm font-medium text-gray-500"
              >
                {page}
              </span>
            ),
          )}
        </div>
        <div className="-mt-px flex w-0 flex-1 justify-end">
          <button
            onClick={() => handlePageChange(pageNumber + 1)}
            disabled={!hasNextPage}
            className={`inline-flex items-center border-t-2 border-transparent pt-4 pl-1 text-sm font-medium ${
              hasNextPage
                ? "text-gray-500 hover:border-gray-300 hover:text-gray-700"
                : "cursor-not-allowed text-gray-300"
            }`}
          >
            Next
            <FontAwesomeIcon
              icon={faArrowRight}
              aria-hidden="true"
              className="ml-3 size-5 text-gray-400"
            />
          </button>
        </div>
      </nav>
    </div>
  );
}
