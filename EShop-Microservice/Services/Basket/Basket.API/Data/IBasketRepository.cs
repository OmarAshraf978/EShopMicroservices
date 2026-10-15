namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default);
        public Task<ShoppingCart> StoreBasket(ShoppingCart Basket, CancellationToken cancellationToken = default);
        public Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default);
    }
}
