"use server";

import { AuctionDataResult } from "@/app/actions/AuctionActionResult";

export async function getData(
  pageNumber: number = 1,
  pageSize: number = 10,
): Promise<AuctionDataResult> {
  try {
    const validPageNumber = Math.max(1, pageNumber);
    const validPageSize = Math.max(1, pageSize);

    const baseApiUrl =
      process.env.NEXT_PUBLIC_API_URL || "http://localhost:5050";
    const url = new URL(`${baseApiUrl}/search`);
    url.searchParams.set("pageNumber", validPageNumber.toString());
    url.searchParams.set("pageSize", validPageSize.toString());

    console.log(`Fetching data from: ${url}`);

    const response = await fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      // Disable caching
      cache: "no-store",
    });

    if (!response.ok) {
      throw new Error(`API error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return {
      data: data.data?.searchListResponses || [],
      totalPages: data.totalPages || 0,
      pageNumber: data.pageNumber || validPageNumber,
    };
  } catch (error) {
    console.error("Error fetching data:", error);

    // Return empty result with error info
    return {
      data: [],
      totalPages: 0,
      pageNumber: pageNumber,
      error: error instanceof Error ? error.message : "Unknown error",
    };
  }
}
