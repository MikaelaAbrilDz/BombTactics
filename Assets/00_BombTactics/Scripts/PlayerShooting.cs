using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    float yRotation;
    LineRenderer lineRenderer;
    [SerializeField] LayerMask enviroMask;
    [SerializeField] Transform shootPivot;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
        Vector3 newPosition = shootPivot.position;
        float time = 0;
        float gravity = 9.8f;
        float initialSpeedX = 5;
        float initialSpeedY = 5;
        while (Physics.OverlapSphere(newPosition, 0.01f, enviroMask).Length == 0 && time < 50)
        {

            newPosition.x = (initialSpeedX * time);
            newPosition.y = shootPivot.position.y + ((initialSpeedY * time) - (gravity * time * time / 2));
            newPosition = new Vector3(Mathf.Cos(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.x, newPosition.y, Mathf.Sin(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.z);
          
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            
            time += Time.deltaTime;
        }
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
    public void OnChangeToPlayer(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            lineRenderer.positionCount = 0;
        }
    }
}
