import { Button, ButtonGroup } from "flowbite-react";
import { useParamsStore } from "@/app/hooks/useParamsStore";
import {
  faArrowDownAZ,
  faClock,
  faCircleStop,
} from "@awesome.me/kit-7f69c3900d/icons/sharp/solid";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const pageSizeButtons = [4, 8, 12, 16];
const orderButtons = [
  { label: "A-Z", icon: faArrowDownAZ, value: "make" },
  { label: "End Date", icon: faClock, value: "endingsoon" },
  { label: "Recently Added", icon: faCircleStop, value: "new" },
];

export default function Filters() {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);
  const orderBy = useParamsStore((state) => state.orderBy);

  return (
    <div className="mb-4 flex items-center justify-between">
      <div>
        <span className="mr-2 text-sm text-gray-500 uppercase">Order By</span>
        <ButtonGroup>
          {orderButtons.map(({ label, icon: Icon, value }) => (
            <Button
              key={value}
              onClick={() => setParams({ orderBy: value })}
              color={`${orderBy === value ? "red" : "gray"}`}
              className="focus:ring-0"
            >
              <FontAwesomeIcon icon={Icon} className="mr-3 h-4 w-4" />
              {label}
            </Button>
          ))}
        </ButtonGroup>
      </div>
      <div>
        <span className="mr-2 text-sm text-gray-500 uppercase">Page Size</span>
        <ButtonGroup>
          {pageSizeButtons.map((size, index) => (
            <Button
              key={index}
              onClick={() => setParams({ pageSize: size })}
              color={`${pageSize === size ? "red" : "gray"}`}
            >
              {size}
            </Button>
          ))}
        </ButtonGroup>
      </div>
    </div>
  );
}
