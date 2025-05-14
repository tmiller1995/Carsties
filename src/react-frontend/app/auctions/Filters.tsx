import { Button, ButtonGroup } from "flowbite-react";

export type FilterProps = {
  pageSize: number;
  setPageSize: (pageSize: number) => void;
};

const pageSizeButtons = [4, 8, 12, 16];

export default function Filters({ pageSize, setPageSize }: FilterProps) {
  return (
    <div className="mb-4 flex items-center justify-between">
      <div>
        <span className="mr-2 text-sm text-gray-500 uppercase">Page Size</span>
        <ButtonGroup>
          {pageSizeButtons.map((size, index) => (
            <Button
              key={index}
              onClick={() => setPageSize(size)}
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
