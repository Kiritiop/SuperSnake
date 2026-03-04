using System.Collections;
using UnityEngine;

public class ScoreMultiplier : PowerUp
{
    public override void OnCollected(SnakeController snake)
    {
        StartCoroutine(ApplyEffect());
        SelfDestroy();
    }

    IEnumerator ApplyEffect()
    {
        ScoreManager.Instance.SetMultiplier(2);
        yield return new WaitForSeconds(duration);
        ScoreManager.Instance.ResetMultiplier();
    }
}