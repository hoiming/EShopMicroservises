

using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PationationRequest) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginationResult<OrderDto> Orders);