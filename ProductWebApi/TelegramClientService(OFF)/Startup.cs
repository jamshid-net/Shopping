using Shopping.TelegramBotService.TelegramService;

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
