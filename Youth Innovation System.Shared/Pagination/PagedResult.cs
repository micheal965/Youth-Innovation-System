using System.Text.Json.Serialization;

namespace Youth_Innovation_System.Shared.Pagination
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        [JsonIgnore]
        public int TotalRecords { get; set; }
        [JsonIgnore]
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public PagedResult(List<T> items, int totalRecords, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageSize = pageSize;
        }
    }
}