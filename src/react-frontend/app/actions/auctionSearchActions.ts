"use server";

import { AuctionDataResult } from "@/app/actions/AuctionActionResult";

export async function getData(query: string): Promise<AuctionDataResult> {
  try {
    const baseApiUrl = process.env.GATEWAY_BASE_URL || "http://localhost:5050";

    const response = await fetch(`${baseApiUrl}/search${query}`);

    if (!response.ok) {
      throw new Error(`API error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return {
      data: data.data?.searchListResponses || [],
      totalPages: data.totalPages || 0,
      pageNumber: data.pageNumber,
    };
  } catch (error) {
    return {
      data: [],
      totalPages: 0,
      pageNumber: 0,
      error: error instanceof Error ? error.message : "Unknown error",
    };
  }
}
