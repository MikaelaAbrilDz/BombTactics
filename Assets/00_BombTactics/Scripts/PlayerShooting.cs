using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    float yRotation;
    float xRotation;
    float power = 25;
    float rotSpeed = 0.2f;
    float powSpeed = 4f;
    float shootVelocity = 2f;
    bool isShooting = false;
    LineRenderer lineRenderer;
    [SerializeField] LayerMask enviroMask;
    [SerializeField] LayerMask bodyMask;
    [SerializeField] Transform shootPivot;
    [SerializeField] Transform bomb;
    [SerializeField] Transform explosionPos;
    [SerializeField] GameObject explosionParticle;

    Animator anim;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (PlayerModeManager.currentMode == PlayerModeManager.PlayerMode.Bomb)
        {
            transform.eulerAngles = new Vector3(0, yRotation, 0);
            DrawLine();
        }
        else
        {
            explosionPos.gameObject.SetActive(false);
        }
    }
    void DrawLine()
    {
        explosionPos.gameObject.SetActive(true);
        lineRenderer.positionCount = 0;
        Vector3 newPosition = shootPivot.position;
        Vector3 prevPos = shootPivot.position;
        float time = 0;
        float gravity = 9.8f;
        Vector2 initialSpeed = SetVelocity();
        while (Physics.OverlapSphere(newPosition, 0.5f, enviroMask).Length == 0 && time < 50)
        {
            prevPos = newPosition;
            newPosition.x = (initialSpeed.x * time);
            newPosition.y = shootPivot.position.y + ((initialSpeed.y * time) - (gravity * time * time / 2));
            newPosition = new Vector3(Mathf.Cos(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.x, newPosition.y, Mathf.Sin(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.z);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            
            time += Time.deltaTime;
        }
        RaycastHit hit;
        if (Physics.Raycast(prevPos, newPosition - prevPos, out hit, 10, enviroMask)) explosionPos.forward = hit.normal;
        explosionPos.position = newPosition;
    }
    public void OnChangeDirection(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        yRotation += context.ReadValue<Vector2>().x * rotSpeed;
    }
    public void OnChangeAngle(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        xRotation = Mathf.Clamp(xRotation + context.ReadValue<Vector2>().y * rotSpeed, 0, 75);
    }
    public void OnChangePower(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        power = Mathf.Clamp(power + context.ReadValue<Vector2>().y * powSpeed, 25, 150);
    }
    public void OnChangeToBomb(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        if (context.started)
        {
            yRotation = transform.eulerAngles.y;
        }
    }
    public void OnChangeToPlayer(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        if (context.canceled)
        {
            lineRenderer.positionCount = 0;
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (isShooting) return;
        if (context.started)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        isShooting = true;
        anim.SetTrigger("bomb");
        yield return new WaitForSeconds(.5f);
        Vector3 newPosition = shootPivot.position;
        bomb.gameObject.SetActive(true);
        bomb.position = newPosition;
        float time = 0;
        float gravity = 9.8f;
        Vector2 initialSpeed = SetVelocity();
        yield return new WaitForSeconds(.1f);
        while (Physics.OverlapSphere(newPosition, 0.5f, enviroMask).Length == 0 && time < 10)
        {
            newPosition.x = (initialSpeed.x * time);
            newPosition.y = shootPivot.position.y + ((initialSpeed.y * time) - (gravity * time * time / 2));
            newPosition = new Vector3(Mathf.Cos(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.x, newPosition.y, Mathf.Sin(-(transform.eulerAngles.y - 90) * Mathf.Deg2Rad) * newPosition.x + shootPivot.position.z);

            bomb.position = newPosition;

            time += Time.deltaTime * shootVelocity;
            yield return new WaitForEndOfFrame();
        }
        Instantiate(explosionParticle, bomb.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        foreach (Collider affected in Physics.OverlapSphere(bomb.position, 5f, bodyMask))
        {
            affected.GetComponent<Rigidbody>().AddExplosionForce(450, bomb.position, 7, 50);
        } 

        bomb.gameObject.SetActive(false);
        lineRenderer.positionCount = 0;

        PlayerModeManager.currentMode = PlayerModeManager.PlayerMode.Player;
        PlayerModeManager.input.SwitchCurrentActionMap("Player");
        isShooting = false;
    }
    Vector2 SetVelocity()
    {
        float xVel = Mathf.Cos(xRotation * Mathf.Deg2Rad) * Mathf.Sqrt(power);
        float yVel = Mathf.Sin(xRotation * Mathf.Deg2Rad) * Mathf.Sqrt(power);

        return new Vector2(xVel, yVel);
    }
}
