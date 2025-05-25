"use client";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@awesome.me/kit-7f69c3900d/icons/sharp/solid";
import { useParamsStore } from "@/app/hooks/useParamsStore";
import { useState, ChangeEvent } from "react";

export default function Search() {
  const setParams = useParamsStore((state) => state.setParams);
  const [value, setValue] = useState("");

  function handleChange(event: ChangeEvent<HTMLInputElement>) {
    setValue(event.target.value);
    setParams({ searchTerm: event.target.value });
  }

  function handleSearch() {
    setParams({ searchTerm: value });
  }

  return (
    <div className="flex w-[50%] items-center rounded-full border-2 border-gray-300 shadow-sm">
      <input
        onKeyPress={(e) => {
          if (e.key === "Enter") {
            handleSearch();
          }
        }}
        onChange={handleChange}
        value={value}
        type="text"
        placeholder="Search for cars by make, model, or color"
        className="flex-grow border-transparent bg-transparent pl-5 text-sm text-gray-600 focus:border-transparent focus:ring-0 focus:outline-none"
      />
      <button onClick={handleSearch}>
        <FontAwesomeIcon
          icon={faSearch}
          size="lg"
          className="mx-2 cursor-pointer rounded-full bg-red-500 p-2 text-white"
        />
      </button>
    </div>
  );
}
