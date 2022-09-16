using UnityEngine;

[CreateAssetMenu(fileName = "BleedEffect", menuName = "ScriptableObjects/Effects/Bleed Effect")]
public class BleedEffect : EffectBaseSO
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
