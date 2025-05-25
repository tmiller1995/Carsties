import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@awesome.me/kit-7f69c3900d/icons/sharp/thin";

export default function Search() {
  return (
    <div className="flex w-[50%] items-center rounded-full border-2 border-gray-300 shadow-sm">
      <input
        type="text"
        placeholder="Search for cars by make, model, or color"
        className="flex-grow border-transparent bg-transparent pl-5 text-sm text-gray-600 focus:border-transparent focus:ring-0 focus:outline-none"
      />
      <button>
        <FontAwesomeIcon
          icon={faSearch}
          size="lg"
          className="mx-2 cursor-pointer rounded-full bg-red-400 p-2 text-white"
        />
      </button>
    </div>
  );
}
