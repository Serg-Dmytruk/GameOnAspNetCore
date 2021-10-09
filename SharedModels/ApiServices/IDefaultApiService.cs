using Refit;
using System;
using System.Threading.Tasks;
using Game.Common.RefitModels;

namespace Game.Common.ApiServices
{
    public interface IDefaultApiService<TApiMethods>
    {
        public TApiMethods ApiMethods { get; set; }
        public Task<ResponseData<T>> ExecuteRequest<T>(Func<Task<ApiResponse<T>>> requestFunction) where T : class;
    }
}
