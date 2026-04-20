using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    CharacterController controller;
    Vector3 movementInput;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        controller.SimpleMove(movementInput * speed);
        if (PlayerModeManager.currentMode == PlayerModeManager.PlayerMode.Player) transform.LookAt(transform.position + movementInput);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y).normalized;
    }
}
