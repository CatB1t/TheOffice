using System.Collections;
using UnityEngine;

public class PlayerChair : PlayerInteractable
{

    [SerializeField] Vector3 offsetFromOrigin;
    private PlayerController _playerController;
    private bool _playerOnSeat = false;

    private void Start()
    {
        base._displayMessage = "Sit";
    }

    public override void Interact(PlayerController controller)
    {
        _playerController = controller;
        controller.SitOnChair(transform.position + offsetFromOrigin);
        StartCoroutine(SetSeatFlag());
    }
    IEnumerator SetSeatFlag()
    {
        yield return new WaitForSeconds(0.2f);
        _playerOnSeat = true;
    }

    public void Update()
    {
        if (_playerOnSeat && Input.GetKeyDown(KeyCode.E))
            {
                _playerController.GetUpFromChair();
                _playerController = null;
                _playerOnSeat = false;
            }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offsetFromOrigin, .1f);
    }
}
