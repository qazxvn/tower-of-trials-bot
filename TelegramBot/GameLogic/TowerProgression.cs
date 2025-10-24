using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace TelegramBot.GameLogic;

public class TowerProgression
{
    private readonly Random _random = new Random();
    public int towerLvl = 1;
    public Interfaces.IEnemiesStats RandomEnemyGeneration(int floor)
    {
        if (floor <= 10)
        {
            List<Interfaces.IEnemiesStats> enemies = new List<Interfaces.IEnemiesStats>()
            {
                new Goblin(),
                new Rat(),
                new Skeleton(),
                new WeakGhost()
            };
            return enemies[_random.Next(enemies.Count)];
        }
        else if (floor <= 20)
        {
            List<Interfaces.IEnemiesStats> enemies = new List<Interfaces.IEnemiesStats>()
            {
                new DarkMagician(),
                new Demon(),
                new Orc(),
                new SkeletonArcher(),
                new Werewolf(),
                new Zombie()
            };
            return enemies[_random.Next(enemies.Count)];
        }
        else
        {
            List<Interfaces.IEnemiesStats> enemies = new List<Interfaces.IEnemiesStats>()
            {
                new AncientDragon(),
                new AncientGolem(),
                new Archmage(),
                new DarkKnight(),
                new DemonLord(),
                new EarthGolem(),
                new ElectricGolem(),
                new FireGolem(),
                new PhantomLord(),
                new WraithKing()
            };
            return enemies[_random.Next(enemies.Count)];
        }
        
    }
    public async Task SendContinueKeyboard(ITelegramBotClient bot, long chatId)
    {
        var continueKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("✅ Да","Yes"),
                InlineKeyboardButton.WithCallbackData("❌ Нет","No"),
            }
        });
        
        await bot.SendMessage(chatId, "Хотите продолжить?", replyMarkup: continueKeyboard);
    }
}