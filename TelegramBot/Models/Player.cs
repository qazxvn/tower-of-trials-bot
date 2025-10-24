using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Models;

public class Player
{
    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;
    public int Mana { get; set; } = 100;
    public int MaxMana { get; set; } = 100;
    public int AttackDamage { get; set; } = 10;
    public int Armor { get; set; } = 5;
    
    // Спелы игрока
    public int FireBoll { get; set; } = 10;
    public int ChaosMeteor { get; set; } = 15;
    public int SunStrike { get; set; } = 10;
    public int ElectricalStorm { get; set; } = 10;
    
    // Здесь ниже логика прокачки персонажа 
    public async Task SendCharacterUpgradeKeyboard(ITelegramBotClient bot, long chatId)
    {
        var upgradeKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("🗡 Урон +5", "attack_upgrade"),
                InlineKeyboardButton.WithCallbackData("🛡 Броня +3", "armor_upgrade"), 
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("❤️ Здоровье +15","hp_upgrade"),
                InlineKeyboardButton.WithCallbackData("🔥 Мана +10", "mana_upgrade"), 
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("🗡 магический урон +5","magicDamage_upgrade"), 
            }
        });

        await bot.SendMessage(chatId, "Выьерите улучшение:", replyMarkup: upgradeKeyboard);
    }
     public async Task WhatThePlayerHasUpgraded(ITelegramBotClient bot, long chatId, string actionData, GameSession session)
     {
         switch (actionData)
         {
             case "attack_upgrade":
                 session.Player.AttackDamage += 5;
                 break;
             
             case "armor_upgrade":
                 session.Player.Armor += 3;
                 break;
             
             case "hp_upgrade":
                 session.Player.MaxHp += 15;
                 break;
             
             case "mana_upgrade":
                 session.Player.MaxMana += 10;
                 break;
             
             case "magicDamage_upgrade":
                 session.Player.ChaosMeteor += 5;
                 session.Player.ElectricalStorm += 5;
                 session.Player.FireBoll += 5;
                 session.Player.SunStrike += 5;
                 break;
         }
         
         var upgradeNames = new Dictionary<string, string>
         {
             { "attack_upgrade", "Урон +5" },
             { "armor_upgrade", "Броня +3" },
             { "hp_upgrade", "Здоровье +15" },
             { "mana_upgrade", "Мана +10" },
             { "magicDamage_upgrade", "Магический урон +5" }
         };

         string upgradeName = upgradeNames.ContainsKey(actionData) ? upgradeNames[actionData] : actionData;
         
         session.Player.Hp = session.Player.MaxHp;
         session.Player.Mana = session.Player.MaxMana;
         
         await bot.SendMessage(chatId,
             $"✅ Вы выбрали улучшение: {upgradeName}\n" +
             $"❤️ HP: {session.Player.Hp}/{session.Player.MaxHp}\n" +
             $"🔥 Мана: {session.Player.Mana}/{session.Player.MaxMana}\n" +
             $"💪 Урон: {session.Player.AttackDamage}, Броня: {session.Player.Armor}\n" +
             $"🔥 Chaos Meteor: {session.Player.ChaosMeteor}\n" +
             $"⚡ Electrical Storm: {session.Player.ElectricalStorm}\n" +
             $"🔮 Fireball: {session.Player.FireBoll}\n" +
             $"☀ Sunstrike: {session.Player.SunStrike}");
         
         await session.TowerProgression.SendContinueKeyboard(bot, chatId);
     }
}