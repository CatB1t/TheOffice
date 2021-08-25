using UnityEngine;

[RequireComponent(typeof(InteractableStatus))]
public class Interactable : MonoBehaviour
{
    public bool IsValid { get { return _status._isInteractable;  } }
    public bool IsInUse { get { return _status._isInUse; } }

    private InteractableStatus _status;

    public void Awake()
    {
        _status = GetComponent<InteractableStatus>();
    }

    protected virtual void Start()
    {

    }

    virtual public void Interact(PlayerController controller)
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }

    virtual public void Interact(BotBrain bot)
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }

    protected void SetInteractable(bool value) => _status._isInteractable = value;
    protected void SetInUse(bool value) => _status._isInUse = value;
}
