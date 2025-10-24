using TelegramBot.GameLogic;

namespace TelegramBot.Models;

public class GameSession
{
    public Player Player { get; set; }
    public Interfaces.IEnemiesStats EnemyBase { get; set; }
    public TowerProgression TowerProgression { get; set; }
    public BattleSystem BattleSystem { get; set; }
}