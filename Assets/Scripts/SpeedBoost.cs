using System.Collections;
using UnityEngine;

public class SpeedBoost : PowerUp
{
    public override void OnCollected(SnakeController snake)
    {
        snake.StartCoroutine(ApplyEffect(snake));
        SelfDestroy();
    }

    IEnumerator ApplyEffect(SnakeController snake)
    {
        float original = snake.moveInterval;
        snake.SetMoveInterval(original *1.5f);
        SpeedBoostTimer.Instance.StartTimer(5000);
        yield return new WaitForSeconds(duration);
        snake.SetMoveInterval(original);
    }
}