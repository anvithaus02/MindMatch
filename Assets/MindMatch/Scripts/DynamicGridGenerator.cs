using UnityEngine;

public class DynamicGridGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public int rows = 2;
    public int columns = 3;
    public float padding = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GenerateGrid();
        }
    }

    public void GenerateGrid()
    {
        RectTransform container = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float containerWidth = container.rect.width;
        float containerHeight = container.rect.height;

        float cellWidth = (containerWidth - (columns - 1) * padding) / columns;
        float cellHeight = (containerHeight - (rows - 1) * padding) / rows;
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        float totalGridWidth = columns * cellSize + (columns - 1) * padding;
        float totalGridHeight = rows * cellSize + (rows - 1) * padding;

        Vector2 startPos = new Vector2(-totalGridWidth / 2 + cellSize / 2, totalGridHeight / 2 - cellSize / 2);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject card = Instantiate(cardPrefab, transform);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellSize, cellSize);

                float posX = startPos.x + c * (cellSize + padding);
                float posY = startPos.y - r * (cellSize + padding);
                rt.localPosition = new Vector3(posX, posY, 0);
            }
        }
    }
}
