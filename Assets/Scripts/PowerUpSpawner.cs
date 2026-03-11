using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;

    public float spawnInterval = 10f;

    private GameObject activePowerUp;

    void Start() => StartCoroutine(SpawnLoop());

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandom();
            yield return new WaitForSeconds(7f);
            if (activePowerUp != null)
            {
                Destroy(activePowerUp);
            }
        }
    }

    void SpawnRandom()
    {
        if (activePowerUp != null) 
        {
            Destroy(activePowerUp);
        }

        var snake = FindAnyObjectByType<SnakeController>();

        if (snake == null)
        {
            return;
        }

        Vector2Int pos = GridManager.Instance.GetRandomPosition(snake.GetBodyPositions());
        int i = Random.Range(0, powerUpPrefabs.Length);
        activePowerUp = Instantiate(powerUpPrefabs[i], new Vector3(pos.x, pos.y, 0), Quaternion.identity);

        PowerUp p = activePowerUp.GetComponent<PowerUp>();
        if (p == null) 
        {
            Debug.LogError($"{powerUpPrefabs[i].name} is missing its PowerUp script!"); 
            return; 
        }
        p.gridPosition = pos;
    }
}