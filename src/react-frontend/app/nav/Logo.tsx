"use client";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCar } from "@awesome.me/kit-7f69c3900d/icons/sharp/solid";
import React from "react";
import { useParamsStore } from "@/app/hooks/useParamsStore";

export default function Logo() {
  const reset = useParamsStore((state) => state.reset);

  return (
    <div
      onClick={reset}
      className="flex cursor-pointer items-center gap-2 text-3xl font-semibold text-red-500"
    >
      <FontAwesomeIcon icon={faCar} size="lg" />
      <div>Carsties Auctions</div>
    </div>
  );
}
