export function normalizeImagePath(imagePath: string): string {
  // Replace all backslashes with forward slashes
  let normalizedPath = imagePath.replace(/\\/g, "/");

  // Ensure the path starts with a forward slash
  if (!normalizedPath.startsWith("/")) {
    normalizedPath = "/" + normalizedPath;
  }

  return normalizedPath;
}
