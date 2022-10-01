using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public string levelName;
    public Sprite levelIcon;
    public List<EnemyStatsConfigSO> enemies;
    public List<MaterialSO> availableMaterials;
    public SceneIndexes levelScene;
}
