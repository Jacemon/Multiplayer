using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    public Vector2 GetMovement()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDeltaRotation()
    {
        //return _playerInputActions.Player.Look.ReadValue<Vector2>();

        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool GetJump()
    {
        return !_playerInputActions.Player.Jump.ReadValue<float>().Equals(0);
    }
}
