import React from "react";
import Search from "@/app/nav/Search";
import Logo from "@/app/nav/Logo";
import LoginButton from "@/app/nav/LoginButton";

export default function Navbar() {
  return (
    <header className="sticky top-0 z-50 flex items-center justify-between bg-white p-5 text-gray-800 shadow-md">
      <Logo />
      <Search />
      <LoginButton />
    </header>
  );
}
