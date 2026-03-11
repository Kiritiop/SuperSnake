using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public Vector2Int gridPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
        {
            return;
        }
        
        SnakeController snake = other.GetComponent<SnakeController>();
        
        if (snake == null) 
        {
            return;
        }

        snake.EatFood();
        FoodSpawner.Instance.SpawnFood(snake.GetBodyPositions());
        Destroy(gameObject);
    }
}