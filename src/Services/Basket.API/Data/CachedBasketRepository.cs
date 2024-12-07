
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public   async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {


            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancelationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await repository.GetBasket(userName, cancelationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancelationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancelationToken = default)
        {
             
            await repository.StoreBasket(basket, cancelationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancelationToken);

            return basket;
        }
    }
}
