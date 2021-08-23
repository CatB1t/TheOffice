using UnityEngine;

public abstract class PlayerInteractable : Interactable
{
    public string DisplayMessage { get { return _displayMessage; } }
    protected string _displayMessage = "Destroy";

}
