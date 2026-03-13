using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

    [SerializeField] private AudioClip EatSFX;

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
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (direction != Vector2Int.down)
            {
                nextDirection = Vector2Int.up;
            }
        }

        if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
        {    
            if (direction != Vector2Int.up) 
            {
                nextDirection = Vector2Int.down;
            }
        }

        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (direction != Vector2Int.right)
            {
                nextDirection = Vector2Int.left;
            }
        }

        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (direction != Vector2Int.left)
            {
                nextDirection = Vector2Int.right;
            }
        }
    }

    void Move()
    {
        direction = nextDirection;
        Vector2Int newHead = headPosition + direction;

        if (!GridManager.Instance.IsInsideBounds(newHead))
        {
            if (GameManager.Instance != null) 
            {
                GameManager.Instance.GameOver();
                return;
            }
        }

        for (int i = 0; i < bodyPositions.Count - 1; i++)
        {
            if (bodyPositions[i] == newHead)
            {
                if (GameManager.Instance != null) 
                {
                    GameManager.Instance.GameOver();
                    return;
                }
            }
        }

        bodyPositions.Insert(0, newHead);
        bodyPositions.RemoveAt(bodyPositions.Count - 1);
        headPosition = newHead;
        UpdateVisuals();
    }

    public void EatFood()
    {
        bodyPositions.Add(bodyPositions[bodyPositions.Count - 1]);
        AddBodySegment();
        SoundEffectManager.instance.PlaySoundEffect(EatSFX, transform, 0.75f);
        ScoreManager.Instance.AddScore(10);
    }

    void AddBodySegment()
    {
        Vector2Int lastPos = bodyPositions[bodyPositions.Count - 1];
        GameObject seg = Instantiate(bodyPrefab, new Vector3(lastPos.x, lastPos.y, 0), Quaternion.identity);
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

    public void SetMoveInterval(float val) => moveInterval = val;
}