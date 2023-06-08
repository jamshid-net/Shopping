using Flurl.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace Shopping.Application.Service;
public static class RateLimiterService
{
    public static IServiceCollection AddLimiters(this IServiceCollection services)
    {
        services.AddRateLimiter(option =>
        {

            //option.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
            //{
            //    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpcontext.Request.Headers.Host.ToString(), partion =>
            //    {
            //        new FixedWindowRateLimiterOptions()
            //        {
            //            Window = TimeSpan.FromSeconds(5),
            //            AutoReplenishment = true,
            //            PermitLimit = 10,
            //            QueueLimit = 15,
            //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            //        };

            //    });

            //});
            option.AddConcurrencyLimiter("GetAllProduct", opt =>
            {
                opt.QueueLimit = 10;
                opt.PermitLimit = 1;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
               
            });

            option.AddSlidingWindowLimiter("SlidingWindowLimiter", opt =>
            {
                opt.QueueLimit = 10;
                opt.PermitLimit = 7;
                opt.AutoReplenishment = true;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.SegmentsPerWindow = 3;
                opt.Window = TimeSpan.FromSeconds(100);


            });

            option.AddFixedWindowLimiter("FixedLimeter", opt =>
            {
                opt.QueueLimit = 10;
                opt.PermitLimit = 15;
                opt.AutoReplenishment = true;
                opt.QueueProcessingOrder= QueueProcessingOrder.OldestFirst;
                opt.Window = TimeSpan.FromSeconds(10);

            });

        });


        return services;

    }

}
