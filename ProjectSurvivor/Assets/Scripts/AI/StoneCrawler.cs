using UnityEngine;

public class StoneCrawler : Enemy
{
    [Space(10)]
    [Header("VISUAL EFFECTS")]
    [SerializeField]
    private GameObject hitEffectPrefab;
    [SerializeField]
    private ParticleSystem[] leftFootDustEffects;
    [SerializeField]
    private ParticleSystem[] righttFootDustEffects;

    public override void StartAttackAnimationEvent()
    {
        attackTrigger.SetDamage(stats.attackDamage);
        attackTrigger.EnableTrigger();

        GameObject hitVFX = PoolManager.Instance.SpawnFromPool(hitEffectPrefab,  
        attackTrigger.transform.position + transform.forward * 1f, Quaternion.identity);
    }

    public override void StopAttackAnimationEvent()
    {
        attackTrigger.DisableTrigger();
    }

    public void LeftFootsDustEffect(){
        foreach(var effect in leftFootDustEffects){
            effect.Play();
        }
    }

    public void RightFootsDustEffect(){
        foreach(var effect in righttFootDustEffects){
            effect.Play();
        }
    }
}