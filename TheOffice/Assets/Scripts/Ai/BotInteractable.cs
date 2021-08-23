using UnityEngine;

public abstract class BotInteractable : Interactable
{
   public override void Interact(BotNavigation bot)
    {
        if(base.IsValid)
            BotInteract();
    }

    protected virtual void BotInteract() {}
}
