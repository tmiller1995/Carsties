import { Auction } from "@/app/auctions/Auction";

export type AuctionDataResult = {
  data: Auction[];
  totalPages: number;
  pageNumber: number;
  error?: string;
};
