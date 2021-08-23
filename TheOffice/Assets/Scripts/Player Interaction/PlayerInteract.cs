using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactableObjectsMask;
    [SerializeField] private float interactionMaxDistance;

    private PlayerController _playerController;
    bool isInteractable = false;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionMaxDistance, interactableObjectsMask))
        {
            UserInterfaceManager.Instance.UpdateInteractionText(hit.collider.GetComponent<PlayerInteractable>().DisplayMessage);
            isInteractable = true;
        }
        else
        {
            UserInterfaceManager.Instance.UpdateInteractionText("");
            isInteractable = false;
        }

        if(isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            // TODO optimize reference
            hit.collider.GetComponent<PlayerInteractable>().Interact(_playerController);
        }

    }

}
