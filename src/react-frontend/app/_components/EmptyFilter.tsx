import { useParamsStore } from "@/app/hooks/useParamsStore";
import Heading from "@/app/_components/Heading";
import { Button } from "flowbite-react";

type EmptyFilterProps = {
  title?: string;
  subtitle?: string;
  showReset?: boolean;
};

export default function EmptyFilter({
  title = "No matches for this Filter",
  subtitle = "Try changing the filter or search term",
  showReset,
}: EmptyFilterProps) {
  const reset = useParamsStore((state) => state.reset);

  return (
    <div className="flex h-[40vh] flex-col items-center justify-center gap-2 shadow-lg">
      <Heading title={title} subtitle={subtitle} center={true} />
      <div className="mt-4">
        {showReset && (
          <Button outline onClick={reset}>
            Remove Filters
          </Button>
        )}
      </div>
    </div>
  );
}
