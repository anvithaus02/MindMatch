using UnityEngine;

namespace MindMatch.Gameplay
{
    public static class CardGridCalculator
    {
        public static SpawnData GetCardSpawnPositions(RectTransform container, int rows, int columns, float padding)
        {
            float containerWidth = container.rect.width;
            float containerHeight = container.rect.height;

            float cellWidth = (containerWidth - (columns - 1) * padding) / columns;
            float cellHeight = (containerHeight - (rows - 1) * padding) / rows;
            float cellSize = Mathf.Min(cellWidth, cellHeight);

            float totalGridWidth = columns * cellSize + (columns - 1) * padding;
            float totalGridHeight = rows * cellSize + (rows - 1) * padding;

            Vector2 startPos = new Vector2(-totalGridWidth / 2 + cellSize / 2, totalGridHeight / 2 - cellSize / 2);

            SpawnData spawnData = new SpawnData(cellSize, startPos, padding);

            return spawnData;
        }

    }
}