using UnityEngine;

public class BotAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _botAnimator;

    public void SitOnChair(bool value)
    {
        _botAnimator.SetBool("Sit", value);
    }

    private float lastSpeed; 

    public void UpdatePlayerSpeed(float value)
    {
        _botAnimator.SetFloat("Speed", value);
    }
}
