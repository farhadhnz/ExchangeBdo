using ExchangeBdo.SharedDtos;

namespace ExchangeBdo.SharedServices
{
    public interface IApiFetchingService<T>
    {
        Task<ApiResult<T>> FetchDataAsync(Dictionary<string, string> headers, string url);
    }
}
