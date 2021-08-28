using UnityEngine;

public class InteractableStatus : MonoBehaviour
{
    [HideInInspector] public bool _isInteractable = true;
    [HideInInspector] public bool _isInUse = false;
    [HideInInspector] public BotBrain _lastIntereactedBot;
}
