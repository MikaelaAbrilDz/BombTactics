using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    float yRotation;
    LineRenderer lineRenderer;
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerModeManager.currentMode == PlayerModeManager.PlayerMode.Bomb)
        {
            transform.eulerAngles = new Vector3(0, yRotation, 0);
            DrawLine();
        } 
    }
    void DrawLine()
    {
        lineRenderer.positionCount = 0;
    }
    public void OnChangeDirection(InputAction.CallbackContext context)
    {
        yRotation += context.ReadValue<Vector2>().x;
    }
    public void OnChangeToBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            yRotation = transform.eulerAngles.y;
        }
    }
}
