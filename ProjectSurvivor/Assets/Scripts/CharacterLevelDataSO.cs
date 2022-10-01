using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CharacterLevelData", menuName = "ScriptableObjects/Level/Character LevelData")]
public class CharacterLevelDataSO : ScriptableObject
{
    public AnimationCurve experienceLevelCurve;

    public UnityAction OnExperienceGained;
    public UnityAction OnLevelUp;
}