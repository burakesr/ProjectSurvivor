using System;
public class MaxHealthStat : Stat
{
    public MaxHealthStat(MaxHealthStatConfig statConfigure)
    {
        this.statConfigure = statConfigure;
        value = statConfigure.baseValue;
    }

    public void Action(Health health)
    {
        health.SetStartingHealth(value);
    }
}
