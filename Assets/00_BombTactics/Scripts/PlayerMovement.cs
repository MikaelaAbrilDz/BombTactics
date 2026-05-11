using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    CharacterController controller;
    Vector3 movementInput;
    Animator anim;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim= GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        controller.SimpleMove(movementInput * speed);
        if (PlayerModeManager.currentMode == PlayerModeManager.PlayerMode.Player) transform.LookAt(transform.position + movementInput);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y).normalized;
        anim.SetBool("isRunning", (movementInput != Vector3.zero));
    }
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Killer"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.CompareTag("Win"))
        {
            SceneManager.LoadScene("Win");
        }
    }
}
