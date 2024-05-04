namespace FinalProyect.Domain.Interface
{
    public interface IRedisCacheService{
        Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime, TimeSpan? unusuedExpireTime);
        Task<T?> GetRecordAsync<T>(string recordId);
    }
}