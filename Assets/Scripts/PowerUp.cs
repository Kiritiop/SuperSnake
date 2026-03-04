using UnityEngine;

public abstract class PowerUp : MonoBehaviour, ICollectible
{
    public Vector2Int gridPosition;
    public float duration = 5f;

    // Each subclass MUST define what happens on collection
    public abstract void OnCollected(SnakeController snake);

    protected void SelfDestroy() => Destroy(gameObject);
}