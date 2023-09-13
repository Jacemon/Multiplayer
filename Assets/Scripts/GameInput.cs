using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public enum MovementType
    {
        Idle,
        Walk,
        Run,
        Sprint
    }

    public enum ArmsState
    {
        Hands,
        Knife,
        Pistol,
        Rifle
    }

    public static Action onAttack;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Attack.performed += DoAttack;
        _playerInputActions.Player.Attack.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }
    
    public Vector2 GetMovement()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public MovementType GetMovementType()
    {
        if (_playerInputActions.Player.Move.ReadValue<Vector2>().Equals(Vector2.zero)) return MovementType.Idle;
        if (!_playerInputActions.Player.Walk.ReadValue<float>().Equals(0)) return MovementType.Walk;
        if (!_playerInputActions.Player.Sprint.ReadValue<float>().Equals(0)) return MovementType.Sprint;
        return MovementType.Run;
    }
    
    public void DoAttack(InputAction.CallbackContext obj)
    {
        onAttack.Invoke();
    }
    
    public bool GetJump()
    {
        return !_playerInputActions.Player.Jump.ReadValue<float>().Equals(0);
    }
    
    public Vector2 GetLook()
    {
        return _playerInputActions.Player.Look.ReadValue<Vector2>();
    }
    
    public bool GetAttack()
    {
        return !_playerInputActions.Player.Attack.ReadValue<float>().Equals(0);
    }

    public bool GetAim()
    {
        return !_playerInputActions.Player.Aim.ReadValue<float>().Equals(0);
    }
    
    public int GetSwitch()
    {
        if (_playerInputActions.Player.Change.ReadValue<float>() < 0)
        {
            return -1;
        }
        if (_playerInputActions.Player.Change.ReadValue<float>() > 0)
        {
            return 1;
        }

        return 0;
    }
}
