
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO
{
    public class SearchBookDTO
    {
        public string? BookName { get; set; }
        public string? CategoryId { get; set; }
        public string? Author { get; set; }
        public string? PublisherName { get; set; }
        public string? sort { get; set; }
        public int PageIndex { get; set; } = 0;
        [Range(1, 100)] public int PageSize { get; set; } = 9;
    }
}
