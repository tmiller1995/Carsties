type HeadingProps = {
  title: string;
  subtitle?: string;
  center?: boolean;
};

export default function Heading({ title, subtitle, center }: HeadingProps) {
  return (
    <div className={center ? "text-center" : "text-start"}>
      <h1 className="2xl font-bold">{title}</h1>
      {subtitle && (
        <p className="mt-2 font-light text-neutral-500">{subtitle}</p>
      )}
    </div>
  );
}
