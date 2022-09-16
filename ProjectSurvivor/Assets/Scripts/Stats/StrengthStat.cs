public class StrengthStat : Stat
{
    public StrengthStat(StrengthStatConfig strengthStat) 
    {
        statConfigure = strengthStat;
        value = strengthStat.baseValue;
    }
}
