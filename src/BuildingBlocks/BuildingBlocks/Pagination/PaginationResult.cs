

namespace BuildingBlocks.Pagination;

public class PaginationResult<IEntity>(int pageIndex, int pageSize, long count, IEnumerable<IEntity> data)
    where IEntity : class
{
    public int PageIndex { get; } = pageIndex;

    public int PageSize { get; } = pageSize;

    public long Count { get; } = count;

    public IEnumerable<IEntity> Data { get; } = data;
}