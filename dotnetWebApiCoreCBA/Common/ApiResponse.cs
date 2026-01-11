namespace dotnetWebApiCoreCBA.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string errorCode, string message)
            => new() { Success = false, ErrorCode = errorCode, Message = message };
    }

    // Non-generic helper
    public class ApiResponse : ApiResponse<object?>
    {
        public static ApiResponse Ok(string? message = null)
            => new() { Success = true, Message = message };
    }
}
