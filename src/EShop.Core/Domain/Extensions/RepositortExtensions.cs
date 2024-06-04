
namespace EShop.Core.Domain.Extensions
{
    public static class RepositortExtensions
    {
        public static async Task<T> ThrowIfNull<T>(this Task<T?> task, string errorMessage) where T : class
        {
            var result = await task;
            if (result == null)
            {
                throw new KeyNotFoundException(errorMessage);
            }
            return result;
        }
    }
}
