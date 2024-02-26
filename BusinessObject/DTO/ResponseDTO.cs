using System.Text.Json.Serialization;

namespace BusinessObject.DTO;

public class ResponseDTO<T>
{
    public bool Success { get; set; } = true;
    public T Payload { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorDetails? Error { get; set; }
}

public class ErrorDetails
{
    public int Code { get; set; }
    public string Message { get; set; }
}