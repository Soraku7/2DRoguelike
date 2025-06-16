
public enum GameState
{
    MENU,
    GAME,
    WAVETRANSITION,
    SHOP,
    WEAPONSELECTION,
    GAMEOVER,
    STAGECOMPLETE
}

public enum Stat
{
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalPrecent,
    MoveSpeed,
    MaxHealth,
    Range,
    HealthRecoverySpeed,
    Armor,
    Luck,
    Dodge,
    LifeSteal,
}

public static class Enums
{
    public static string FormatStat(Stat stat)
    {
        string formated = "";
        string unformatedString = stat.ToString();
        
        if(unformatedString.Length == 0)
        {
            return "Unvalid Stat Name";
        }
        
        for(int i = 0 ; i < unformatedString.Length; i++)
        {
            if (i > 0 && char.IsUpper(unformatedString[i]))
            {
                formated += " ";
            }
            formated += unformatedString[i];
        }
        
        return stat.ToString().Replace("RecoverySpeed", "Recovery Speed");
    }
}