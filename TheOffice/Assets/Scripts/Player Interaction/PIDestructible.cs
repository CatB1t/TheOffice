using UnityEngine;

public abstract class PIDestrutable : PlayerInteractable
{
    [SerializeField] protected int points = 0;
    public override void Interact(PlayerController player)
    {
        if (base.IsValid)
        {
            Destruct();
            base.SetInteractable(false);
            GameManager.Instance.UpdateScore(points);
        }
    }

    protected virtual void Destruct() {}
}
