import React from "react";
import CountdownTimer from "@/app/auctions/CountdownTimer";
import { AuctionProps } from "@/app/auctions/AuctionProps";
import CarImage from "@/app/auctions/CarImage";

export default function AuctionCard({ auction }: AuctionProps) {
  return (
    <a href={`#`} className={`group`}>
      <div
        className={`relative aspect-[16/10] w-full overflow-hidden rounded-lg bg-gray-200`}
      >
        <CarImage auction={auction} />
        <div className={`absolute bottom-2 left-2`}>
          <CountdownTimer auctionEnd={auction.auctionEnd} />
        </div>
      </div>
      <div className={`mt-4 flex items-center justify-between`}>
        <h3 className={`text-gray-700`}>
          {auction.make} {auction.model}
        </h3>
        <p className={`text-sm font-semibold`}>{auction.year}</p>
      </div>
    </a>
  );
}
