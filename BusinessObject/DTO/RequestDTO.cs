using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO;

public class RequestDTO
{
    public int PageIndex { get; set; } = 0;
    [Range(1, 100)] public int PageSize { get; set; } = 10;
}