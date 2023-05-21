using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Shopping.Application.Interfaces;
using Shopping.Application.Service;
using Shopping.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Shopping.TelegramBotService.TelegramService
{
    public class TelegramHandler : IUpdateHandler
    {
        static TelegramButtons mainButtons = new TelegramButtons();
      

        private readonly string _webrootpath;
        static HttpClient _httpClient;
        public TelegramHandler(string webrootpath)
        {
            
            _webrootpath = webrootpath;
            _httpClient = new HttpClient();
          
        }
       
        List<User> _users = new();
        public async Task  HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("HandlePollingErrorAsync:" + exception.Message);
            //await botClient.SendTextMessageAsync(591208356, "Date:" + DateTime.Now + " Exception: " + exception.Message);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient _client, Update update, CancellationToken cancellationToken)
        {

            if (update.Message == null) return;

            Parallel.Invoke(async () =>
            {


                await Console.Out.WriteLineAsync(update.Message.Chat.FirstName);


                string cmd = update?.Message?.Text ?? "";
                if (!cmd.StartsWith("/")) { cmd = "/" + cmd.ToLower(); }
                _users.Add(new User() { ChatId = update.Message.Chat.Id });
                var user = _users.FirstOrDefault(x => x.ChatId == update.Message.Chat.Id);

                if (user?.step == -1)
                {
                    user.Command = cmd;
                    if (cmd.Equals("/addJam722")) user.step = 0;
                    if (cmd.Equals("/updateJam722")) user.step = 0;
                    if (cmd.Equals("/removeJam722")) user.step = 0;
                    if (cmd.Equals("/getbyidJam722")) user.step = 0;
                   
                }
                else if (user != null) cmd = user.Command;

                switch (cmd)
                {
                    case "/start": StartHandler(_client, update); break;
                    case "/addJam722": Add(_client, update, cancellationToken); break;
                    case "/removeJam722": Remove(_client, update); break;
                    case "/updateJam722": Update(_client, update, cancellationToken); break;
                    case "/getbyidJam722": GetByiD(_client, update); break;
                    case "/getallJam722": GetAll(_client, update); break;
                    default:
                        StartHandler(_client, update);
                        ; break;
                }
            });
        }
        private void StartHandler(ITelegramBotClient _client, Update update)
        {

            _client.SendTextMessageAsync
                  (update.Message.Chat.Id,
                  "Hi " + update.Message.Chat.FirstName,
                   replyMarkup: mainButtons.Buttons());

        }


        private async void Remove(ITelegramBotClient client, Update? update)
        {
            long id = update.Message.Chat.Id;
            var user = _users.FirstOrDefault(x => x.ChatId == id);
            if (user != null)
            {
                switch (user.step)
                {
                    case 0:
                        {
                            await client.SendTextMessageAsync(id, "Enter product id for remove");
                            user.step++;


                            break;
                        }

                    case 1:
                        {
                            if (int.TryParse(update.Message.Text, out int ProductId))
                            {
                            
                                string url = "/api/Product/";
                                await _httpClient.DeleteAsync(url + ProductId);
                                user.step = -1;
                                await client.SendTextMessageAsync(id, "Succsess!", replyMarkup: mainButtons.Buttons());
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id, "Error!", replyMarkup: mainButtons.Buttons());

                            }


                            break;
                        }


                }


            }


        }
        private async void Add(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            long id = update.Message.Chat.Id;
            var user = _users.FirstOrDefault(x => x.ChatId == id);
            if (user != null)
            {
                switch (user.step)
                {
                    case 0:
                        {
                            await client.SendTextMessageAsync(id, "Enter Product name: ");
                            user.step++;
                            break;

                        }
                    case 1:
                        {
                            user.Product.ProductName = update.Message.Text;
                            await client.SendTextMessageAsync(id, "Enter price:");
                            user.step++;
                            break;

                        }
                    case 2:
                        {
                            if (decimal.TryParse(update.Message.Text, out decimal _price))
                            {
                                user.Product.Price = _price;
                                await client.SendTextMessageAsync(id, "Photo file or url: ");
                                user.step++;
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id, "Invalid price! please reinput value ");

                            }
                            break;

                        }
                    case 3:
                        {
                            Telegram.Bot.Types.File file = new Telegram.Bot.Types.File();
                            string path = _webrootpath;
                            bool flag = false;
                            if (update.Message.Document != null)
                            {
                                file = await client.GetFileAsync(update.Message.Document.FileId);
                                flag = true;
                            }
                            else if (update.Message.Photo != null)
                            {
                                file = await client.GetFileAsync(update.Message.Photo.Last().FileId);
                                flag = true;
                            }
                            else
                            {

                                user.Product.Picture = update.Message.Text;
                                await client.SendTextMessageAsync(id, "Url succsess uploaded!");
                                await client.SendTextMessageAsync(id, "Enter description");
                                user.step++;
                            }

                            if (flag)
                            {

                                path += Guid.NewGuid() + Path.GetExtension(file.FilePath);
                                using var stream = System.IO.File.OpenWrite(path);
                                var res = await client.GetInfoAndDownloadFileAsync(file.FileId, stream);
                                await client.SendTextMessageAsync(id, "Uploading... wait!");
                            }
                            if (System.IO.File.Exists(path))
                            {
                                user.Product.Picture = $"photos/{Path.GetFileName(path)}";
                                await client.SendTextMessageAsync(id, "Photo succsess uploaded!");
                                await client.SendTextMessageAsync(id, "Enter description");
                                user.step++;
                            }
                            break;

                        }
                    case 4:
                        {
                            user.Product.Description = update.Message.Text;
                        
                            string url = "/api/Product/AddProduct";
                            await _httpClient.PostAsJsonAsync(url, user.Product);
                            await client.SendTextMessageAsync(id,
                           "Succsess",
                           replyMarkup: mainButtons.Buttons());
                            user.Product = new Product();
                            user.step = -1;
                            break;

                        }

                }


            }


        }

        private async void Update(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            long id = update.Message.Chat.Id;
            var user = _users.FirstOrDefault(x => x.ChatId == id);
            if (user != null)
            {
                switch (user.step)
                {
                    case 0:
                        {
                            await client.SendTextMessageAsync(id, "Enter Product Id: ");
                            user.step++;
                            break;
                        }
                    case 1:
                        {
                            if (int.TryParse(update.Message.Text, out int productId))
                            {
                                user.Product.ProductId = productId;
                                await client.SendTextMessageAsync(id, "Enter Product name: ");
                                user.step++;
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id, "Error for id reinput plese: ");
                            }
                            break;

                        }
                    case 2:
                        {
                            user.Product.ProductName = update.Message.Text;
                            await client.SendTextMessageAsync(id, "Enter price:");
                            user.step++;
                            break;

                        }
                    case 3:
                        {
                            if (decimal.TryParse(update.Message.Text, out decimal _price))
                            {
                                user.Product.Price = _price;
                                await client.SendTextMessageAsync(id, "Photo file or url: ");
                                user.step++;
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id, "Invalid price! please reinput value ");

                            }
                            break;

                        }
                    case 4:
                        {
                            Telegram.Bot.Types.File file = new Telegram.Bot.Types.File();
                            string path = _webrootpath;
                            bool flag = false;
                            if (update.Message.Document != null)
                            {
                                file = await client.GetFileAsync(update.Message.Document.FileId);
                                flag = true;
                            }
                            else if (update.Message.Photo != null)
                            {
                                file = await client.GetFileAsync(update.Message.Photo.Last().FileId);
                                flag = true;
                            }
                            else
                            {

                                user.Product.Picture = update.Message.Text;
                                await client.SendTextMessageAsync(id, "Url succsess uploaded!");
                                await client.SendTextMessageAsync(id, "Enter description");
                                user.step++;
                            }

                            if (flag)
                            {

                                path += Guid.NewGuid() + Path.GetExtension(file.FilePath);
                                using var stream = System.IO.File.OpenWrite(path);
                                var res = await client.GetInfoAndDownloadFileAsync(file.FileId, stream);
                                await client.SendTextMessageAsync(id, "Uploading... wait!");
                            }
                            if (System.IO.File.Exists(path))
                            {
                                user.Product.Picture = $"photos/{Path.GetFileName(path)}";
                                await client.SendTextMessageAsync(id, "Photo succsess uploaded!");
                                await client.SendTextMessageAsync(id, "Enter description");
                                user.step++;
                            }
                            break;

                        }
                    case 5:
                        {
                            user.Product.Description = update.Message.Text;
                       
                            string url = "/api/Product/UpdateProduct";
                            var result = await _httpClient.PutAsJsonAsync(url, user.Product);
                            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                await client.SendTextMessageAsync(id,
                                 "wrong id or other errors!",
                                replyMarkup: mainButtons.Buttons());
                                
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id,
                                 "Succsess!",
                                replyMarkup: mainButtons.Buttons());
                            }
                            user.Product = new Product();
                            user.step = -1;
                            break;

                        }

                }
            }
        }

        private async void GetByiD(ITelegramBotClient client, Update? update)
        {
            long id = update.Message.Chat.Id;
            var user = _users.FirstOrDefault(x => x.ChatId == id);
            if (user != null)
            {
                switch (user.step)
                {
                    case 0:
                        {
                            await client.SendTextMessageAsync(id, "Enter product id for get");
                            user.step++;
                            break;
                        }

                    case 1:
                        {
                            if (int.TryParse(update.Message.Text, out int ProductId))
                            {
                          
                                string url = "/api/Product/GetById/" + ProductId;
                                try
                                {
                                    Product? gotProduct = await _httpClient.GetFromJsonAsync<Product>(url);
                                    StringBuilder stringBuilder = new StringBuilder();
                                    stringBuilder.AppendLine($"Product id:{gotProduct.ProductId}");
                                    stringBuilder.AppendLine($"Product name:{gotProduct.ProductName}");
                                    stringBuilder.AppendLine($"Product price:{gotProduct.Price}");
                                    stringBuilder.AppendLine($"Product description:{gotProduct.Description}");       
                                    await client.SendTextMessageAsync(id, stringBuilder.ToString(), replyMarkup: mainButtons.Buttons());
                                }
                                catch (Exception ex)
                                {
                                    await client.SendTextMessageAsync(id, "wrong id", replyMarkup: mainButtons.Buttons());
                                }
                                
                            }
                            else
                            {
                                await client.SendTextMessageAsync(id, "Error!", replyMarkup: mainButtons.Buttons());

                            }
                            user.step = -1;

                            break;
                        }


                }


            }
        }
        
        private async void GetAll(ITelegramBotClient client, Update? update)
        {
            long id = update.Message.Chat.Id;
            StringBuilder stringBuilder = new StringBuilder();
            // Define column headers
            (string Header, int Width)[] columns = {
            ("ID", 5),
            ("Name", 12),
            ("Price", 8),
            ("Image", 12),
            
        };

            // Print headers
            stringBuilder.AppendLine(string.Join("", columns.Select(c => c.Header.PadRight(c.Width))));

         

          
            var products =  await _httpClient.GetFromJsonAsync<List<Product>>("/api/Product/Products");



            foreach (var product in products)
            {
                stringBuilder.AppendFormat("{0,-5} {1,-12} {2,-8}\n",
                    product.ProductId,
                    product.ProductName,
                    product.Price.ToString("C"));
            }


            using (StreamWriter fs = new("products.txt"))
            {
                fs.WriteLine(stringBuilder.ToString());
                
            }


            using Stream fr = System.IO.File.OpenRead("products.txt");
            await client.SendDocumentAsync(id, document: new InputFile(content: fr, fileName: "products.txt"));
            fr.Close();
            await client.SendTextMessageAsync(id, stringBuilder.ToString());



            await client.SendTextMessageAsync(id, "✅",
                 replyMarkup: mainButtons.Buttons());


        }


        record User
        {
            public long ChatId { get; set; } = 0;
            public sbyte step { get; set; } = -1;
            public Product Product { get; set; } = new Product();
            public string Command { get; set; } = "";
        }
    }
}
