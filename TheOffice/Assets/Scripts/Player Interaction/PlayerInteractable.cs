using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour
{
    public string DisplayMessage { get { return _displayMessage; } }
    protected string _displayMessage = "Use";

    virtual public void Interact(PlayerController controller) 
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }
}
