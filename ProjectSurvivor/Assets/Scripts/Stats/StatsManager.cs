using System.Collections.Generic;

public class StatsManager : SingletonMonoBehaviour<StatsManager>
{
    public List<Stat> stats = new List<Stat>();

    public MaxHealthStat GetMaxHealthStat => maxHealthStat;
    public RecoveryStat GetRecoveryStat => recoveryStat;
    public ArmorStat GetArmorStat => armorStat;
    public StrengthStat GetStrengthStat => strengthStat;
    public CriticalHitChanceStat GetCriticalHitChanceStat => criticalHitChanceStat;
    public CriticalHitDamageStat GetCriticalHitDamageStat => criticalHitDamageStat;

    private MaxHealthStat maxHealthStat;
    private RecoveryStat recoveryStat;
    private ArmorStat armorStat;
    private StrengthStat strengthStat;
    private CriticalHitChanceStat criticalHitChanceStat;
    private CriticalHitDamageStat criticalHitDamageStat;

    private void Start()
    {
        if (GameManager.Instance.isTestBuild){
            CreateStats();
        }
    }

    private void OnEnable() {
        GameManager.Instance.OnPlayerSpawned += CreateStats;
    }

    private void OnDisable() {
        GameManager.Instance.OnPlayerSpawned -= CreateStats;
    }

    private void CreateStats()
    {
        maxHealthStat = new MaxHealthStat(GameManager.Instance.GetPlayer().CharacterConfig.maxHealthStatConfig);
        recoveryStat = new RecoveryStat(GameManager.Instance.GetPlayer().CharacterConfig.recoveryStatConfig);
        armorStat = new ArmorStat(GameManager.Instance.GetPlayer().CharacterConfig.armorStatConfig);
        strengthStat = new StrengthStat(GameManager.Instance.GetPlayer().CharacterConfig.strengthStatConfig);
        criticalHitChanceStat = new CriticalHitChanceStat(GameManager.Instance.GetPlayer().CharacterConfig.criticalHitChanceStatConfig);
        criticalHitDamageStat = new CriticalHitDamageStat(GameManager.Instance.GetPlayer().CharacterConfig.criticalHitDamageStatConfig);

        stats.Add(maxHealthStat);
        stats.Add(recoveryStat);
        stats.Add(armorStat);
        stats.Add(strengthStat);
        stats.Add(criticalHitChanceStat);
        stats.Add(criticalHitDamageStat);

        // Set max health
        maxHealthStat.Action(GameManager.Instance.GetPlayer().GetHealth);
    }
}
