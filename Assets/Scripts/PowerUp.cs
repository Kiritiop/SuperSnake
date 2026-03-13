using UnityEngine;

public abstract class PowerUp : MonoBehaviour, ICollectible
{
    public Vector2Int gridPosition;

    public float duration = 5f;

    [SerializeField] private AudioClip PowerupSFX;

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

         SoundEffectManager.instance.PlaySoundEffect(PowerupSFX, transform, 0.75f);
        
        OnCollected(snake);
    }
    protected void SelfDestroy()
    {
        Destroy(gameObject);
    }
}