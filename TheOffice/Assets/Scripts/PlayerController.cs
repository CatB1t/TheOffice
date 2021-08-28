using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsMoving { get { return _currentMovementSpeed > .1;  } }
    public bool CanBeCaught {  get { return _playerCanBeCaught;  } }

    [SerializeField] private Transform playerCamera;
    [Header("Mouse")]
    [SerializeField] private float mouseSenstivity;
    [Header("Player Movement")]
    [SerializeField] private float speed;

    private CharacterController _characterController;
    private float xRotation = 0f;

    public float gravity = -9.18f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    private bool canMoveAround = true;
    private bool _playerCanBeCaught = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseLook();
        if (canMoveAround)
            Move();
    }

    private float _currentMovementSpeed = 0;

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 8f;
        }
        else
        {
            speed = 6f;
        }

        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 movement = (m_Input.x * transform.right) + (m_Input.z * transform.forward);
        _currentMovementSpeed = m_Input.sqrMagnitude;

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        _characterController.Move(movement * Time.deltaTime * speed);
        velocity.y += gravity * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private Vector3 positionBeforeSit;

    public void SitOnChair(Vector3 seatPoint)
    {
        _characterController.enabled = false;
        canMoveAround = false;
        positionBeforeSit = transform.position;
        transform.position = seatPoint;
        _playerCanBeCaught = false;
        _currentMovementSpeed = 0;
    }

    public void GetUpFromChair()
    {
        transform.position = positionBeforeSit;
        canMoveAround = true;
        _characterController.enabled = true;
        _playerCanBeCaught = true;
    }

}
