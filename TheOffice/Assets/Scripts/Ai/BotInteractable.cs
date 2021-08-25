using UnityEngine;

public abstract class BotInteractable : Interactable
{
    public bool HasBot { get { return _botInteracting; } }
    private BotBrain _botInteracting = null;

    public override void Interact(BotBrain bot)
    { 

        Debug.Log("i'm Bot john tryna interact");

        if (base.IsValid)
        {

            if (_botInteracting == null)
            {
                _botInteracting = bot;
                base.SetInUse(true);
            }
            else if (_botInteracting == bot)
            {
                _botInteracting = null;
                base.SetInUse(false);
            }
            else if(bot != _botInteracting)
               return;

            BotInteract(bot);
        }
        else if(!base.IsValid && !base.IsInUse)
        {
            bot.GoChaos();
        }

    }

    protected virtual void BotInteract(BotBrain bot)
    {
      
    }
}
