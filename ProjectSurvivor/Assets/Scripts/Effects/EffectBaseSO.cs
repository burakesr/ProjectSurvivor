using UnityEngine;

public abstract class EffectBaseSO : ScriptableObject
{
    public EffectType effectType;

    public int effetDuration;
    public int damagePerSecond;
    public Sprite effectIcon;

    
    public abstract void Enable(Enemy target);
    public abstract void Tick(Enemy target);
    public abstract void Disable(Enemy target);
}
