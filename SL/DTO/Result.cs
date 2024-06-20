namespace SL.DTO
{
    public class Result
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public Exception Error { get; set; }
        public object Data { get; set; }
    }
}
