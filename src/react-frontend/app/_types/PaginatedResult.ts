export type PaginatedResult<T> = {
  data: T;
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  readonly totalPages: number; // Computed on the server as Math.Ceiling(totalCount / pageSize)
};
