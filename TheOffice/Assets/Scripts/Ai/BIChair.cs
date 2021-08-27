using UnityEngine;

public class BIChair : BotInteractable
{
    [SerializeField] Vector3 offsetFromOrigin;

    private Vector3 trueOffset;
    private bool _isOnChair = false;

    private Quaternion _cachedRotation;

    protected override void Start()
    {
        base.Start();
        trueOffset = (offsetFromOrigin.x * transform.right) + (offsetFromOrigin.y * transform.up) + (offsetFromOrigin.z * transform.forward);
        _cachedRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y + 90, transform.localRotation.z);
    }

    protected override void BotInteract(BotBrain bot) 
    {
        if (!_isOnChair)
            bot.GetComponent<BotNavigation>().SitOnChair(transform.position + trueOffset, transform.right);
        else
            bot.GetComponent<BotNavigation>().StepOutOfChair();

        _isOnChair = !_isOnChair;
    }

    private void OnDrawGizmos()
    {
        trueOffset = (offsetFromOrigin.x * transform.right) + (offsetFromOrigin.y * transform.up) + (offsetFromOrigin.z * transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + trueOffset, .1f);
    }
}
