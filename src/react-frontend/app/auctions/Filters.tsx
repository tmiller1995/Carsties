import { Button, ButtonGroup } from "flowbite-react";
import { useParamsStore } from "@/app/hooks/useParamsStore";

const pageSizeButtons = [4, 8, 12, 16];

export default function Filters() {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);

  return (
    <div className="mb-4 flex items-center justify-between">
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
