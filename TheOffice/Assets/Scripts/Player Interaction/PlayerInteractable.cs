using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour
{
    public bool IsValid { get { return _isInteractable; } }
    public string DisplayMessage { get { return _displayMessage; } }
    protected string _displayMessage = "Destroy";
    protected bool _isInteractable = true;

    virtual public void Interact(PlayerController controller) 
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }
}
