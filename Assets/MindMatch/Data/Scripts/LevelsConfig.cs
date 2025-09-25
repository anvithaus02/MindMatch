using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "MindMatch/LevelsConfig", order = 2)]
public class LevelsConfig : ScriptableObject
{
    [Header("All Levels")]
    [SerializeField] private List<LevelData> _levels = new List<LevelData>();

    public LevelData GetLevel(int index)
    {
        return _levels[index];
    }

    public List<LevelData> GetAllLevels()
    {
        return _levels;
    }
}

[System.Serializable]
public class LevelData
{
    [Header("Level Info")]
    public int LevelNumber = 1;
    public Category Category;

    [Header("Grid Settings")]
    public int Rows = 2;
    public int Columns = 2;
}
