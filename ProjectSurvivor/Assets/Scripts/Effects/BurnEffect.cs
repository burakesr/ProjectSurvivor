using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "ScriptableObjects/Effects/Burn Effect")]
public class BurnEffect : EffectBaseSO
{

    public override void Enable(Enemy target)
    {
    }

    public override void Tick(Enemy target)
    {
        target.GetHealth.TakeDamage(damagePerSecond);
    }
    public override void Disable(Enemy target)
    {
    }

}
