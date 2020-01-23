using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;
using TeleBotService.Model;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json.Linq;
using TeleBotService.ControlCenter;
using TeleBotService.Repository;


namespace TeleBotService
{
    public static class Program
    {
        private static TelegramBotClient Bot;
        private static iLottoRepository LTRepository = new LottoRepository();
        private static List<string> checkWordList = new List<string>() { "로또", "/Lotto", "마이키", "Mikey", "@Mikey_봇", "봇" };

        public static async Task Main()
        {
            Bot = new TelegramBotClient(Configuration.BotToken);
            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            Bot.StartReceiving(Array.Empty<UpdateType>());

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }


        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            Console.WriteLine(message.Text);
            if (message == null || message.Type != MessageType.Text) return;

            if(checkWordList.Any(s=>message.Text.Contains(s)))
            {
                switch (message.Text.Split(' ').First())
                {
                    case "/Lotto":
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                        // simulate longer running task
                        await Task.Delay(500);

                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            // first row
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("최신당첨번호", "최신당첨번호"),
                                InlineKeyboardButton.WithCallbackData("최신1등당첨금", "최신1등당첨금"),
                            },
                            // second row
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("이번주 번호추천", "이번주 번호추천"),
                                //InlineKeyboardButton.WithCallbackData("1등지점", "1등지점"),

                            }

                        });
                        await Bot.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "다음 중 하나를 선택 해 주세요",
                            replyMarkup: inlineKeyboard
                        );
                        break;

                    default:
                        const string usage = "안녕하세요:\n" +
                                             "저와 다음 내용에 대해서 이야기 하시겠습니까?\n" +
                                             "\n" +
                                             "/Lotto   - 로또 관련 검색 \n";
                        //+
                        //"/keyboard - send custom keyboard\n" +
                        //"/photo    - send a photo\n" +
                        //"/request  - request location or contact";
                        await Bot.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: usage,
                            replyMarkup: new ReplyKeyboardRemove()
                        );
                        break;
                }
            }
            
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            await Bot.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}"
            );

            var textMessage = "궁금하면 5백원.. :) 하하하";

            switch (callbackQuery.Data)
            {
                case "최신당첨번호":
                    textMessage = LTRepository.GetLastestLottNumber();
                    break;
                case "최신1등당첨금":
                    textMessage = LTRepository.GetLastestFirstPrice();
                    break;
                default:

                    break;
            }
            await Bot.SendTextMessageAsync(
                chatId: callbackQuery.Message.Chat.Id,
                text: textMessage
            );
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

            InlineQueryResultBase[] results = {
                // displayed result
                new InlineQueryResultArticle(
                    id: "3",
                    title: "TgBots",
                    inputMessageContent: new InputTextMessageContent(
                        "hello"
                    )
                )
            };
            await Bot.AnswerInlineQueryAsync(
                inlineQueryId: inlineQueryEventArgs.InlineQuery.Id,
                results: results,
                isPersonal: true,
                cacheTime: 0
            );
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }


    }   
}
