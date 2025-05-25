import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCar } from "@awesome.me/kit-7f69c3900d/icons/sharp/thin";
import Search from "@/app/nav/Search";

export default function Navbar() {
  return (
    <header
      className={`sticky top-0 z-50 flex items-center justify-between bg-white p-5 text-gray-800 shadow-md`}
    >
      <div
        className={`flex items-center gap-2 text-3xl font-semibold text-red-500`}
      >
        <FontAwesomeIcon icon={faCar} size="lg" />
        <div>Carsties Auctions</div>
      </div>
      <Search />
      <div>Login</div>
    </header>
  );
}
