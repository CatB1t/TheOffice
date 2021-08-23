using UnityEngine;

public abstract class PIDestrutable : PlayerInteractable
{
    public override void Interact(PlayerController player)
    {
        if (base.IsValid)
        {
            Destruct();
            base.SetInteractable(false);
        }
    }

    protected virtual void Destruct() {}
}
