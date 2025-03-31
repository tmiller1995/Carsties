"use client";

import Image from "next/image";
import React, { useState } from "react";
import { AuctionProps } from "@/app/auctions/AuctionProps";

export default function CarImage({ auction }: AuctionProps) {
  const [isLoading, setIsLoading] = useState(true);
  return (
    <Image
      src={auction.imageUrl}
      alt={`Image of ${auction.make} ${auction.model} in ${auction.color}`}
      fill={true}
      sizes={`(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 25vw)`}
      priority={true}
      className={`object-cover duration-700 ease-in-out group-hover:opacity-75 ${
        isLoading
          ? "scale-110 blur-2xl grayscale"
          : "blur-0 scale-100 grayscale-0"
      } `}
      onLoad={() => setIsLoading(false)}
    />
  );
}
