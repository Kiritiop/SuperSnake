using UnityEngine;

public abstract class PowerUp : MonoBehaviour, ICollectible
{
    public Vector2Int gridPosition;

    public float duration = 5f;

    public abstract void OnCollected(SnakeController snake);

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
        
        OnCollected(snake);
    }
    protected void SelfDestroy() => Destroy(gameObject);
}