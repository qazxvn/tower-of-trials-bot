using Telegram.Bot;
using TelegramBot.Models;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegramBot.GameLogic;

public class BattleSystem
{
    public async Task Battle(GameSession session, string actionData, ITelegramBotClient bot, long chatId, Interfaces.IEnemiesStats enemy)
    {
        var player = session.Player;
        double enemyDamageMultiplier = 1.0 + (session.TowerProgression.towerLvl - 1) * 0.1; // +10% урона за этаж
        double enemyArmorMultiplier = 1.0 + (session.TowerProgression.towerLvl - 1) * 0.08; // +8% брони за этаж
        double enemyMagicResistMultiplier = 1.0 + (session.TowerProgression.towerLvl - 1) * 0.07; // +7% маг. защиты за этаж
        //Ход игрока
        if (actionData == "default_attack")
        {
            var adjustedArmor = enemy.Armor * enemyArmorMultiplier;
            var physicalAttackDamage = Math.Max(0, (int)(player.AttackDamage * (100.0 / (100.0 + adjustedArmor))));
            enemy.Hp -= physicalAttackDamage;
            await bot.SendMessage(chatId,
                $"⚔ Вы нанесли {enemy.EnemyName} {physicalAttackDamage} урона!\n" +
                $"💖 Осталось HP у врага: {enemy.Hp}");
            
            if (enemy.Hp <= 0)
            {
                session.TowerProgression.towerLvl++;
                session.EnemyBase = null;
                await bot.SendMessage(chatId,
                    $"🏆 Поздравляем! Вы победили {enemy.EnemyName} и поднимаетесь на этаж {session.TowerProgression.towerLvl}!\n\n" +
                    $"🔧 Выберите улучшение для персонажа:");
                await session.Player.SendCharacterUpgradeKeyboard(bot,chatId);
                return;
            }
            
            // ход противника при дефолт атаке
            var baseEnemyDamageForPlayer = Math.Max(0, (int)(enemy.AttackDamage * (100.0 / (100.0 + player.Armor))));
            var scaledDamage = (int)(baseEnemyDamageForPlayer * enemyDamageMultiplier);
            
            player.Hp -= scaledDamage;
            await bot.SendMessage(chatId, $"💥 Противник атаковал! \nВы получили {scaledDamage} урона.\n❤️ Ваше здоровье: {player.Hp}");
            
            if (player.Hp <= 0)
            {
                PlayerStatReset(session);
                session.TowerProgression.towerLvl = 1;
                await bot.SendMessage(chatId, $"Вы проиграли! Вас одолел {enemy.EnemyName}");
            }
        }
        else if (actionData.EndsWith("_spell"))
        {
            int damage = 0;
            switch (actionData)
            {
                case "chaosMeteor_spell":
                    damage = Math.Max(0, (int)(player.ChaosMeteor * (1 - enemy.MagicResistance / 100.0)));
                    break;
                case "fireball_spell":
                    damage = Math.Max(0, (int)(player.FireBoll * (1 - enemy.MagicResistance / 100.0)));
                    break;
                case "sunstrike_spell":
                    damage = Math.Max(0, (int)(player.SunStrike * (1 - enemy.MagicResistance / 100.0)));
                    break;
                case "electricStorm_spell":
                    damage = Math.Max(0, (int)(player.ElectricalStorm * (1 - enemy.MagicResistance / 100.0)));
                    break;
            }

            player.Mana -= 15;
            var scaledMagicResistance = enemy.MagicResistance * enemyMagicResistMultiplier;
            var resistanceFactor = 1 - (scaledMagicResistance / 100.0);
            resistanceFactor = Math.Max(0, resistanceFactor); // защита не может быть больше 100%

            // Применяем сопротивление к базовому урону
            var damageTakingIntoAccountMagicResistance = (int)(damage * resistanceFactor);
            enemy.Hp -= damageTakingIntoAccountMagicResistance;

            await bot.SendMessage(chatId,
                $"⚔ Вы нанесли {enemy.EnemyName} {damage} урона!\n" +
                $"💖 Осталось HP у врага: {enemy.Hp}");
            
            if (enemy.Hp <= 0)
            {
                session.TowerProgression.towerLvl++;
                session.EnemyBase = null;
                
                await bot.SendMessage(chatId,
                    $"🏆 Поздравляем! Вы победили {enemy.EnemyName} и поднимаетесь на этаж {session.TowerProgression.towerLvl}!\n\n" +
                    $"🔧 Выберите улучшение для персонажа:");
                
                await session.Player.SendCharacterUpgradeKeyboard(bot,chatId);
                return;
            }
            //Ход противника если маг атака
            int startingDamageToThePlayer = Math.Max(0, (int)(enemy.AttackDamage * (100.0 / (100.0 + player.Armor))));
            int multipliedDamage = (int)(startingDamageToThePlayer * enemyDamageMultiplier);
            player.Hp -= multipliedDamage;
            await bot.SendMessage(chatId, $"💥 Противник атаковал!\nВы получили {multipliedDamage} урона.\n❤️ Ваше здоровье: {player.Hp}");
            if (player.Hp <= 0)
            {
                PlayerStatReset(session);
                session.TowerProgression.towerLvl = 1;
                await bot.SendMessage(chatId, $"Вы проиграли вас одолел {enemy.EnemyName}");
            }
        }
        else if (actionData == "spell")
        {
            await SendSpellKeyboard(bot, chatId);
            return;
        }
        if (player.Hp > 0 && enemy.Hp > 0 && actionData != "spell")
        {
            await ChosenAttackType(bot, chatId);
        }
    }
    
    public async Task WhatEnemyAttacked(ITelegramBotClient bot, long chatId, Interfaces.IEnemiesStats enemyBase)
    {
        await bot.SendMessage(chatId,
            $"👹 На вас напал: {enemyBase.EnemyName}!\n" +
            $"💖 HP: {enemyBase.Hp}\n" +
            $"🗡 Урон: {enemyBase.AttackDamage}\n" +
            $"🛡 Броня: {enemyBase.Armor}\n" +
            $"✨ Сопротивление магии: {enemyBase.MagicResistance}"); 
    }
    
    private async Task SendSpellKeyboard(ITelegramBotClient bot, long chatId)
    {
        var spellKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("🔮 Огненный шар", "fireball_spell"),
                InlineKeyboardButton.WithCallbackData("🔥 Метеорит хаоса", "chaosMeteor_spell")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("☀ Санстрайк", "sunstrike_spell"),
                InlineKeyboardButton.WithCallbackData("⚡ Электрический шторм", "electricStorm_spell")
            }
        });

        await bot.SendMessage(chatId, "Выберите заклинание:", replyMarkup: spellKeyboard);
    }

    public async Task ChosenAttackType(ITelegramBotClient bot, long chatId)
    {
        var attackType = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("⚔ Обычная атака", "default_attack"),
                InlineKeyboardButton.WithCallbackData("✨ Заклинание", "spell"), 
            }
        });

        await bot.SendMessage(chatId, "Выберите тип аткаки", replyMarkup: attackType);
    }

    public void PlayerStatReset(GameSession session)
    { 
        session.Player.Armor = 5;
        session.Player.AttackDamage = 15;
        session.Player.ChaosMeteor = 15;
        session.Player.ElectricalStorm = 10;
        session.Player.FireBoll = 10;
        session.Player.Hp = 100;
        session.Player.Mana = 100;
        session.Player.SunStrike = 10;
    }
}