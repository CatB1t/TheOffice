using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactablePlayerMask;
    [SerializeField] private float interactionMaxDistance;

    private PlayerController _playerController;
    private bool foundInteractableObject = false;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        PlayerInteractable cachedReference;

        if(Physics.Raycast(ray, out hit, interactionMaxDistance, interactablePlayerMask) && hit.collider.GetComponent<PlayerInteractable>().IsValid)
        {
            cachedReference = hit.collider.GetComponent<PlayerInteractable>();

            if(cachedReference.IsValid)
            { 
                UserInterfaceManager.Instance.UpdateInteractionText(cachedReference.DisplayMessage);
                foundInteractableObject = true;
            }

        }
        else
        {
            UserInterfaceManager.Instance.UpdateInteractionText("");
            foundInteractableObject = false;
        }

        if(foundInteractableObject && Input.GetKeyDown(KeyCode.E))
        {
            // TODO optimize reference
            hit.collider.GetComponent<PlayerInteractable>().Interact(_playerController);
            Debug.Log("Interacting");
        }

    }

}
