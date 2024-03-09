using OralData.Responses;

namespace OralData.Backend.services
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}
