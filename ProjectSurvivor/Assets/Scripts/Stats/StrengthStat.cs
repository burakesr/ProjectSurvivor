public class StrengthStat : Stat
{
    public StrengthStat(StrengthStatConfig strengthStat) 
    {
        statConfigure = strengthStat;
        value = strengthStat.baseValue;
    }

    
    public int DamageWithStrengthStat(int damageAmount)
    {
        damageAmount += StatsManager.Instance.GetStrengthStat.value;
        return damageAmount;
    }
}
