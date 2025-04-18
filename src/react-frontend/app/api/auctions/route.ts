import { NextRequest, NextResponse } from "next/server";

export async function GET(request: NextRequest) {
  try {
    const searchParams = request.nextUrl.searchParams;
    const pageNumberParam = searchParams.get("pageNumber") || "1";
    const pageSizeParam = searchParams.get("pageSize") || "10";

    const validPageNumber = Math.max(1, parseInt(pageNumberParam));
    const validPageSize = parseInt(pageSizeParam);

    const response = await fetch(
      `http://localhost:5050/search?pageNumber=${validPageNumber}&pageSize=${validPageSize}`,
      {
        method: "GET",
        signal: AbortSignal.timeout(10000),
        headers: {
          "Content-Type": "application/json",
        },
      },
    );

    if (!response.ok) {
      throw new Error(`Error fetching auctions: ${response.status}`);
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error) {
    console.error("Error fetching auctions:", error);
    return NextResponse.json(
      { error: error instanceof Error ? error.message : "Unknown error" },
      { status: 500 },
    );
  }
}
