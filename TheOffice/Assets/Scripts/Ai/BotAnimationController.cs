using UnityEngine;

public class BotAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _botAnimator;

    public void SitOnChair(bool value)
    {
        _botAnimator.SetBool("Sit", value);
    }

    public void UpdatePlayerSpeed(float value)
    {
        _botAnimator.SetFloat("Speed", value);
    }

    public void GoChaos()
    {
        _botAnimator.SetBool("Chaos", true);
    }
}
