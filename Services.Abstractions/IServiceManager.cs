namespace Services.Abstractions
{
    public interface IServiceManager
    {


        public IProductService ProductService { get; }

        public IAuthenticationService AuthenticationService { get; }
        public IBasketService BasketService { get; }


        public IEmailService EmailService { get; }


        public IOrderService OrderService { get; }
    }
}
