namespace EShop.CustomerFe.Services.Interfaces
{
    public interface ICacheClientService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);
        void Remove(string key);
    }
}
