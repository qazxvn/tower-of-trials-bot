namespace TelegramBot;

public class Interfaces
{
    public interface IEnemiesStats
    {
        public string EnemyName { get; }
        public int Hp { get; set; }
        public int AttackDamage { get; }
        public int Armor { get; }
        public double MagicResistance { get; }
    }
}