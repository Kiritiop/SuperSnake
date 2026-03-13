using UnityEngine;

public class SpeedBoostTimer : MonoBehaviour
{
    public static SpeedBoostTimer Instance;

    private float endTime = 0f;
    private float duration = 1f;
    private bool active = false;
    private float fullWidth;

    void Awake()
    {
        Instance = this;
        fullWidth = transform.localScale.x;
    }

    void Update()
    {
        if (!active) return;

        float remaining = endTime - Time.time;

        if (remaining <= 0f)
        {
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            active = false;
            return;
        }

        float scale = remaining / duration;
        transform.localScale = new Vector3(fullWidth * scale, transform.localScale.y, transform.localScale.z);
    }

    public void StartTimer(float seconds)
    {
        duration = seconds;
        endTime = Time.time + seconds;
        active = true;
        transform.localScale = new Vector3(fullWidth, transform.localScale.y, transform.localScale.z);
    }
}