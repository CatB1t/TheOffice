using UnityEngine;

public abstract class BotInteractable : Interactable
{
    public bool HasBot { get { return _botInteracting; } }
    private BotBrain _botInteracting;

    public override void Interact(BotBrain bot)
    {
        if (base.IsValid)
        {
            BotInteract(bot);
        }
        else
        {
            bot.GoChaos();
        }
        Debug.Log("i'm Bot john interacting");
    }

    protected virtual void BotInteract(BotBrain bot)
    {
        if (!_botInteracting)
            _botInteracting = bot;
        else if (_botInteracting == bot)
            _botInteracting = null;
        else
            return;
    }
}
