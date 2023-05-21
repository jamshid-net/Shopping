using Microsoft.Extensions.Configuration;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;
using Shopping.TelegramBotService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Shopping.TelegramBotService.TelegramService
{
    public class TelegramServiceClient 
    {
        private readonly IConfiguration _config;
       
        TelegramBotClient botClient;
       
        public TelegramServiceClient(IConfiguration configuration,string webrootPath)
        {
            
            botClient = new(configuration.GetConnectionString("TelegramToken"));
            botClient.StartReceiving(new TelegramHandler(webrootPath)); 
        }

        
       
    }
  
}
