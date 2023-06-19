namespace Api.Models
{
    public class Response
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }

        public object? User { get; set; }

    }
}
