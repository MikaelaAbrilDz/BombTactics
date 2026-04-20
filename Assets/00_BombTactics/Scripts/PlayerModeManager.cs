using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModeManager : MonoBehaviour
{
    PlayerInput input;
    public enum PlayerMode
    {
        Player, Bomb
    }
    public static PlayerMode currentMode;
    void Start()
    {
        currentMode = PlayerMode.Player;
        input = GetComponent<PlayerInput>();
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
