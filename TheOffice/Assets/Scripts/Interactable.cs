using UnityEngine;

[RequireComponent(typeof(InteractableStatus))]
public class Interactable : MonoBehaviour
{
    public bool IsValid { get { return _status._isInteractable;  } }
    private InteractableStatus _status;

    public void Awake()
    {
        _status = GetComponent<InteractableStatus>();
    }

    virtual public void Interact(PlayerController controller)
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }

    virtual public void Interact(BotNavigation bot)
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }

    protected void SetInteractable(bool value) => _status._isInteractable = value;
}
