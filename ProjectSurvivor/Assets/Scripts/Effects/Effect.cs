[System.Serializable]
public class Effect
{
    public float duration;
    public float elapsedTime;
    public float applyTime;
    public EffectStatusUI effectStatus;

    private EffectBaseSO effectBase;
    private Enemy effectedTarget;

    public Effect(EffectBaseSO effectBase, EffectStatusUI effectStatus)
    {
        this.effectBase = effectBase;
        duration = effectBase.effetDuration;
        this.effectStatus = effectStatus;
    }

    public void SetTarget(Enemy target)
    {
        effectedTarget = target;
    }

    public void Enable()
    {
        effectBase.Enable(effectedTarget);
    }

    public void Disable()
    {
        effectBase.Disable(effectedTarget);
    }

    public void Tick(float second)
    {
        duration -= second;

        effectBase.Tick(effectedTarget);
    }
}
