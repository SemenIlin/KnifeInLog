using UnityEngine;

public class WoodModel : MonoBehaviour
{
    public float Speed { get; set; }
    public int Firmness { get; private set; }
    public float ChangeCreateApple { get; private set; }
    public float TimerForChangeDirection{ get; private set; }
    public void Initialize(float speed, int firmness, float changeCreateApple, float timerForChangeDirection)
    {
        Speed = speed;
        Firmness = firmness;
        ChangeCreateApple = changeCreateApple;
        TimerForChangeDirection = timerForChangeDirection;
    }
}
