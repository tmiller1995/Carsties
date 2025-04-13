"use server";

import { PaginatedResult } from "@/app/_types/PaginatedResult";
import { Auction } from "@/app/auctions/Auction";

export async function getData(
  pageNumber: number = 1,
  pageSize: number = 10,
): Promise<PaginatedResult<Auction>> {
  const url = new URL("http://localhost:5050/search");
  url.searchParams.append("pageNumber", pageNumber.toString());
  url.searchParams.append("pageSize", pageSize.toString());

  const response = await fetch(url);
  if (!response.ok) throw new Error("Failed to fetch data");
  return response.json();
}
