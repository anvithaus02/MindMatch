using System.Collections.Generic;
using MindMatch.Core;
using UnityEngine;

public class CardImageProvider
{
    private CategoryData _categoryData;

    public CardImageProvider(CategoryData categoryData)
    {
        _categoryData = categoryData;
    }

    public List<Sprite> GetShuffledPairs(Category category, int totalCards)
    {
        List<Sprite> available = _categoryData.GetAllImages(category);

        if (available.Count < totalCards / 2)
        {
            Debug.LogError($"Not enough images in category {category}.");
            return new List<Sprite>();
        }

        List<Sprite> chosen = new List<Sprite>(Utility.ShuffleList(available)); 
        chosen = chosen.GetRange(0, totalCards / 2);                             

        List<Sprite> cardImages = new List<Sprite>();
        foreach (var img in chosen)
        {
            cardImages.Add(img);
            cardImages.Add(img);
        }

        return Utility.ShuffleList(cardImages); 
    }
}
