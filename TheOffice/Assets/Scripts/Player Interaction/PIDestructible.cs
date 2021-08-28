using UnityEngine;

public abstract class PIDestrutable : PlayerInteractable
{
    [SerializeField] protected int points = 0;
    public override void Interact(PlayerController player)
    {
        if (base.IsValid)
        {
            NotifyBotsInRange(transform.position, 3f);
            NotifyLastIntereactedBot();
            Destruct();
            base.SetInteractable(false);
            GameManager.Instance.UpdateScore(points);
        }
    }

    protected abstract void Destruct();

    private void NotifyBotsInRange(Vector3 position, float range)
    {
        Collider[] hitColliders = new Collider[10];
        int numColliders = Physics.OverlapSphereNonAlloc(position, range, hitColliders, LayerMask.GetMask("Bots"));
        for (int i = 0; i < numColliders; i++)
        {
            hitColliders[i].GetComponent<BotBrain>().GoChaos();
        }
    }

    private void NotifyLastIntereactedBot()
    {
        if (base.LastIntereactedBot)
            LastIntereactedBot.GoChaos();
    }

}
