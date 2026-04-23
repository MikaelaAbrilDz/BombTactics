using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModeManager : MonoBehaviour
{
    public static PlayerInput input;
    public enum PlayerMode
    {
        Player, Bomb
    }
    public static PlayerMode currentMode;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentMode = PlayerMode.Player;
        input = GetComponent<PlayerInput>();
        input.neverAutoSwitchControlSchemes = true;
        currentMode = PlayerMode.Player;
        input.SwitchCurrentActionMap("Player");
    }
    public void OnChangeToBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentMode = PlayerMode.Bomb;
            input.SwitchCurrentActionMap("Bomb");
        }
    }
    public void OnChangeToPlayer(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentMode = PlayerMode.Player;
            input.SwitchCurrentActionMap("Player");
        }
    }
}
