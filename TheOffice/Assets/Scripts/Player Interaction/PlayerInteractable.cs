using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour
{
    virtual public void Interact(PlayerController controller) 
    {
#if UNITY_EDITOR
        Debug.LogError("Interactable object is not implemented");
#endif
    }
}
