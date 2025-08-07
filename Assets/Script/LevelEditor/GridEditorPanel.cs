using UnityEngine;

public class GridEditorPanel : MonoBehaviour
{
    public TileSelectorPanel tileSelectorPanel;
    public Transform container;
    public GridTile gridTileTemplate;
    public float cellSize;
    public float gap;
    public GridTile[,] gridTiles;

    private void Start()
    {
        int[,] data = TempDataHandler.Get<int[,]>("CurrentLevelData");
        if (data != null)
            LoadLevelData(data);

    }

    public void SetGridSize(int row, int col)
    {
        if (gridTiles != null)
        {
            for (int i = 0; i < gridTiles.GetLength(0); i++)
            {
                for (int j = 0; j < gridTiles.GetLength(1); j++)
                {
                    if (gridTiles[i, j] != null)
                    {
                        DestroyImmediate(gridTiles[i, j].gameObject);
                    }
                }
            }
        }

        gridTiles = new GridTile[row, col];

        float totalWidth = col * cellSize + (col - 1) * gap;
        float totalHeight = row * cellSize + (row - 1) * gap;

        float offsetX = -totalWidth / 2f + cellSize / 2f;
        float offsetY = totalHeight / 2f - cellSize / 2f;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                float posX = offsetX + j * (cellSize + gap);
                float posY = offsetY - i * (cellSize + gap);

                Vector3 position = new Vector3(posX, posY, 0);

                gridTiles[i, j] = Instantiate(gridTileTemplate, container.position + position, Quaternion.identity, container);

                RectTransform rectTransform = gridTiles[i, j].GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.sizeDelta = new Vector2(cellSize, cellSize);
                }

                var cellType = CellType.Empty;
                if (i == 0 || j == 0 || i == row - 1 || j == col - 1)
                    cellType = CellType.Obstacle;

                gridTiles[i, j].SetValue(i, j, cellType, tileSelectorPanel);
            }
        }

    }

    public int[,] GetLevelData()
    {
        int[,] data = new int[gridTiles.GetLength(0), gridTiles.GetLength(1)];
        for (int i = 0; i < gridTiles.GetLength(0); i++)
        {
            for (int j = 0; j < gridTiles.GetLength(1); j++)
            {
                data[i, j] = (int)gridTiles[i, j].Type;
            }
        }
        return data;
    }

    public void LoadLevelData(int[,] data)
    {
        SetGridSize(data.GetLength(0), data.GetLength(1));
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {

                gridTiles[i, j].SetCellType((CellType)data[i, j]);
            }
        }
    }

}
