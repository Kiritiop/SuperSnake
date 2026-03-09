using System.Collections;
using UnityEngine;

public class ScoreMultiplier : PowerUp
{
    public override void OnCollected(SnakeController snake)
    {
        snake.StartCoroutine(ApplyEffect(snake));
        SelfDestroy();
    }

    IEnumerator ApplyEffect(SnakeController snake)
    {
        ScoreManager.Instance.SetMultiplier(2);
        yield return new WaitForSeconds(duration);
        ScoreManager.Instance.ResetMultiplier();
    }
}