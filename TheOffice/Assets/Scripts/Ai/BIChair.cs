using UnityEngine;

public class BIChair : BotInteractable
{
    [SerializeField] Vector3 offsetFromOrigin;
    [SerializeField] Quaternion sitRotation;

    private Vector3 trueOffset;

    private void Start()
    {
        // TODO still causes bugs?
        trueOffset = (offsetFromOrigin.x * transform.forward) + (offsetFromOrigin.y * transform.up) + (offsetFromOrigin.z * transform.right);
    }

    protected override void BotInteract(BotBrain bot) 
    {
        bot.GetComponent<BotNavigation>().SitOnChair(transform.position + trueOffset, transform.localRotation);
    }

    private void OnDrawGizmos()
    {
        trueOffset = (offsetFromOrigin.x * transform.right) + (offsetFromOrigin.y * transform.up) + (offsetFromOrigin.z * transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + trueOffset, .1f);
    }
}
