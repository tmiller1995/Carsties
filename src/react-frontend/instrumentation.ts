import { registerOTel } from "@vercel/otel";

export function register() {
  registerOTel({
    serviceName: process.env.OTEL_SERVICE_NAME || "frontend",
  });
  console.log(
    `OpenTelemetry registered for service: ${process.env.OTEL_SERVICE_NAME || "frontend"} using @vercel/otel`,
  );
  console.log(
    `Expecting OTLP Endpoint: ${process.env.OTEL_EXPORTER_OTLP_ENDPOINT}`,
  );
}
