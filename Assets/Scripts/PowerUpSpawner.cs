using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // assign in Inspector
    public float spawnInterval = 10f;

    private GameObject activePowerUp;

    void Start() => StartCoroutine(SpawnLoop());

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandom();
            yield return new WaitForSeconds(7f); // despawn after 7s
            if (activePowerUp != null) Destroy(activePowerUp);
        }
    }

    void SpawnRandom()
    {
        if (activePowerUp != null) Destroy(activePowerUp);
        var snake = FindAnyObjectByType<SnakeController>();
        Vector2Int pos = GridManager.Instance.GetRandomPosition(snake.GetBodyPositions());
        int i = Random.Range(0, powerUpPrefabs.Length);
        activePowerUp = Instantiate(powerUpPrefabs[i], new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        activePowerUp.GetComponent<PowerUp>().gridPosition = pos;
    }
}