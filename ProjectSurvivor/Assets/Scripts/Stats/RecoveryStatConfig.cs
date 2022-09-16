using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RecoveryStatConfig", menuName = "ScriptableObjects/Stats/Recovery Stat Config")]
public class RecoveryStatConfig : StatConfigureSO
{
    [Header("STAT ATTRIBUTE")]
    public float recoveryTime = 5f;
}
