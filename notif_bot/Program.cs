using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace notif_bot
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            OnStart();
        }

        static ITelegramBotClient bot = new TelegramBotClient(AppSettings.Key);
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            StartMessage(botClient, update);
            GetHelpMessage(botClient, update, cancellationToken);
            GetImageMessage(botClient, update, cancellationToken);
            Encryption_Decryption(botClient, update);
            QR_Encryption_Decryption(botClient, update);
        }

        #region Start
        private static void OnStart()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
        #endregion

        #region consoleOutput
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        #endregion

        #region StartMesMethod
        private static async void StartMessage(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, AppSettings.StartMessage);
                    return;
                }
            }
        }
        #endregion

        #region GetImageMessage

        private static async void GetImageMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "картинку")
                {
                    try
                    {
                        await botClient.SendPhotoAsync(message.Chat, message.Caption = GetNewImage());
                    }
                    catch
                    {
                        GetImageMessage(botClient, update, cancellationToken);
                    }

                    return;
                }
            }
        }

        private static string GetNewImage()
        {
            var newurl = "";
            var url = "http://img10.reactor.cc/pics/post/Anime-Art-Anime-";
            Random rnd = new Random(); newurl = url + rnd.Next(999999, 9999999) + ".jpeg";
            return newurl;
        }
        #endregion

        #region HelpMessage
        private static async void GetHelpMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/help")
                {
                    try
                    {
                        await botClient.SendTextMessageAsync(message.Chat, AppSettings.helpMessage);
                    }
                    catch
                    {
                        GetImageMessage(botClient, update, cancellationToken);
                    }
                    return;
                }
            }
        }
        #endregion

        private static async void Encryption_Decryption(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                #region bynaryReg
                var message = update.Message;
                if (message.Text.ToLower().StartsWith(AppSettings.binaryEncrypt))
                {
                    var toEncryptMes = message.Text.Substring(8);

                    await botClient.SendTextMessageAsync(message.Chat, toEncryptMes.ToBinary());
                    return;
                }
                if (message.Text.ToLower().StartsWith(AppSettings.binaryDecrypt))
                {
                    var toDecryptMes = message.Text.Substring(10);

                    await botClient.SendTextMessageAsync(message.Chat, Encrypt.BinaryToString(toDecryptMes));
                    return;
                }
                #endregion

                #region Base64Reg
                if (message.Text.ToLower().StartsWith(AppSettings.base64Encrypt))
                {
                    var toDecryptMes = message.Text.Substring(8);

                    await botClient.SendTextMessageAsync(message.Chat, Encrypt.Base64Encode(toDecryptMes));
                    return;
                }
                if (message.Text.ToLower().StartsWith(AppSettings.base64Decrypt))
                {
                    var toDecryptMes = message.Text.Substring(10);

                    await botClient.SendTextMessageAsync(message.Chat, Encrypt.Base64Decode(toDecryptMes));
                    return;
                }
                #endregion
            }
        }

        private static async void QR_Encryption_Decryption(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower().StartsWith(AppSettings.QREncrypt))
                {
                    var toEncryptMes = message.Text.Substring(4);
                    await botClient.SendPhotoAsync(message.Chat, "http://api.qrserver.com/v1/create-qr-code/?data=" + toEncryptMes +"&size=1000x1000");
                    return;
                }
            }
        }
    }
}
