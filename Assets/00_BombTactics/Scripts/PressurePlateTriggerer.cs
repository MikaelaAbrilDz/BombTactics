using UnityEngine;

public class PressurePlateTriggerer : Triggerer
{
    [SerializeField] float force = 1;
    [SerializeField] Rigidbody plateRb;
    float yLimit;

    private void Start()
    {
        yLimit = plateRb.transform.position.y;
    }
    private void FixedUpdate()
    {
        if (plateRb.transform.position.y < yLimit) plateRb.AddForce(Vector3.up * force, ForceMode.Force);
        else plateRb.linearVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == plateRb.gameObject)
        {
            triggerable.ChangeValue(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == plateRb.gameObject)
        {
            triggerable.ChangeValue(false);
        }
    }
}
