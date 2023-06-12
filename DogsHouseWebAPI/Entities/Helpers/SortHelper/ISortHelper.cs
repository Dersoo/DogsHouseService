namespace Entities.Helpers.SortHelper
{
    public interface ISortHelper<T>
    {
        IEnumerable<T> ApplySort(IEnumerable<T> entities, string orderByQueryString);
    }
}
