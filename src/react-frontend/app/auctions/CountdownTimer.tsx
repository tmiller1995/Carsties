"use client";

import Countdown, { zeroPad } from "react-countdown";
import clsx from "clsx";

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
      className={clsx(
        "flex justify-center rounded-lg border-2 border-white px-2 py-1 text-white",
        {
          "bg-red-600": completed,
          "bg-amber-600": days == 0 && hours < 10,
          "bg-green-600": days > 0 || hours >= 10,
        },
      )}
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

export type CountdownTimerProps = {
  auctionEnd: string;
};

export default function CountdownTimer({ auctionEnd }: CountdownTimerProps) {
  return (
    <div>
      <Countdown date={auctionEnd} renderer={renderer} />
    </div>
  );
}
