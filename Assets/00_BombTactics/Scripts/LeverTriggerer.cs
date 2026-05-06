using UnityEngine;

public class LeverTriggerer : Triggerer
{
    [SerializeField] bool isOn;
    [SerializeField] GameObject lever;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lever)
        {
            triggerable.ChangeValue(isOn);
        }
    }
}
