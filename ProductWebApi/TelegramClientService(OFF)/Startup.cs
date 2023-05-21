using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;
using Shopping.TelegramBotService.TelegramService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Shopping.TelegramBotService
{
    public static class Startup
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration, string webRootPath)
        {
           
            services.AddSingleton(new TelegramServiceClient(configuration, webRootPath));
            return services;
        }

    }
}
