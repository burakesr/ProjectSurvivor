using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public EffectUIHolder effectUIHolder;

    public List<Effect> currentActiveEffects = new List<Effect>();

    private void OnEnable()
    {
        RemoveAllEffects();
    }

    private void Update()
    {
        if (currentActiveEffects.Count == 0) return;

        for (int i = 0; i < currentActiveEffects.Count; i++)
        {
            if (currentActiveEffects[i] == null) break;

            Effect effect = currentActiveEffects[i];
            if (effect.duration <= 0)
            {
                RemoveEffect(effect);
            }

            effect.elapsedTime += Time.deltaTime;
            if (effect.elapsedTime >= 1f)
            {
                effect.elapsedTime %= 1;
                effect.Tick(1);
            }
        }
    }

    public Effect AddEffect(EffectBaseSO effectBase)
    {
        var effectStatus = effectUIHolder.CreateEffectStatus(effectBase.effectIcon);
        var effect = new Effect(effectBase, effectStatus);

        currentActiveEffects.Add(effect);

        return effect;
    }

    public void RemoveEffect(Effect effect)
    {
        effect.Disable();
        effect.effectStatus.Disable();

        currentActiveEffects.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        for (int i = 0; i < currentActiveEffects.Count; i++)
        {
            if (currentActiveEffects[i] == null) break;

            currentActiveEffects.Remove(currentActiveEffects[i]);
        }
    }
}