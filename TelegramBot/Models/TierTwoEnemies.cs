namespace TelegramBot.Models;

public class Zombie : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Зомби";
    public int Hp { get; set; } = 30;
    public int AttackDamage { get; } = 15;
    public int Armor { get; } = 5;
    public double MagicResistance { get; } = 0;
}

public class Orc : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Орк";
    public int Hp { get; set; } = 35;
    public int AttackDamage { get; } = 15;
    public int Armor { get; } = 5;
    public double MagicResistance { get; } = 0;
}

public class DarkMagician : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Темный маг";
    public int Hp { get; set; } = 20;
    public int AttackDamage { get; } = 30;
    public int Armor { get; } = 7;
    public double MagicResistance { get; } = 0;
}

public class SkeletonArcher : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Скелет лучник";
    public int Hp { get; set; } = 25;
    public int AttackDamage { get; } = 25;
    public int Armor { get; } = 2;
    public double MagicResistance { get; } = 0;
}

public class Werewolf : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Оборотень";
    public int Hp { get; set; } = 35;
    public int AttackDamage { get; } = 20;
    public int Armor { get; } = 10;
    public double MagicResistance { get; } = 0;
}

public class Demon : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Демон";
    public int Hp { get; set; } = 40;
    public int AttackDamage { get; } = 20;
    public int Armor { get; } = 5;
    public double MagicResistance { get; } = 0;
}