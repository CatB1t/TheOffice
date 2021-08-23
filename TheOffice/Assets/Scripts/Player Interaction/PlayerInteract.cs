using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Text _textObject;

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

        if(Physics.Raycast(ray, out hit,  interactionMaxDistance, interactableObjectsMask))
        {
            _textObject.gameObject.SetActive(true);
            isInteractable = true;
        }
        else
        {
            _textObject.gameObject.SetActive(false);
            isInteractable = false;
        }

        if(isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            hit.collider.GetComponent<PlayerInteractable>().Interact(_playerController);
        }

    }
}
