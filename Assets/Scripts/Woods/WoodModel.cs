using UnityEngine;
public class WoodModel : MonoBehaviour
{
    private void Start()
    {
        Knife.HideWood += Hide;
        ButtonManager.Instance.ShoowWood += Show;
    }
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

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Knife.HideWood -= Hide;
        ButtonManager.Instance.ShoowWood -= Show;
    }
}
