namespace ExchangeBdo.SharedDtos
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T ResultValue { get; set; }
    }
}
