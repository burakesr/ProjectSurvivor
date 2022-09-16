using UnityEngine;

[CreateAssetMenu(fileName = "ChillEffect", menuName = "ScriptableObjects/Effects/Chill Effect")]
public class ChillEffect : EffectBaseSO
{
    [SerializeField]
    private float slowPercentage = 10f;

    private float defaultSpeed;
    
    public override void Enable(Enemy target)
    {
        defaultSpeed = target.GetAgent.speed;

        float speed = defaultSpeed - ((defaultSpeed * slowPercentage) / 100);
        target.GetAgent.speed = speed;
    }

    public override void Tick(Enemy target)
    {
        target.GetHealth.TakeDamage(damagePerSecond);
    }

    public override void Disable(Enemy target)
    {
        target.GetAgent.speed = defaultSpeed;
    }
}
