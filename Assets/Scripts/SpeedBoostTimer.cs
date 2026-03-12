using Unity.VisualScripting;
using UnityEngine;

public class SpeedBoostTimer : MonoBehaviour
{
    private float startTime;
    // Update is called once per frame

    void Update()
    {
        // Increase scale uniformly over time
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;

        // Reset scale if too big
        if (transform.localScale.x > 3f)
            transform.localScale = Vector3.one;
    }
    void startTimer(float duration)
    {
        startTime = Time.deltaTime;
        float scale = (Time.deltaTime - startTime) / duration;

    }
}
