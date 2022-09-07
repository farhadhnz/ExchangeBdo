using ExchangeBdo.SharedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeBdo.SharedServices
{
    public class ApiFetchingService<T> : IApiFetchingService<T>
    {
        private readonly HttpClient _httpClient;

        public ApiFetchingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<T>> FetchDataAsync(Dictionary<string, string> headers, string url)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            ApiResult<T> result = null;
            try
            {
                var outputStream = await _httpClient.GetStreamAsync(_httpClient.BaseAddress.OriginalString +  url);
                result = new ApiResult<T>
                {
                    Success = true,
                    ResultValue = await DeserializeStreamAsync(outputStream)
                };
            }
            catch (Exception)
            {
                result = new ApiResult<T>
                {
                    Success = false,
                    ResultValue = Activator.CreateInstance<T>()
                };
            }

            return result;
        }

        private async Task<T> DeserializeStreamAsync(Stream? outputStream)
        {
            var exchange = await JsonSerializer.DeserializeAsync<T>(outputStream);

            return exchange;
        }
    }
}
