using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
  public static class ApplicationServicesExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      // Injecting Caching service
      services.AddSingleton<IResponseCacheService, ResponseCacheService>();

      // Injecting Token service
      services.AddScoped<ITokenService, TokenService>();

      // Injecting the repository
      services.AddScoped<IProductRepository, ProductRepository>();

      // Injecting the payment service
      services.AddScoped<IPaymentService, PaymentService>();

      // Injecting Basket Repository
      services.AddScoped<IBasketRepository, BasketRepository>();

      // Injecting the Generic Repository
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

      // Injecting the Order Service
      services.AddScoped<IOrderService, OrderService>();

      // Injecting the UnitOfWork (UoW)
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = ActionContext =>
        {
          var errors = ActionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

          var errorResponse = new ApiValidationErrorResponse
          {
            Errors = errors
          };

          return new BadRequestObjectResult(errorResponse);
        };
      });

      return services;
    }
  }
}
