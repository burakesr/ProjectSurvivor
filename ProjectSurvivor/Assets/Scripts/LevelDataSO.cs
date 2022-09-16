using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public AnimationCurve experienceLevelCurve;

    public UnityAction OnExperienceGained;
    public UnityAction OnLevelUp;
}