using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.GameLogic;
using TelegramBot.Models;
using System.Collections.Concurrent;

ConcurrentDictionary<long, GameSession> sessions = new();

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("8477008462:AAFeYB44UgVKUMXavcZqMpdp-JEWBqsaREo", cancellationToken: cts.Token);
var me = await bot.GetMe();
bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception);
}

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text == "/start")
    {
        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Да", "start_yes"),
                InlineKeyboardButton.WithCallbackData("Нет", "start_no")
            }
        });
        await bot.SendMessage(msg.Chat, "Начать прохождение башни?", replyMarkup: keyboard);
    }
}

async Task OnUpdate(Update update)
{
    if (update.CallbackQuery is { } query)
    {
        var chatId = query.Message.Chat.Id;

        if (!sessions.TryGetValue(chatId, out var session))
        {
            session = new GameSession
            {
                Player = new Player(),
                TowerProgression = new TowerProgression(),
                BattleSystem = new BattleSystem()
            };
            sessions[chatId] = session;
        }
        if (query.Data == "start_yes")
        {
            session.EnemyBase = session.TowerProgression.RandomEnemyGeneration(session.TowerProgression.towerLvl);
            
            await session.BattleSystem.WhatEnemyAttacked(bot, chatId, session.EnemyBase);
            await session.BattleSystem.ChosenAttackType(bot, chatId);
        }
        else if (query.Data == "default_attack" || query.Data.EndsWith("_spell") || query.Data == "spell")
        {
            await session.BattleSystem.Battle(session, query.Data, bot, chatId, session.EnemyBase);
        }
        else if (query.Data == "Yes")
        {
            session.EnemyBase = session.TowerProgression.RandomEnemyGeneration(session.TowerProgression.towerLvl);
            
            await session.BattleSystem.WhatEnemyAttacked(bot, chatId, session.EnemyBase);
            await session.BattleSystem.ChosenAttackType(bot, chatId);
        }
        else if(query.Data == "No")
        {
            session.BattleSystem.PlayerStatReset(session);
            session.TowerProgression.towerLvl = 1;
            await bot.SendMessage(chatId, "⬇️ Вы спускаетесь на 1 этаж! \nВсе ваши улучшения пропадают 😢");
            await bot.SendMessage(chatId, "⚔️ Для того чтобы начать заново проходить башню, напишите |`/start`| в чат!");
        }
        else if (query.Data.EndsWith("_upgrade"))
        {
            await session.Player.WhatThePlayerHasUpgraded(bot, chatId, query.Data, session);
            
        }
    }
}


