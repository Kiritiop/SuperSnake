using UnityEngine;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    public GameObject foodPrefab;
    private GameObject currentFood;

    void Awake() => Instance = this;
    void Start() { }

    public void SpawnFood(List<Vector2Int> occupiedPositions)
    {
        if (currentFood != null) Destroy(currentFood);
        if (foodPrefab == null) { Debug.LogError("foodPrefab is not assigned!"); return; }

        Vector2Int pos = GridManager.Instance.GetRandomPosition(occupiedPositions);
        currentFood = Instantiate(foodPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        currentFood.GetComponent<FoodItem>().gridPosition = pos;
    }
    public Vector2Int GetCurrentFoodPosition()
    {
        if (currentFood == null) return new Vector2Int(-999, -999);
        FoodItem item = currentFood.GetComponent<FoodItem>();
        if (item == null) return new Vector2Int(-999, -999);
        return item.gridPosition;
    }
}