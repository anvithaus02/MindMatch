using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    Animals,
    Fruits,
    Vehicles
}

[System.Serializable]
public class CategoryEntry
{
    public Category category;
    public List<Sprite> images = new List<Sprite>();
}

[CreateAssetMenu(fileName = "CategoryData", menuName = "MindMatch/Category Data", order = 1)]
public class CategoryData : ScriptableObject
{
    [Header("All Categories and Images")]
    public List<CategoryEntry> categories = new List<CategoryEntry>();

    public List<Sprite> GetAllImages(Category category)
    {
        var entry = categories.Find(c => c.category == category);
        return entry != null ? entry.images : new List<Sprite>();
    }
}
