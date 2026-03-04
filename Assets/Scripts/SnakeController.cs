using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bodyPrefab;

    [Header("Movement")]
    public float moveInterval = 0.2f;

    private Vector2Int direction = Vector2Int.right;
    private Vector2Int nextDirection = Vector2Int.right;
    private List<Vector2Int> bodyPositions = new List<Vector2Int>();
    private List<GameObject> bodyObjects = new List<GameObject>();
    private Vector2Int headPosition;

    private float timer;

    void Start()
    {
        headPosition = Vector2Int.zero;
        bodyPositions.Add(headPosition);
        transform.position = new Vector3(headPosition.x, headPosition.y, 0);

        FoodSpawner.Instance.SpawnFood(bodyPositions);
    }

    void Update()
    {
        HandleInput();

        timer += Time.deltaTime;
        if (timer >= moveInterval)
        {
            timer = 0;
            Move();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (direction != Vector2Int.down) nextDirection = Vector2Int.up;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (direction != Vector2Int.up) nextDirection = Vector2Int.down;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            if (direction != Vector2Int.right) nextDirection = Vector2Int.left;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            if (direction != Vector2Int.left) nextDirection = Vector2Int.right;
    }

    void Move()
    {
        direction = nextDirection;
        Vector2Int newHead = headPosition + direction;

        // Wall collision
        if (!GridManager.Instance.IsInsideBounds(newHead))
        {
            GameManager.Instance.GameOver();
            return;
        }

        // Self collision (skip tail tip — it will move away)
        for (int i = 0; i < bodyPositions.Count - 1; i++)
        {
            if (bodyPositions[i] == newHead)
            {
                GameManager.Instance.GameOver();
                return;
            }
        }

        // Check food
        bool ate = (newHead == FoodSpawner.Instance.GetCurrentFoodPosition());
        PowerUp[] powerUps = FindObjectsByType<PowerUp>(FindObjectsSortMode.None);
        foreach (var p in powerUps)
        {
            if (p.gridPosition == newHead)
                p.OnCollected(this);
        }

        // Move body
        bodyPositions.Insert(0, newHead);
        if (!ate) bodyPositions.RemoveAt(bodyPositions.Count - 1);
        else
        {
            AddBodySegment();
            ScoreManager.Instance.AddScore(10);
            FoodSpawner.Instance.SpawnFood(bodyPositions);
        }

        headPosition = newHead;
        UpdateVisuals();
    }

    void AddBodySegment()
    {
        GameObject seg = Instantiate(bodyPrefab);
        bodyObjects.Add(seg);
    }

    void UpdateVisuals()
    {
        transform.position = new Vector3(bodyPositions[0].x, bodyPositions[0].y, 0);

        for (int i = 0; i < bodyObjects.Count; i++)
        {
            int posIndex = i + 1;
            if (posIndex < bodyPositions.Count)
                bodyObjects[i].transform.position = new Vector3(
                    bodyPositions[posIndex].x,
                    bodyPositions[posIndex].y, 0);
        }
    }

    public List<Vector2Int> GetBodyPositions() => bodyPositions;

    // Called by power-ups
    public void SetMoveInterval(float val) => moveInterval = val;

    public void Shrink(int amount)
    {
        for (int i = 0; i < amount && bodyObjects.Count > 0; i++)
        {
            Destroy(bodyObjects[bodyObjects.Count - 1]);
            bodyObjects.RemoveAt(bodyObjects.Count - 1);
            bodyPositions.RemoveAt(bodyPositions.Count - 1);
        }
    }
}