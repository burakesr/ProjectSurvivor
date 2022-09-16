using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("MAX HEALTH STAT CONFIG")]
    [SerializeField] private MaxHealthStatConfig maxHealthStatConfig;
    [Header("RECOVERY STAT CONFIG")]
    [SerializeField] private RecoveryStatConfig recoveryStatConfig;
    [Header("ARMOR STAT CONFIG")]
    [SerializeField] private ArmorStatConfig armorStatConfig;
    [Header("STRENGTH STAT CONFIG")]
    [SerializeField] private StrengthStatConfig strengthStatConfig;

    public List<Stat> stats = new List<Stat>();

    public MaxHealthStat GetMaxHealthStat => maxHealthStat;
    public RecoveryStat GetRecoveryStat => recoveryStat;
    public ArmorStat GetArmorStat => armorStat;
    public StrengthStat GetStrengthStat => strengthStat;

    private MaxHealthStat maxHealthStat;
    private RecoveryStat recoveryStat;
    private ArmorStat armorStat;
    private StrengthStat strengthStat;


    private void Start()
    {
        CreateStats();

        // Set max health
        maxHealthStat.Action(GameManager.Instance.GetPlayer().GetHealth);
    }

    private void CreateStats()
    {
        maxHealthStat = new MaxHealthStat(maxHealthStatConfig);
        recoveryStat = new RecoveryStat(recoveryStatConfig);
        armorStat = new ArmorStat(armorStatConfig);
        strengthStat = new StrengthStat(strengthStatConfig);

        stats.Add(maxHealthStat);
        stats.Add(recoveryStat);
        stats.Add(armorStat);
        stats.Add(strengthStat);        
    }
}
