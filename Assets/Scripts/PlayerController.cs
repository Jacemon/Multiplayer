using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 28f;
    public float sprintSpeed = 42f;
    public float walkSpeed = 14f;
    
    [Header("Ground Check")]
    [SerializeField]
    public float groundRayOffset = 0.2f;
    public float groundDrag = 7f;
    public LayerMask groundLayerMask;
    public float groundTimeErrorRate = 0.1f;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float lastGroundedTime;

    [Header("Jump")]
    public float airMultiplier = 0.7f;
    public float jumpForce;
    public float jumpCooldown;
    public bool readyToJump = true;
    
    [Header("Rotation")]
    public Transform mainCamera;
    [Range(0.0f, 2.0f)]
    public float rotationSensitivity = 1f;
    public float verticalLimit = 90f;

    [Header("Body parts")]
    public Transform head;
    
    [Header("Special")]
    [SerializeField]
    private GameInput gameInput;

    private float _rotationX;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void Update()
    {
        GroundUpdate();
        
        JumpUpdate();
        LookUpdate();
        DragUpdate();
    }

    private void GroundUpdate()
    {
        var rayStart = transform.position;
        isGrounded = Physics.Raycast(
            rayStart, 
            Vector3.down, 
            groundRayOffset,
            groundLayerMask);
        
        Debug.DrawLine(rayStart, new Vector3(rayStart.x, rayStart.y - groundRayOffset, rayStart.z), Color.red);

        if (Time.time - lastGroundedTime >= groundTimeErrorRate)
        {
            _animator.SetBool(IsGrounded, isGrounded);
        }

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }
    
    private void MoveUpdate()
    {
        var movementType = gameInput.GetMovementType();
        
        var speed = movementType switch
        {
            GameInput.MovementType.Run => runSpeed,
            GameInput.MovementType.Walk => walkSpeed,
            GameInput.MovementType.Sprint => sprintSpeed,
            GameInput.MovementType.Idle => 0.0f,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var inputMovement = gameInput.GetMovement();
        var moveDirection = transform.forward * inputMovement.y + transform.right * inputMovement.x;
        
        _rigidbody.AddForce(moveDirection.normalized * (speed * (isGrounded ? 1.0f : airMultiplier)),
            ForceMode.Force);

        _animator.SetInteger(IsMoving, (int)movementType);
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
        
        _animator.SetTrigger(IsJumping);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    
    private void LookUpdate()
    {
        var inputRotation = gameInput.GetLook();
        inputRotation *= rotationSensitivity;
        
        transform.Rotate(Vector3.up, inputRotation.x);

        _rotationX -= inputRotation.y;
        _rotationX = Mathf.Clamp(_rotationX, -verticalLimit, verticalLimit);

        var targetRotation = transform.eulerAngles;
        targetRotation.x = _rotationX;
        mainCamera.eulerAngles = targetRotation;

        // TODO: not working
        head.eulerAngles = targetRotation;
    }
}
