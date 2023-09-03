using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls playerControls;

    public override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    #region Movement
    public Vector2 GetMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool GetPlayerJump()
    {
        return playerControls.Player.Jump.triggered;
    }
    public bool GetPlayerSprint()
    {
        return (playerControls.Player.Sprint.activeControl != null) ? true : false;
    }
    public bool PlayerDashed()
    {
        return playerControls.Player.Dash.triggered;
    }
    #endregion

    #region General
    public bool PlayerInteracted()
    {
        return playerControls.Player.Interact.triggered;
    }
    public bool ToggleScanner()
    {
        return playerControls.Player.Scanner.triggered;
    }

    #endregion

    #region Weapons
    public bool PlayerShoot()
    {
        return playerControls.Player.Shoot.triggered;
    }
    public bool PlayerAimed()
    {
        return playerControls.Player.Aim.triggered;
    }
    public int PlayerSwitchWeapon()
    {
        return playerControls.Player.SwitchWeapon.ReadValue<int>();
    }
    #endregion

}
