"use client";

import Countdown, { zeroPad } from "react-countdown";

type Props = {
  auctionEnd: string;
};

const renderer = ({
  days,
  hours,
  minutes,
  seconds,
  completed,
}: {
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  completed: boolean;
}) => {
  return (
    <div
      className={`flex justify-center rounded-lg border-2 border-white px-2 py-1 text-white ${completed ? "bg-red-600" : days == 0 && hours < 10 ? "bg-amber-600" : "bg-green-600"}`}
    >
      {completed ? (
        <span>Auction Finished</span>
      ) : (
        <span suppressHydrationWarning={true}>
          {zeroPad(days)} : {zeroPad(hours)} : {zeroPad(minutes)} :{" "}
          {zeroPad(seconds)}{" "}
        </span>
      )}
    </div>
  );
};

export default function CountdownTimer({
  auctionEnd,
}: Props & { auctionEnd: string }) {
  return (
    <div>
      <Countdown date={auctionEnd} renderer={renderer} />
    </div>
  );
}
