using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class ServiceManager : IServiceManager
    {


        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _BasketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        private readonly Lazy<IEmailService> _emailService;


        private readonly Lazy<IOrderService> _orderService;




        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User> usermanager, IConfiguration configuration, IOptions<JwtOptions> options)
        {



            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

            _BasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));

            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(usermanager, options, mapper));

            _emailService = new Lazy<IEmailService>(() => new EmailService(configuration));

            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));

        }
        public IProductService ProductService => _productService.Value;


        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IBasketService BasketService => _BasketService.Value;

        public IEmailService EmailService => _emailService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
