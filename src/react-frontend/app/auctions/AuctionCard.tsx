import React from 'react';
import Image from "next/image";
import {Auction} from "@/app/auctions/Auction";

type Props = {
    auction: Auction
}

export default function AuctionCard({auction}: Props) {
    return (
        <a href={`#`}>
            <div className={`relative w-full bg-gray-200 aspect-video rounded-lg overflow-hidden`}>
                <Image src={auction.imageUrl}
                       alt={`Image of ${auction.make} ${auction.model} in ${auction.color}`}
                       width={150}
                       height={150}
                       className={`object-cover`} />
            </div>
        </a>
    )
}