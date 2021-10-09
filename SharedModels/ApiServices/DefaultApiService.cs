using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Game.Common.RefitModels;
using System.Net.Http;

namespace Game.Common.ApiServices
{
    public class DefaultApiService<TApiMethods> : IDefaultApiService<TApiMethods>
    {
        public HttpClient HttpClient { get; set; }
        public TApiMethods ApiMethods { get; set; }

        public DefaultApiService(HttpClient httpClient, string connectionString)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(connectionString);
            ApiMethods = RestService.For<TApiMethods>(HttpClient);
        }

        private async Task<ResponseData<T>> ExecuteInnerRequest<T>(Func<Task<ApiResponse<T>>> request) where T : class
        {
            ApiResponse<T> response = await request.Invoke();
            ResponseData<T> result = new() { StatusCode = response.StatusCode };

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result.Data = response.Content;
                    break;

                case HttpStatusCode.BadRequest:
                    result.Errors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(response.Error.Content);
                    break;

            }
            return result;
        }
        public async Task<ResponseData<T>> ExecuteRequest<T>(Func<Task<ApiResponse<T>>> request) where T : class
        {
            return await ExecuteInnerRequest(request);
        }
    }
}
