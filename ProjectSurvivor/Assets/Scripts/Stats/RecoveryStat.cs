public class RecoveryStat : Stat
{
    public float recoveryTime;

    public RecoveryStat(RecoveryStatConfig statConfigure)
    {
        this.statConfigure = statConfigure;
        value = statConfigure.baseValue;
        recoveryTime = statConfigure.recoveryTime;
    }

    public void Action(Health health)
    {
        health.Heal(value);
    }
}