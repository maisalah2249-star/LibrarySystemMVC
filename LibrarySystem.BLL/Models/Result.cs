namespace LibrarySystem.BLL.Models
{
    public sealed class Result<T>
    {
        public bool Successeded { get; }
        public T? Data { get; }
        public string? Error { get; }

        private Result(bool successeded, T? data, string? error)
        {
            Successeded = successeded;
            Data = data;
            Error = error;
        }

        public static Result<T> Success(T data) => new(true, data, null);
        public static Result<T> Failure(string error) => new(false, default, error);
    }
}