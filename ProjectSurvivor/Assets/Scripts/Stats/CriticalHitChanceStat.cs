using UnityEngine;

public class CriticalHitChanceStat : Stat {
    public CriticalHitChanceStat(CriticalHitChanceStatConfig config){
        this.statConfigure = config;
        value = statConfigure.baseValue;
    }

    public bool IsHitCritical()
    {
        int random = Random.Range(0, 100);

        Debug.Log(random + "   " + value);

        if (value >= random){
            return true;
        }
        return false;
    }
}