namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(UserName, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(UserName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart Basket, CancellationToken cancellationToken = default)
        {
            session.Store(Basket);
            await session.SaveChangesAsync(cancellationToken);
            return Basket;
        }

        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(UserName, cancellationToken);
            if (basket is null)
                throw new BasketNotFoundException(UserName);
            session.Delete<ShoppingCart>(UserName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
