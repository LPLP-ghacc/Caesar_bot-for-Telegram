using System;
using System.Collections.Generic;
using System.Text;

namespace notif_bot
{
    public static class AppSettings
    {
        public static string Url { get; set; } = "https://URL:443/{0}";
        public static string Name { get; set; } = "<BOT_NAME>";
        public static string Key { get; set; } = "5238182469:AAHdikyemtoDw4ENWLsATjdNf1aTFDDXOiw";

        public static string helpMessage { get; set; } = "Бот Caesar имеет следующие функции: \n" +
                                                         "Команда /binary  зашифровать данные в двоичный код. \n" +
                                                         "Команда /binaryde  расшифровать двоичный код в читаемый текст. \n" +
                                                         "Команда /base64  зашифровать данные в Base64. \n" +
                                                         "Команда /base64de  расшифровать Base64 код в читаемый текст. \n" +
                                                         "Команда /qr зашифровать данные в QR-изображение.";

        public static string StartMessage { get; set; } = "Здравствуйте, бот Caesar создан для работы с шифрованием и дешифрованием данных. \n" +
                                                          "Чтобы узнать какие функции доступны напишите команду /help";
        //comm
        public static string binaryEncrypt { get; } = "/binary "; //8
        public static string binaryDecrypt { get;} = "/binaryde "; //10
        public static string base64Encrypt { get;} = "/base64 "; //8
        public static string base64Decrypt { get; } = "/base64de "; //10
        public static string QREncrypt { get; } = "/qr "; //4
    }
}
