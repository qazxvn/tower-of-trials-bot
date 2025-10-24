using System.Runtime.InteropServices.ComTypes;

namespace TelegramBot.Models;

public class DemonLord : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Демон лорд";
    public int Hp { get; set; } = 100;
    public int AttackDamage { get; } = 40;
    public int Armor { get; } = 12;
    public double MagicResistance { get; } = 25.5;
}

public class Archmage : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Архимаг";
    public int Hp { get; set; } = 100;
    public int AttackDamage { get; } = 60;
    public int Armor { get; } = 3;
    public double MagicResistance { get; } = 85.5;
}

public class DarkKnight : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Тёмный рыцарь";
    public int Hp { get; set; } = 150;
    public int AttackDamage { get; } = 40;
    public int Armor { get; } = 20;
    public double MagicResistance { get; } = 15.3;
}

public class AncientDragon : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Древний дракон";
    public int Hp { get; set; } = 200;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 30;
    public double MagicResistance { get; } = 20.5;
}

public class PhantomLord : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Фантомный лорд";
    public int Hp { get; set; } = 150;
    public int AttackDamage { get; } = 45;
    public int Armor { get; } = 0;
    public double MagicResistance { get; } = 90;
}

public class WraithKing : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Призрачный король";
    public int Hp { get; set; } = 170;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 25;
    public double MagicResistance { get; } = 35;
}

public class ElectricGolem : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Элекстрический голем";
    public int Hp { get; set; } = 170;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 25;
    public double MagicResistance { get; } = 35;
}

public class EarthGolem : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Земляной голем";
    public int Hp { get; set; } = 170;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 25;
    public double MagicResistance { get; } = 35;
}

public class FireGolem : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Огненный голем";
    public int Hp { get; set; } = 170;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 25;
    public double MagicResistance { get; } = 35;
}

public class AncientGolem : Interfaces.IEnemiesStats
{
    public string EnemyName { get; } = "Древний голем";
    public int Hp { get; set; } = 200;
    public int AttackDamage { get; } = 50;
    public int Armor { get; } = 35;
    public double MagicResistance { get; } = 50;
}