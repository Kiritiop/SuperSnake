using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    public GameObject foodPrefab;
    private GameObject currentFood;

    void Awake() => Instance = this;

    public void SpawnFood(List<Vector2Int> occupiedPositions)
    {
        if (currentFood != null) Destroy(currentFood);

        Vector2Int pos = GridManager.Instance.GetRandomPosition(occupiedPositions);
        currentFood = Instantiate(foodPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        currentFood.GetComponent<FoodItem>().gridPosition = pos;
    }
    public Vector2Int GetCurrentFoodPosition()
    {
    if (currentFood != null)
        return currentFood.GetComponent<FoodItem>().gridPosition;
    return new Vector2Int(-999, -999);
    }
}