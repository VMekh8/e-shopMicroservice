namespace BuildingBlock.Pagination;

public record PaginatedRequest(int PageSize = 10, int PageIndex = 1);