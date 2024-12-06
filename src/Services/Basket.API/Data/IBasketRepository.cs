﻿namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default);

        Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancelationToken = default);

        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);    
    }
}
