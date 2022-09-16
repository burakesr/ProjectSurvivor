public class ArmorStat : Stat
{
    public ArmorStat(ArmorStatConfig statConfigure)
    {
        this.statConfigure = statConfigure;
        value = statConfigure.baseValue;
    }

    public int ArmoredDamage(int amount)
    {
        return amount -= value;
    }
}
