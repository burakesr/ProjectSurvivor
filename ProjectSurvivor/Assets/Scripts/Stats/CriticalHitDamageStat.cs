public class CriticalHitDamageStat : Stat {
    public CriticalHitDamageStat(StatConfigureSO config){
        this.statConfigure = config;
        value = statConfigure.baseValue;
    }

    public int CriticalDamage(int damage)
    {
        return (int)((damage * value) / 100f);
    }
}