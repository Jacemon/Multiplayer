using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 28f;
    
    [Header("Ground Check")]
    [SerializeField]
    private float playerHeight;
    public float groundDrag = 7f;
    public LayerMask groundLayerMask;
    [SerializeField]
    private bool isGrounded;

    [Header("Jump")]
    public float airMultiplier = 0.7f;
    public float jumpForce;
    public float jumpCooldown;
    public bool readyToJump = true;
    
    [Header("Rotation")]
    public Transform mainCamera;
    public float rotationSensitivity = 1f;
    public float verticalLimit = 90f;

    [Header("Body parts")]
    public Transform head;
    
    [SerializeField]
    private GameInput gameInput;

    private Rigidbody _rigidbody;
    
    private float _rotationX;

    private const float GroundRayOffset = 0.2f;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerHeight = GetComponent<CapsuleCollider>().height;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(
            transform.position, 
            Vector3.down, 
            playerHeight * 0.5f + GroundRayOffset, 
            groundLayerMask);
        
        JumpUpdate();
        LookUpdate();
        DragUpdate();
    }

    private void MoveUpdate()
    {
        var inputMovement = gameInput.GetMovement();
        var moveDirection = transform.forward * inputMovement.y + transform.right * inputMovement.x;
        
        _rigidbody.AddForce(moveDirection.normalized * (moveSpeed * (isGrounded ? 1.0f : airMultiplier)), 
            ForceMode.Force);
    }

    private void DragUpdate()
    {
        _rigidbody.drag = isGrounded ? groundDrag : 0;
    }

    private void JumpUpdate()
    {
        if (!gameInput.GetJump() || !readyToJump || !isGrounded) return;

        Jump();
        
        Invoke(nameof(ResetJump), jumpCooldown);
    }
    
    private void Jump()
    {
        readyToJump = false;
        
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    
    private void LookUpdate()
    {
        var inputRotation = gameInput.GetMouseDeltaRotation();
        
        transform.Rotate(Vector3.up, inputRotation.x);

        _rotationX -= inputRotation.y;
        _rotationX = Mathf.Clamp(_rotationX, -verticalLimit, verticalLimit);

        var targetRotation = transform.eulerAngles;
        targetRotation.x = _rotationX;
        mainCamera.eulerAngles = targetRotation;
    }
}
