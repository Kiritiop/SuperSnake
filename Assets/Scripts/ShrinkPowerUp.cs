public class ShrinkPowerUp : PowerUp
{
    public override void OnCollected(SnakeController snake)
    {
        snake.Shrink(3);
        SelfDestroy();
    }
}