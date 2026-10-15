namespace Basket.API.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string UserName) : base($"Basket With UserName {UserName} Not Found.")
        {
        }
    }
}
