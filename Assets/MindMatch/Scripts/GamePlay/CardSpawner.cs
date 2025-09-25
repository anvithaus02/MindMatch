using System.Collections.Generic;
using MindMatch.Core;
using UnityEngine;

namespace MindMatch.Gameplay
{
    public class SpawnData
    {
        public float cellSize;
        public Vector2 startPos;
        public float padding;

        public SpawnData(float cellSize, Vector2 startPos, float padding)
        {
            this.cellSize = cellSize;
            this.startPos = startPos;
            this.padding = padding;
        }
    }
    public static class CardSpawner
    {
        private const int DefaultPadding = 5;
        public static void SpawnCards(MindCard cardPrefab, CategoryData categoryData, RectTransform parent, LevelData levelData)
        {
            var cardImages = GetShuffledPairs(categoryData, levelData.Category, levelData.Rows * levelData.Columns);

            SpawnData spawnData = CardGridCalculator.GetCardSpawnPositions(parent, levelData.Rows, levelData.Columns, DefaultPadding);

            int index = 0;
            for (int r = 0; r < levelData.Rows; r++)
            {
                for (int c = 0; c < levelData.Columns; c++)
                {
                    MindCard card = Object.Instantiate(cardPrefab, parent.transform);
                    RectTransform rt = card.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(spawnData.cellSize, spawnData.cellSize);
                    rt.localPosition = new Vector3(
                        spawnData.startPos.x + c * (spawnData.cellSize + spawnData.padding),
                        spawnData.startPos.y - r * (spawnData.cellSize + spawnData.padding),
                        0);

                    card.SetImage(cardImages[index]);
                    index++;
                }
            }
        }

        private static List<Sprite> GetShuffledPairs(CategoryData categoryData, Category category, int totalCards)
        {
            List<Sprite> available = categoryData.GetAllImages(category);

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
}