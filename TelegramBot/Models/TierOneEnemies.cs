namespace TelegramBot.Models;

public class Goblin : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Гоблин";
    public int Hp { get; set; } = 15;
    public int AttackDamage { get; } = 10;
    public int Armor { get; } = 0;
    public double MagicResistance { get; } = 0;
}

public class Rat : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Крыса";
    public int Hp { get; set; } = 10;
    public int AttackDamage { get; } = 3;
    public int Armor { get; } = 0;
    public double MagicResistance { get; } = 0;
}

public class Skeleton : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Скелет";
    public int Hp { get; set; } = 10;
    public int AttackDamage { get; } = 5;
    public int Armor { get; } = 0;
    public double MagicResistance { get; } = 0;
}

public class WeakGhost : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Слабый призрак";
    public int Hp { get; set; } = 15;
    public int AttackDamage { get; } = 10;
    public int Armor { get; } = 0;
    public double MagicResistance { get; } = 0;
}