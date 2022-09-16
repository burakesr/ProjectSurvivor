using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public LevelDataSO levelData;

    private int totalExperience;
    private int currentLevel = 1;
    private int nextExperience;
    private int previousExperience;

    public int TotalExperience
    {
        get
        {
            return totalExperience;
        }
    }

    public int NextExperience
    {
        get
        {
            return nextExperience;
        }
    }

    public int GetCurrentLevel => currentLevel;


    private void Start()
    {        
        previousExperience = (int)levelData.experienceLevelCurve.Evaluate(currentLevel);
        nextExperience = (int)levelData.experienceLevelCurve.Evaluate(currentLevel + 1);

        AddExperience(0);
    }


    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AddExperience(100);
        }
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;

        if (totalExperience - previousExperience < 0)
        {
            currentLevel--;
        }
        if ((nextExperience - totalExperience) < 0)
        {
            currentLevel++;
        }
        if (currentLevel < 0)
        {
            currentLevel = 0;
        }
        if (totalExperience < 0)
        {
            totalExperience = 0;
        }

        previousExperience = (int)levelData.experienceLevelCurve.Evaluate(currentLevel);
        nextExperience = (int)levelData.experienceLevelCurve.Evaluate(currentLevel + 1);

        levelData.OnExperienceGained?.Invoke();
    }
    public float GetExperienceFraction()
    {
        int range = nextExperience - previousExperience;
        int cap = totalExperience - previousExperience;

        float value = (float)cap / range;

        return value;
    }
}
