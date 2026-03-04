using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 20;
    public int height = 20;

    void Awake()
    {
        Instance = this;
    }

    public Vector2Int GetRandomPosition(List<Vector2Int> occupiedPositions)
    {
        List<Vector2Int> available = new List<Vector2Int>();

        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -height / 2; y < height / 2; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (!occupiedPositions.Contains(pos))
                    available.Add(pos);
            }
        }

        return available[Random.Range(0, available.Count)];
    }

    public bool IsInsideBounds(Vector2Int pos)
    {
        return pos.x >= -width / 2 && pos.x < width / 2 &&
               pos.y >= -height / 2 && pos.y < height / 2;
    }
}