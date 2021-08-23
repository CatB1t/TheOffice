using UnityEngine;

public abstract class PIDestrutable : PlayerInteractable
{
    public override void Interact(PlayerController player)
    {
        if (_isInteractable)
        {
            Destruct();
            base._isInteractable = false;
        }
    }

    protected virtual void Destruct() {}
}
