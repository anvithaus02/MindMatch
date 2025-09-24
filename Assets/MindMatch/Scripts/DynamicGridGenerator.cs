using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGridGenerator : MonoBehaviour
{
    public GameObject cardPrefab;    // Card prefab
    public int rows = 2;
    public int columns = 5;
    public float padding = 5f;       // Space between cards

    private void Update()
    {
        // Test trigger: generate grid when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed: generating grid for testing...");
            GenerateGrid();
        }
    }

    public void GenerateGrid()
    {
        if (cardPrefab == null)
        {
            Debug.LogError("Assign a cardPrefab!");
            return;
        }

        RectTransform container = GetComponent<RectTransform>();
        if (container == null)
        {
            Debug.LogError("Attach this script to a UI container with RectTransform!");
            return;
        }

        // Clear existing cards
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float containerWidth = container.rect.width;
        float containerHeight = container.rect.height;

        float cellWidth = (containerWidth - (columns - 1) * padding) / columns;
        float cellHeight = (containerHeight - (rows - 1) * padding) / rows;

        Vector2 startPos = new Vector2(-containerWidth / 2 + cellWidth / 2, containerHeight / 2 - cellHeight / 2);

        // Instantiate cards in rows and columns
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject card = Instantiate(cardPrefab, transform);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellWidth, cellHeight);

                float posX = startPos.x + c * (cellWidth + padding);
                float posY = startPos.y - r * (cellHeight + padding);
                rt.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}
